using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskReminder.Core.Domain.Repository;

namespace TaskReminder.Web.Core
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        private AuthorizationContext filterContext;

        public AuthorizeUserAttribute()
        {

        }

        public AuthorizeUserAttribute(string role)
        {
            Roles = role;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            this.filterContext = filterContext;
            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.Request.IsAuthenticated)
                return false;

            if (!String.IsNullOrEmpty(Roles))
            {
                if ((filterContext.Controller as TaskReminder.Web.Mvc.Controller).UserContext.Is(Roles))
                    return true;
                else
                    return false;
            }

            return base.AuthorizeCore(httpContext);
        }
    }
}