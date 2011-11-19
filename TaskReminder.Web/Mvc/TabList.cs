using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskReminder.Web.Mvc
{
    public class TabList
    {
        private Dictionary<string, List<TabItem>> tabMapping = new Dictionary<string, List<TabItem>>();

        public void Register(string controller, string action, params TabItem[] tabs)
        {
            string key = GetKey(controller, action);
            List<TabItem> tabList;

            controller = controller.ToLowerInvariant();
            action = action.ToLowerInvariant();

            if (tabMapping.TryGetValue(key, out tabList))
            {
                foreach (TabItem item in tabs)
                {
                    if (!tabList.Contains(item))
                        tabList.Add(item);
                }
            }
            else
            {
                tabList = new List<TabItem>();
                tabList.AddRange(tabs);
                tabMapping.Add(key, tabList);
            }
        }

        public List<TabItem> GetTabs(string controller, string action)
        {
            controller = controller.ToLowerInvariant();
            action = action.ToLowerInvariant();

            List<TabItem> tabList;
            if (tabMapping.TryGetValue(GetKey(controller, action), out tabList))
                return tabList;

            if (tabMapping.TryGetValue(GetKey(controller, "*"), out tabList))
                return tabList;

            return new List<TabItem>();
        }

        public string GetKey(string controller, string action)
        {
            return String.Format("{0}-{1}", controller, action);
        }
    }

    public class TabItem
    {
        public string LinkText { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string ActiveOnController { get; set; }

        public string ActiveOnAction { get; set; }

        public string Role { get; set; }

        public TabItem(string linkText, string controller, string action, string role = null)
        {
            LinkText = linkText;
            Controller = controller;
            Action = action;
            Role = role;
        }

        public bool IsActive(string controller, string action)
        {
            string thisController = ActiveOnController ?? Controller;
            string thisAction = ActiveOnAction ?? Action;

            return (thisController == "*" || thisController == controller) && (thisAction == "*" || thisAction == action);
        }
    }
}