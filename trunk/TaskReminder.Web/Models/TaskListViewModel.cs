using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskReminder.Core.Domain;

namespace TaskReminder.Web.Models
{
    public class TaskListViewModel
    {
        public IEnumerable<Task> Items { get; set; }

        public string Heading { get; set; }
    }
}