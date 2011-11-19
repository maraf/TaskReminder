using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TaskReminder.Web.Models;
using TaskReminder.Web.Mvc.Html;

namespace TaskReminder.Web.Mvc
{
    public class UIHelper<TModel>
    {
        public WebViewPage<TModel> Page { get; protected set; }

        public UIHelper(WebViewPage<TModel> page)
        {
            Page = page;
        }

        public MvcHtmlString Tabs()
        {
            StringBuilder output = new StringBuilder();
            string controller = Page.ViewContext.RouteData.Values["controller"].ToString().ToLowerInvariant();
            string action = Page.ViewContext.RouteData.Values["action"].ToString().ToLowerInvariant();

            foreach (TabItem item in WebViewPageHelper.TabList.GetTabs(controller, action).Where(t => Page.UserContext.Is(t.Role)))
                output.Append(TabItem(item.LinkText, Page.Url.Action(item.Action, item.Controller), item.IsActive(controller, action)));

            return MvcHtmlString.Create(output.ToString());
        }

        public MvcHtmlString TabItem(string name, string url, bool selected = false)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.InnerHtml = name;
            tag.Attributes["href"] = url;
            tag.AddCssClass("tab-item");
            if (selected)
                tag.AddCssClass("selected");
            return MvcHtmlString.Create(tag.ToString());
        }

        public MvcHtmlString PageLinks(PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

        public MvcHtmlString DeleteLink(string linkText, string action, object routeValues)
        {
            return DeleteLink(linkText, action, Page.ViewContext.RouteData.Values["controller"].ToString(), routeValues);
        }

        public MvcHtmlString DeleteLink(string linkText, string action, string controller, object routeValues)
        {
            return MvcHtmlString.Create(String.Format("<form name=\"delete-form\" method=\"post\" action=\"{0}\" class=\"inline-form\"><input type=\"submit\" value=\"{1}\" class=\"link-button\" /></form>", 
                Page.Url.Action(action, controller, routeValues), 
                linkText));
        }

        public MvcHtmlString ToByteSize(int size)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int count = 0;
            while (size > 1024)
            {
                size /= 1024;
                count++;
            }
            return MvcHtmlString.Create(String.Format("{0} {1}", size, sizes[count]));
        }

        public MvcHtmlString Message(HtmlMessage message)
        {
            if (message != null)
            {
                TagBuilder tag = new TagBuilder("div");
                tag.AddCssClass("message");
                tag.AddCssClass(message.CssClass);
                tag.InnerHtml = "<div class=\"message-icon\"></div><div class=\"message-content\">" + message.Content + "</div><div class=\"clear\"></div>";
                return MvcHtmlString.Create(tag.ToString());
            }
            else
            {
                return MvcHtmlString.Create("");
            }
        }

        public GridView<T> Grid<T>(IEnumerable<T> items) where T : class
        {
            return new GridView<T>(Page.Html, Page.ViewContext.Writer, Page.Request).SetItems(items);
        }
    }
}