using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TaskReminder.Core.Domain;
using TaskReminder.Core.Domain.Repository;

namespace TaskReminder.Web.Core
{
    public class FormsAuthProvider : IAuthProvider
    {
        IRepository Repository { get; set; }

        public FormsAuthProvider(IRepository repository)
        {
            Repository = repository;
        }

        public IAuthResult Authenticate(string domain, string username, string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                User user = Repository.Users.FirstOrDefault(u => u.Domain.Url == domain && u.Username == username && u.Password == null && u.Enabled);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return new FormsAuthResult(new UserContext(user), true);
                }
            }
            else
            {
                User user = Repository.Users.FirstOrDefault(u => u.Domain.Url == domain && u.Username == username && u.Password == password && u.Enabled);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return new FormsAuthResult(new UserContext(user));
                }
            }

            return new FormsAuthResult();
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }

    public class FormsAuthResult : IAuthResult
    {
        public bool IsAuthenticated { get; protected set; }

        public bool IsNew { get; protected set; }

        public UserContext UserContext { get; protected set; }

        public FormsAuthResult(UserContext userContext = null, bool? isNew = null)
        {
            IsAuthenticated = userContext != null;
            UserContext = userContext;
            IsNew = isNew ?? false;
        }
    }

}