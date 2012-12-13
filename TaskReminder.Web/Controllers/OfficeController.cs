using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskReminder.Core.Domain;
using TaskReminder.Core.Domain.Repository;
using TaskReminder.Web.Core;
using TaskReminder.Web.Mvc;

namespace TaskReminder.Web.Controllers
{
    [AuthorizeUser]
    public class OfficeController : TaskReminder.Web.Mvc.Controller
    {
        public ActionResult List(int companyID)
        {
            ViewBag.Company = Repository.Companies.First(c => c.ID == companyID);
            return View(Repository.Offices.Where(o => o.Company.ID == companyID).ToArray());
        }

        public ActionResult Edit(int companyID, int id)
        {
            ViewBag.Companies = Repository.Companies.Where(c => c.Domain.ID == CurrentDomain.ID);
            return View(Repository.Offices.First(o => o.ID == id));
        }

        [HttpPost]
        public ActionResult Edit(int companyID, Office office)
        {
            
            if (ModelState.IsValid)
            {
                Repository.Save(office);
                return RedirectToAction("list", new { CompanyID = companyID });
            }

            ViewBag.Companies = Repository.Companies.Where(c => c.Domain.ID == CurrentDomain.ID);
            ViewBag.Company = Repository.Companies.First(c => c.ID == companyID);
            return View(office);
        }

        public ActionResult Create(int companyID)
        {
            ViewBag.Companies = Repository.Companies.Where(c => c.Domain.ID == CurrentDomain.ID);
            return View("Edit", new Office
            {
                Company = Repository.Companies.First(c => c.ID == companyID),
                Address = new Address()
            });
        }

        [HttpPost]
        public ActionResult Delete(int companyID, int id)
        {
            Office company = Repository.Offices.FirstOrDefault(c => c.ID == id);
            if (company != null)
            {
                Repository.Delete(company);
                ShowMessage("Provozovna smazán");
            }
            else
            {
                ShowMessage("Neexistující provozovna", HtmlMessageType.Warning);
            }

            return RedirectToAction("list", new { CompanyID = companyID });
        }
    }
}
