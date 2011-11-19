using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskReminder.Core.Domain.Repository;
using TaskReminder.Core.Domain;
using TaskReminder.Web.Core;
using TaskReminder.Web.Models;

namespace TaskReminder.Web.Controllers
{
    [AuthorizeUser]
    public class CompanyController : TaskReminder.Web.Mvc.Controller
    {
        public ActionResult List()
        {
            return View(Repository.Companies.Where(c => c.Domain.ID == CurrentDomain.ID).OrderBy(c => c.Name).ToArray());
        }

        public ActionResult Detail(int id)
        {
            return View(Repository.Companies.First(c => c.ID == id));
        }

        public ActionResult Create()
        {
            return View("Edit", new CompanyEditViewModel
            {
                Item = new Company
                {
                    Domain = CurrentDomain,
                    Address = new Address()
                },
                Keys = Repository.PropertyKeys.Where(p => p.Domain.ID == CurrentDomain.ID && p.Target == "Company").ToArray(),
                Properties = new List<CompanyProperty>()
            });
        }

        public ActionResult Edit(int companyId)
        {
            Company company = Repository.Companies.First(c => c.ID == companyId && c.Domain.ID == CurrentDomain.ID);
            return View(new CompanyEditViewModel
            {
                Item = company,
                Keys = Repository.PropertyKeys.Where(p => p.Domain.ID == CurrentDomain.ID && p.Target == "Company").ToArray(),
                Properties = Repository.CompanyProperties.Where(p => p.Target.ID == company.ID).ToArray()
            });
        }

        [HttpPost]
        public ActionResult Edit([Bind(Prefix="Item")] Company company, IDictionary<int, string> properties)
        {
            if (!ModelState.IsValid)
            {
                return View(new CompanyEditViewModel
                {
                    Item = company,
                    Keys = Repository.PropertyKeys.Where(p => p.Domain.ID == CurrentDomain.ID && p.Target == "Company").ToArray(),
                    Properties = Repository.CompanyProperties.Where(p => p.Target.ID == company.ID).ToArray()
                });
            }

            company.Domain = CurrentDomain;
            Repository.Save(company);

            if (properties != null)
            {
                List<CompanyProperty> resultprop = new List<CompanyProperty>();
                foreach (KeyValuePair<int, string> item in properties)
                {
                    CompanyProperty prop = Repository.CompanyProperties.FirstOrDefault(p => p.Target.ID == company.ID && p.Key.ID == item.Key);
                    if (prop == null)
                    {
                        resultprop.Add(new CompanyProperty
                        {
                            Key = Repository.PropertyKeys.First(p => p.ID == item.Key),
                            Target = company,
                            Value = item.Value
                        });
                    }
                    else
                    {
                        prop.Value = item.Value;
                        resultprop.Add(prop);
                    }
                }
                Repository.Save(resultprop.ToArray());
            }

            return RedirectToAction("list");
        }
    }
}
