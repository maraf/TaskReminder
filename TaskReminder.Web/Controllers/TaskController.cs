using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TaskReminder.Core.Domain;
using TaskReminder.Core.Domain.Repository;
using TaskReminder.Web.Core;
using TaskReminder.Web.Models;
using TaskReminder.Web.Mvc;

namespace TaskReminder.Web.Controllers
{
    [AuthorizeUser]
    public class TaskController : TaskReminder.Web.Mvc.Controller
    {
        private IQueryable<Task> OrderTasks(IQueryable<Task> tasks)
        {
            switch (Request.QueryString["Sort"])
            {
                case "Name": return tasks.OrderBy(t => t.Name);
                case "TaskState": return tasks.OrderBy(t => t.TaskState.Name);
                case "Created": return tasks.OrderBy(t => t.Created);
                case "CreatedBy": return tasks.OrderBy(t => t.CreatedBy.FirstName);
                case "Assigned": return tasks.OrderBy(t => t.Assigned);
                case "AssignedTo": return tasks.OrderBy(t => t.AssignedTo.FirstName);
                case "ToComplete": return tasks.OrderByDescending(t => t.ToComplete);
                default: return tasks.OrderByDescending(t => t.Created);
            }
        }

        public ActionResult List(TaskListType type = TaskListType.All)
        {
            TaskListViewModel model = new TaskListViewModel();

            switch (type)
            {
                case TaskListType.All:
                    model.Heading = "Vše";
                    if (UserContext.IsAdmin || UserContext.IsSuperAdmin)
                        model.Items = OrderTasks(Repository.Tasks.Where(t => t.Domain.ID == CurrentDomain.ID)).ToArray();
                    else if (UserContext.IsManager)
                        model.Items = OrderTasks(Repository.Tasks.Where(t => t.Domain.ID == CurrentDomain.ID && (t.CreatedByID == UserContext.CurrentUser.ID
                            || t.CreatedBy.BossID == UserContext.CurrentUser.ID
                            || t.AssignedToID == UserContext.CurrentUser.ID || t.AssignedTo.BossID == UserContext.CurrentUser.ID))).ToArray();
                    else if (UserContext.IsWorker)
                        model.Items = OrderTasks(Repository.Tasks.Where(t => t.Domain.ID == CurrentDomain.ID && (t.CreatedByID == UserContext.CurrentUser.ID 
                            || t.AssignedToID == UserContext.CurrentUser.ID))).ToArray();
                    else if(UserContext.IsBookKeeper)
                        model.Items = OrderTasks(Repository.Tasks.Where(t => t.Domain.ID == CurrentDomain.ID && (t.CreatedByID == UserContext.CurrentUser.ID
                            || t.AssignedToID == UserContext.CurrentUser.ID
                            || t.TaskState.Flag == TaskStateFlag.Approved))).ToArray();
                    break;
                case TaskListType.Assigned:
                    model.Heading = "Přiřazené úkoly";
                    model.Items = OrderTasks(Repository.Tasks.Where(t => t.AssignedTo.ID == UserContext.CurrentUser.ID && t.Domain.ID == CurrentDomain.ID)).ToArray();
                    break;
                case TaskListType.Created:
                    model.Heading = "Vytvořené úkoly";
                    model.Items = OrderTasks(Repository.Tasks.Where(t => t.CreatedBy.ID == UserContext.CurrentUser.ID && t.Domain.ID == CurrentDomain.ID)).ToArray();
                    break;
                case TaskListType.Handled:
                    model.Heading = "Zpracovávané úkoly";
                    model.Items = OrderTasks(Repository.Tasks
                        .Where(t => t.AssignedToID == UserContext.CurrentUser.ID && t.TaskState.Flag == TaskStateFlag.Handled && t.Domain.ID == CurrentDomain.ID)).ToArray();
                    break;
                default:
                    throw new ApplicationException("Type not specified.");
            }

            return View("List", model);
        }

        public ActionResult Create()
        {
            return View("Edit", new TaskEditViewModel(
                new Task
                {
                    TaskState = Repository.TaskStates.FirstOrDefault(t => t.Flag == TaskStateFlag.Created)
                },
                Repository.TaskStates.Where(s => s.Domain.ID == CurrentDomain.ID).ToArray(),
                Repository.Users.Where(u => u.Domain.ID == CurrentDomain.ID).ToArray(),
                Repository.Offices.Where(o => o.Company.Domain.ID == CurrentDomain.ID).ToArray(),
                Repository.Companies.Where(o => o.Domain.ID == CurrentDomain.ID).ToArray()
            ));
        }

        public ActionResult Edit(int taskId)
        {
            Task task = Repository.Tasks.FirstOrDefault(t => t.ID == taskId);
            if (Permissions.CanAccessTask(task))
            {
                return View("Edit", new TaskEditViewModel(
                    task,
                    Repository.TaskStates.Where(s => s.Domain.ID == CurrentDomain.ID).ToArray(),
                    Repository.Users.Where(u => u.Domain.ID == CurrentDomain.ID).ToArray(),
                    Repository.Offices.Where(o => o.Company.Domain.ID == CurrentDomain.ID).ToArray(),
                    Repository.Companies.Where(o => o.Domain.ID == CurrentDomain.ID).ToArray(),
                    Repository.TaskAttachments.Where(a => a.Task.ID == taskId)
                ));
            }
            else
            {
                return View("Sorry");
            }
        }

