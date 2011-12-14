using System;
using System.Web;

namespace TaskReminder.Web.Core
{
    public class FileHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            
        }
    }
}
