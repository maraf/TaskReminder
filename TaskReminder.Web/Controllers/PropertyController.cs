using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskReminder.Core.Domain.Repository;
using TaskReminder.Core.Domain;
using TaskReminder.Web.Core;

namespace TaskReminder.Web.Controllers
{
    [AuthorizeUser]
    public class PropertyController : TaskReminder.Web.Mvc.Controller
    {
        public ActionResult List()
        {
            return View(Repository.PropertyKeys.Where(k => k.Domain.ID == CurrentDomain.ID).OrderBy(k => k.Name));
        }

        public ActionResult Edit(int id)
        {
            return View(Repository.PropertyKeys.First(k => k.ID == id && k.Domain.ID == CurrentDomain.ID));
        }

        [HttpPost]
        public ActionResult Edit(PropertyKey propertyKey)
        {
            propertyKey.Domain = Repository.Domains.First(d => d.ID == propertyKey.Domain.ID);
            if (ModelState.IsValid)
            {
                Repository.Save(propertyKey);
                return RedirectToAction("list");
            }

            return View(propertyKey);
        }

        public ActionResult Create()
        {
            return View("Edit", new PropertyKey { Domain = CurrentDomain });
        }

        [HttpPost]
        public ActionResult Delete(int propertyKeyID)
        {
            PropertyKey key = Repository.PropertyKeys.FirstOrDefault(k => k.ID == propertyKeyID);
            if (key != null)
            {
                Repository.Delete(key);
            }
            return RedirectToAction("list");
        }
    }
}
