using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskReminder.Web.Controllers
{
    public class CommonController : TaskReminder.Web.Mvc.Controller
    {
        [ChildActionOnly]
        public ActionResult VersionInfo()
        {
            return View();
        }

        public ActionResult Version()
        {
            return View();
        }
    }
}
