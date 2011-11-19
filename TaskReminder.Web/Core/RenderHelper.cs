using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskReminder.Core.Domain;

namespace TaskReminder.Web.Core
{
    public static class RenderHelper
    {
        public static Func<User, string> UserToString()
        {
            return u => u.FirstName + " " + u.LastName;
        }
    }
}