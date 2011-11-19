using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Text;
using System.IO;

namespace TaskReminder.Web.Mvc.Html
{
    public class GridView<T> where T : class
    {
        private HtmlHelper html;
        private TextWriter writer;
        private HttpRequestBase request;

        private string emptyText;
        private string cssClass = "grid";
        private IEnumerable<T> items;
        private List<GridViewColumn<T>> columns;

        private Action<TagBuilder, T> buildRow;

        public GridView(HtmlHelper html, TextWriter writer, HttpRequestBase request)
        {
            columns = new List<GridViewColumn<T>>();
            this.html = html;
            this.writer = writer;
            this.request = request;
        }

        public GridView<T> SetItems(IEnumerable<T> items)
        {
            this.items = items;
            return this;
        }

        public GridView<T> SetEmptyText(string emptyText)
        {
            this.emptyText = emptyText;
            return this;
        }

        public GridView<T> AddCssClass(string cssClass)
        {
            if (!String.IsNullOrEmpty(this.cssClass))
                this.cssClass += " ";

            this.cssClass += cssClass;
            return this;
        }

        public GridView<T> AddColumn(GridViewColumn<T> column)
        {
            columns.Add(column);
            return this;
        }

        public GridView<T> AddColumn(string header, Func<T, object> value, string headerCssClass = null)
        {
            return AddColumn(null, header, value, headerCssClass);
        }

        public GridView<T> AddColumn(string name, string header, Func<T, object> value, string headerCssClass = null)
        {
            return AddColumn(new GridViewColumn<T>
            {
                Name = name,
                Header = header,
                Value = value,
                HeaderCssClass = headerCssClass
            });
        }

        public GridView<T> OnBuildRow(Action<TagBuilder, T> buildRow)
        {
            this.buildRow = buildRow;
            return this;
        }

        public void Render()
        {
            if (items != null && items.Count() < 0)
            {
                writer.Write(emptyText);
                return;
            }

            StringBuilder result = new StringBuilder();

            result.AppendFormat("<table class='{0}'>", cssClass);
            result.Append("<tr>");

            foreach (GridViewColumn<T> column in columns)
                result.AppendFormat("<th class='{0}'>{1}</th>", column.HeaderCssClass + (IsSorted(column.Name) ? " " + "sorted" : ""), column.Name != null ? String.Format("<a href='{0}'>{1}</a>", SortLink(column.Name), column.Header) : column.Header);

            result.Append("</tr>");

            int i = 0;
            foreach (T item in items)
            {
                TagBuilder tr = new TagBuilder("tr");
                tr.AddCssClass(i % 2 == 0 ? "even" : "idle");

                if (buildRow != null)
                    buildRow(tr, item);

                foreach (GridViewColumn<T> column in columns)
                    tr.InnerHtml += String.Format("<td>{0}</td>", column.Value(item));

                result.Append(tr.ToString());
                i++;
            }

            result.Append("</table>");

            //return MvcHtmlString.Create(result.ToString());
            writer.Write(result.ToString());
        }

        private string SortLink(string property)
        {
            string result = request.Url.PathAndQuery;

            if(!request.QueryString.AllKeys.Contains("Sort")) {
                if (!request.QueryString.HasKeys())
                    result += "?";
                else
                    result += "&";

                return result + "Sort=" + property;
            }

            if (request.QueryString["Sort"] != property)
                return result.Replace("Sort=" + request.QueryString["Sort"], "Sort=" + property);

            return result;
        }

        private bool IsSorted(string property)
        {
            if (property == null)
                return false;

            return request.QueryString["Sort"] == property;
        }
    }
}