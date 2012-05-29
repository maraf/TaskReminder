using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskReminder.Core.Domain;
using System.Web.Mvc;

namespace TaskReminder.Web.Models
{
    public class TaskEditViewModel
    {
        public Task Task { get; set; }

        public DropDownModel<TaskState> TaskStates { get; set; }

        public DropDownModel<User> Users { get; set; }

        public DropDownModel<Office> Offices { get; set; }

        public DropDownModel<Company> Companies { get; set; }

        public IEnumerable<TaskAttachment> Attachments { get; set; }

        public TaskEditViewModel(Task task, IEnumerable<TaskState> taskStates, IEnumerable<User> users, IEnumerable<Office> offices, IEnumerable<Company> companies, IEnumerable<TaskAttachment> attachments = null)
        {
            Task = task;
            TaskStates = new DropDownModel<TaskState>(taskStates, t => t.Name);
            Users = new DropDownModel<User>(users, u => u.FirstName + " " + u.LastName, true);
            Offices = new DropDownModel<Office>(offices, o => o.Name);
            Companies = new DropDownModel<Company>(companies, o => o.Name);
            Attachments = attachments ?? new List<TaskAttachment>();
        }

        //public IEnumerable<SelectListItem> GetPeriods()
        //{
        //    yield return new SelectListItem
        //    {
        //        Text = "Měsíčně",
        //        Value = "monthly",
        //    };
        //}
    }
}