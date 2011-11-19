using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskReminder.Web.Mvc.Html
{
    public class ButtonSection :IDisposable
    {
        protected ViewContext context;

        public ButtonSection(ViewContext context)
        {
            this.context = context;

            context.Writer.Write("<div class='buttons'>");
        }

        public void Dispose()
        {
            context.Writer.Write("</div>");
        }
    }
}