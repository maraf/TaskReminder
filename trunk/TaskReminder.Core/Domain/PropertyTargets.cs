using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace TaskReminder.Core.Domain
{
    public static class PropertyTargets
    {
        public static readonly string Company = "Company";

        public static IEnumerable<SelectListItem> AsSelectedList(string selected)
        {
            yield return new SelectListItem
            {
                Selected = selected == "Company",
                Text = "Company"
            };
        }
    }
}
