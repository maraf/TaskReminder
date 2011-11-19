using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace TaskReminder.Web.Mvc
{
    public class DateTimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string value = GetValue(bindingContext, bindingContext.ModelName);

            DateTime dateTime;
            if (DateTime.TryParse(value, CultureInfo.GetCultureInfo("cs-CZ"), DateTimeStyles.None, out dateTime))
                return dateTime;
            else
                return base.BindModel(controllerContext, bindingContext);
        }

        private string GetValue(ModelBindingContext context, string key)
        {
            ValueProviderResult vpr = context.ValueProvider.GetValue(key);
            return vpr == null ? null : vpr.AttemptedValue;
        }
    }
}