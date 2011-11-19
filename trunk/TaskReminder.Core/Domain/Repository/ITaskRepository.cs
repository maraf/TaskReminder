using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain.Repository
{
    public interface ITaskRepository
    {
        IQueryable<Task> Tasks { get; }

        void Save(Task task);

        void Delete(Task task);
    }
}
