using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain
{
    /// <summary>
    /// Stav úkolu
    /// </summary>
    public class TaskState : BaseEntity, IDomain
    {
        public string Name { get; set; }

        public int Flag { get; set; }

        public Domain Domain { get; set; }
    }
}
