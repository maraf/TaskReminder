using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskReminder.Core.Domain;

namespace TaskReminder.Web.Core
{
    public class Permissions
    {
        public UserContext UserContext { get; set; }

        public Domain CurrentDomain { get; set; }

        
        public bool CanAccessTask(Task t)
        {
            if (t.IsNew)
                return true;

            if (UserContext.IsAdmin || UserContext.IsSuperAdmin)
                return t.Domain.ID == CurrentDomain.ID;
            else if (UserContext.IsManager)
                return t.Domain.ID == CurrentDomain.ID && (t.CreatedByID == UserContext.CurrentUser.ID
                    || t.CreatedBy.BossID == UserContext.CurrentUser.ID || t.AssignedToID == UserContext.CurrentUser.ID || t.AssignedTo.BossID == UserContext.CurrentUser.ID);
            else if (UserContext.IsWorker)
                return t.Domain.ID == CurrentDomain.ID && (t.CreatedByID == UserContext.CurrentUser.ID || t.AssignedToID == UserContext.CurrentUser.ID);
            else if (UserContext.IsBookKeeper)
                return t.Domain.ID == CurrentDomain.ID && (t.CreatedByID == UserContext.CurrentUser.ID || t.AssignedToID == UserContext.CurrentUser.ID || t.TaskState.Flag == TaskStateFlag.Approved);
            else
                return false;
        }
    }
}