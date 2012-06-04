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
    public class TaskTemplateController : TaskReminder.Web.Mvc.Controller
    {
        public ActionResult List()
        {
            return View(Repository.TaskTemplates.ToList());
        }

        public ActionResult Create()
        {
            return View("Edit", new TaskTemplateEditViewModel(
                new TaskTemplate
                {
                    AutoRepeat = true,
                    Period = TemplatePeriods.Quarterly,
                    TaskState = Repository.TaskStates.FirstOrDefault(t => t.Flag == TaskStateFlag.Created)
                },
                Repository.TaskStates.Where(s => s.Domain.ID == CurrentDomain.ID).ToArray(),
                Repository.Users.Where(u => u.Domain.ID == CurrentDomain.ID).ToArray(),
                Repository.Offices.Where(o => o.Company.Domain.ID == CurrentDomain.ID).ToArray(),
                Repository.Companies.Where(o => o.Domain.ID == CurrentDomain.ID).ToArray()
            ));
        }

        public ActionResult Edit(int taskID)
        {
            TaskTemplate task = Repository.TaskTemplates.FirstOrDefault(t => t.ID == taskID);
            if (task == null)
            {
                ShowMessage("Úkol neexistuje!", HtmlMessageType.Warning);
                return RedirectToAction("list");
            }

            return View("Edit", new TaskTemplateEditViewModel(
                task,
                Repository.TaskStates.Where(s => s.Domain.ID == CurrentDomain.ID).ToArray(),
                Repository.Users.Where(u => u.Domain.ID == CurrentDomain.ID).ToArray(),
                Repository.Offices.Where(o => o.Company.Domain.ID == CurrentDomain.ID).ToArray(),
                Repository.Companies.Where(o => o.Domain.ID == CurrentDomain.ID).ToArray()
            ));
        }

        [HttpPost]
        public ActionResult Edit(TaskTemplateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Task.Domain = CurrentDomain;
                model.Task.CreatedBy = Repository.Users.FirstOrDefault(u => u.ID == model.Task.CreatedByID);
                model.Task.AssignedTo = Repository.Users.FirstOrDefault(u => u.ID == model.Task.AssignedToID);
                model.Task.TaskState = Repository.TaskStates.FirstOrDefault(s => s.ID == model.Task.TaskStateID);
                model.Task.Office = Repository.Offices.FirstOrDefault(o => o.ID == model.Task.OfficeID);

                if (model.Task.ID == 0)
                {
                    model.Task.Created = DateTime.Now;
                    model.Task.CreatedBy = UserContext.CurrentUser;
                }

                Repository.Save(model.Task);
                ShowMessage("Opakový úkol uložen");

                return RedirectToAction("edit", new { TaskID = model.Task.ID });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int taskId)
        {
            TaskTemplate task = Repository.TaskTemplates.FirstOrDefault(t => t.ID == taskId);
            if (task != null)
            {
                Repository.Delete(task);
                ShowMessage("Opakový úkol smazán.");
            }
            return RedirectToAction("list");
        }
    }
}
