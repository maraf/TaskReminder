using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TaskReminder.Core.Domain
{
    public class TaskAttachment : Attachment
    {
        public virtual Task Task { get; set; }
    }
}