        [HttpPost]
        public ActionResult Edit([Bind(Prefix="Task")] Task task)
        {
            bool isNew = false;

            task.Domain = CurrentDomain;
            task.CreatedBy = Repository.Users.FirstOrDefault(u => u.ID == task.CreatedByID);
            task.AssignedTo = Repository.Users.FirstOrDefault(u => u.ID == task.AssignedToID);
            task.TaskState = Repository.TaskStates.FirstOrDefault(s => s.ID == task.TaskStateID);
            task.Office = Repository.Offices.FirstOrDefault(o => o.ID == task.OfficeID);

            if (!Permissions.CanAccessTask(task))
                return View("Sorry");

            if (task.ID == 0)
            {
                task.Created = DateTime.Now;
                task.CreatedBy = UserContext.CurrentUser;
                isNew = true;
            }

            if (task.AssignedToID != null)
                task.Assigned = DateTime.Now;

            if (ModelState.IsValid)
            {
                if ((isNew || HasAssignedChanged(task.ID, task.AssignedToID, task.TaskState.Flag)) && task.AssignedTo != null)
                    EmailHelper.SendTaskAssigned(task);

                Repository.Save(task);
                ShowMessage("Úkol uložen.");

                if (FormButton.GetButton(Request) == FormButtonType.SaveAndClose)
                    return RedirectToAction("list");

                return RedirectToAction("edit", new { TaskID = task.ID });
            }

            return View("Edit", new TaskEditViewModel(
                task,
                Repository.TaskStates.Where(s => s.Domain.ID == CurrentDomain.ID).ToArray(),
                Repository.Users.Where(u => u.Domain.ID == CurrentDomain.ID).ToArray(),
                Repository.Offices.Where(o => o.Company.Domain.ID == CurrentDomain.ID).ToArray(),
                Repository.Companies.Where(o => o.Domain.ID == CurrentDomain.ID).ToArray(),
                Repository.TaskAttachments.Where(a => a.Task.ID == task.ID)
            ));
        }

        private bool HasAssignedChanged(int taskID, int? assignedTo, int newState)
        {
            var task = Repository.Tasks.Select(t => new
            {
                ID = t.ID,
                AssignedToID = t.AssignedToID,
                TaskStateFlag = t.TaskState.Flag
            }).FirstOrDefault(t => t.ID == taskID);

            if(task == null)
                return false;

            if (task.AssignedToID != assignedTo && newState == TaskStateFlag.Assigned)
                return true;

            return task.TaskStateFlag != newState && newState == TaskStateFlag.Assigned;
        }

        [HttpPost]
        public ActionResult Delete(int taskId)
        {
            Task task = Repository.Tasks.FirstOrDefault(t => t.ID == taskId);
            if (Permissions.CanAccessTask(task))
            {
                Repository.Delete(task);
                ShowMessage("Úkol smazán.");
            }
            return RedirectToAction("list");
        }

        public ActionResult Attachment(int taskId, int? attachmentId)
        {
            Task task = Repository.Tasks.FirstOrDefault(t => t.ID == taskId);
            if (Permissions.CanAccessTask(task))
            {
                if (attachmentId == null)
                    return View(new TaskAttachment() { Task = task });
                else
                    return View(Repository.TaskAttachments.First(a => a.ID == attachmentId.Value));
            }
            else
            {
                return RedirectToAction("list");
            }
        }

        [HttpPost]
        public ActionResult Attachment(int taskId, TaskAttachment attachment, HttpPostedFileBase file)
        {
            Task task = Repository.Tasks.FirstOrDefault(t => t.ID == taskId);
            if (Permissions.CanAccessTask(task))
            {
                if (!ModelState.IsValid)
                    return View(attachment);

                if (attachment.ID == 0 && file == null)
                {
                    ModelState.AddModelError("", "Musíte zadat soubor, který se má nahrát!");
                    return View(attachment);
                }

                if (attachment.ID == 0)
                {
                    attachment.Task = task;
                    attachment.Creator = UserContext.CurrentUser;
                }
                else
                {
                    attachment.Creator = Repository.Users.FirstOrDefault(u => u.ID == attachment.CreatorID);
                }

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath("~/Attachments"), fileName));

                    attachment.FileName = fileName;
                    attachment.ContentType = file.ContentType;
                    attachment.ContentLength = file.ContentLength;
                    attachment.Creator = UserContext.CurrentUser;
                }
                attachment.Created = DateTime.Now;
                Repository.Save(attachment);
                ShowMessage("Příloha uložena k úKolu.");

                return RedirectToAction("edit", new { taskId = taskId });
            }
            else
            {
                return RedirectToAction("list");
            }
        }

        [HttpPost]
        public ActionResult DeleteAttachment(int attachmentId)
        {
            TaskAttachment attachment = Repository.TaskAttachments.FirstOrDefault(a => a.ID == attachmentId);
            if (attachment != null && Permissions.CanAccessTask(attachment.Task))
            {
                int taskId = attachment.Task.ID;
                string file = Server.MapPath(Path.Combine("~/Attachments", attachment.FileName));
                Repository.Delete(attachment);
                ShowMessage("Příloha smazána.");

                if(System.IO.File.Exists(file))
                    System.IO.File.Delete(file);
                
                return RedirectToAction("edit", new { TaskID = taskId });
            }
            return RedirectToAction("list");
        }
    }
}
