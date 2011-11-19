using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskReminder.Web.Mvc
{
    public class FormButton
    {
        public static FormButtonType GetButton(HttpRequestBase request)
        {
            if (request.Form["save-button"] != null)
                return FormButtonType.Save;

            if (request.Form["saveandclose-button"] != null)
                return FormButtonType.SaveAndClose;

            return FormButtonType.Close;
        }
    }

    public enum FormButtonType
    {
        Save, SaveAndClose, Close
    }
}