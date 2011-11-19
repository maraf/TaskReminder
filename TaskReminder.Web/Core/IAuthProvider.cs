using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Web.Core
{
    public interface IAuthProvider
    {
        IAuthResult Authenticate(string domain, string username, string password);

        void SignOut();
    }

    public interface IAuthResult
    {
        bool IsAuthenticated {get;}

        bool IsNew { get; }

        UserContext UserContext { get; }
    }
}
