using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain.Repository
{
    public interface ITaskStateRepository
    {
        IQueryable<TaskState> TaskStates { get; }

        void Save(TaskState taskState);

        void Delete(TaskState taskState); 
    }
}
