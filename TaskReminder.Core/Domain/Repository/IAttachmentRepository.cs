using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain.Repository
{
    public interface IAttachmentRepository
    {
        IQueryable<TaskAttachment> TaskAttachments { get; }

        void Save(TaskAttachment attachment);

        void Delete(TaskAttachment attachment);
    }
}
