using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskReminder.Core.Domain;

namespace TaskReminder.Web.Mvc
{
    public static class WebViewPageHelper
    {
        public static TabList TabList { get; private set; }

        public static void RegisterTabs()
        {
            TabList = new TabList();

            //TabList
            TabItem companyList = new TabItem("Zákazníci", "company", "list") { ActiveOnController = "company", ActiveOnAction = "*" };
            TabItem taskList = new TabItem("Úkoly", "task", "list") { ActiveOnController = "task", ActiveOnAction = "*" };
            TabItem propertyList = new TabItem("Vlastnosti", "property", "list", Roles.SuperAdmin) { ActiveOnController = "property", ActiveOnAction = "*" };
            TabItem accountList = new TabItem("Uživatelé", "account", "list", Roles.Admin) { ActiveOnController = "account", ActiveOnAction = "*" };

            //Register
            TabList.Register("task", "*", taskList, companyList, accountList, propertyList);
            TabList.Register("company", "*", taskList, companyList, accountList, propertyList);
            TabList.Register("office", "*", taskList, companyList, accountList, propertyList);
            TabList.Register("property", "*", taskList, companyList, accountList, propertyList);
            TabList.Register("account", "*", taskList, companyList, accountList, propertyList);
            TabList.Register("common", "*", taskList, companyList, accountList, propertyList);
        }
    }
}