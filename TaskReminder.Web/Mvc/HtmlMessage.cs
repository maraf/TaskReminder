using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskReminder.Web.Mvc
{
    public class HtmlMessage
    {
        public string Content { get; set; }

        public HtmlMessageType Type { get; set; }

        public string CssClass
        {
            get
            {
                switch (Type)
                {
                    case HtmlMessageType.Success:
                        return "message-success";
                    case HtmlMessageType.Warning:
                        return "message-warning";
                    case HtmlMessageType.Error:
                        return "message-error";
                }
                return "";
            }
        }

        public static HtmlMessage Create(string content, HtmlMessageType type = HtmlMessageType.Success)
        {
            return new HtmlMessage
            {
                Content = content,
                Type = type
            };
        }
    }

    public enum HtmlMessageType
    {
        Success, Warning, Error
    }
}