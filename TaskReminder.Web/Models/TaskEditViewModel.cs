using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskReminder.Core.Domain;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TaskReminder.Web.Models
{
    public class TaskEditViewModel<TEntity>
    {
        public TEntity Task { get; set; }

        public DropDownModel<TaskState> TaskStates { get; set; }

        public DropDownModel<User> Users { get; set; }

        public DropDownModel<Office> Offices { get; set; }

        public DropDownModel<Company> Companies { get; set; }

        public IEnumerable<TaskAttachment> Attachments { get; set; }

        public TaskEditViewModel() 
        { }

        public TaskEditViewModel(TEntity task, IEnumerable<TaskState> taskStates, IEnumerable<User> users, IEnumerable<Office> offices, IEnumerable<Company> companies, IEnumerable<TaskAttachment> attachments = null)
        {
            Task = task;
            TaskStates = new DropDownModel<TaskState>(taskStates, t => t.Name);
            Users = new DropDownModel<User>(users, u => u.FirstName + " " + u.LastName, true);
            Offices = new DropDownModel<Office>(offices, o => o.Name);
            Companies = new DropDownModel<Company>(companies, o => o.Name);
            Attachments = attachments ?? new List<TaskAttachment>();
        }
    }

    public class TaskEditViewModel : TaskEditViewModel<Task>
    {
        public TaskEditViewModel(Task task, IEnumerable<TaskState> taskStates, IEnumerable<User> users, IEnumerable<Office> offices, IEnumerable<Company> companies, IEnumerable<TaskAttachment> attachments = null)
            : base(task, taskStates, users, offices, companies, attachments)
        { }
    }

    public class TaskTemplateEditViewModel : TaskEditViewModel<TaskTemplate>
    {
        public TaskTemplateEditViewModel()
        { }

        public TaskTemplateEditViewModel(TaskTemplate task, IEnumerable<TaskState> taskStates, IEnumerable<User> users, IEnumerable<Office> offices, IEnumerable<Company> companies, IEnumerable<TaskAttachment> attachments = null)
            : base(task, taskStates, users, offices, companies, attachments)
        { }

        public IEnumerable<SelectListItem> GetTemplatePeriods()
        {
            return TemplatePeriods.AsSelectList(Task.Period);
        }

        public IEnumerable<SelectListItem> GetMonths()
        {
            yield return new SelectListItem
            {
                Text = "Leden",
                Value = 1.ToString(),
                Selected = Task.CompleteInMonth == 1
            };
            yield return new SelectListItem
            {
                Text = "Únor",
                Value = 2.ToString(),
                Selected = Task.CompleteInMonth == 2
            };
            yield return new SelectListItem
            {
                Text = "Březen",
                Value = 3.ToString(),
                Selected = Task.CompleteInMonth == 3
            };
            yield return new SelectListItem
            {
                Text = "Duben",
                Value = 4.ToString(),
                Selected = Task.CompleteInMonth == 4
            };
            yield return new SelectListItem
            {
                Text = "Květen",
                Value = 5.ToString(),
                Selected = Task.CompleteInMonth == 5
            };
            yield return new SelectListItem
            {
                Text = "Červen",
                Value = 6.ToString(),
                Selected = Task.CompleteInMonth == 6
            };
            yield return new SelectListItem
            {
                Text = "Červenec",
                Value = 7.ToString(),
                Selected = Task.CompleteInMonth == 7
            };
            yield return new SelectListItem
            {
                Text = "Srpen",
                Value = 8.ToString(),
                Selected = Task.CompleteInMonth == 8
            };
            yield return new SelectListItem
            {
                Text = "Září",
                Value = 9.ToString(),
                Selected = Task.CompleteInMonth == 9
            };
            yield return new SelectListItem
            {
                Text = "Říjen",
                Value = 10.ToString(),
                Selected = Task.CompleteInMonth == 10
            };
            yield return new SelectListItem
            {
                Text = "Listopad",
                Value = 11.ToString(),
                Selected = Task.CompleteInMonth == 11
            };
            yield return new SelectListItem
            {
                Text = "Prosinec",
                Value = 12.ToString(),
                Selected = Task.CompleteInMonth == 12
            };
        }
    }
}