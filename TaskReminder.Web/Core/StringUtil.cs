using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace TaskReminder.Web.Core
{
    public static class StringUtil
    {
        public static string ToUrl(string content)
        {
            string stFormD = content.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC)).Replace(" ", "-").ToLowerInvariant();
        }

        public static string Cut(string content, int length = 20)
        {
            if (content == null)
                return "";

            if (content.Length > length)
                return content.Substring(0, length) + " ...";

            return content;
        }
    }
}