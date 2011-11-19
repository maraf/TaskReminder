using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskReminder.Web.Mvc.Html
{
    public class GridViewColumn<T> where T : class
    {
        public string Name { get; set; }

        public string Header { get; set; }

        public string HeaderCssClass { get; set; }

        public Func<T, object> Value { get; set; }
    }
}