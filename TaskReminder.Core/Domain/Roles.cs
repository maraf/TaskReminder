using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace TaskReminder.Core.Domain
{
    public static class Roles
    {
        private static Dictionary<string, string> RoleToName = new Dictionary<string, string>();

        /// <summary>
        /// Vidí vše, může spravovat domény.
        /// </summary>
        public const string SuperAdmin = "super-admin";

        /// <summary>
        /// Nejvyšší pracovník v doméně.
        /// </summary>
        public const string Admin = "admin";

        /// <summary>
        /// Vedoucí pracovník.
        /// </summary>
        public const string Manager = "manager";

        /// <summary>
        /// Běžný pracovník.
        /// </summary>
        public const string Worker = "worker";

        /// <summary>
        /// Účetní domény.
        /// </summary>
        public const string BookKeeper = "bookkeeper";

        public static List<SelectListItem> AsSelectList(User user, bool addSuperAdmin = false)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            if (user.Role == Roles.SuperAdmin || user.Role == Admin)
            {
                if(addSuperAdmin)
                    result.Add(new SelectListItem { Text = GetRoleName(SuperAdmin), Value = SuperAdmin });

                result.Add(new SelectListItem { Text = GetRoleName(Admin), Value = Admin });
            }

            result.Add(new SelectListItem { Text = GetRoleName(Manager), Value = Manager });
            result.Add(new SelectListItem { Text = GetRoleName(Worker), Value = Worker });
            result.Add(new SelectListItem { Text = GetRoleName(BookKeeper), Value = BookKeeper });

            return result;
        }

        public static string GetRoleName(string role)
        {
            if (RoleToName.Count == 0)
            {
                RoleToName.Add(BookKeeper, "Účetní");
                RoleToName.Add(SuperAdmin, "Super Administrator");
                RoleToName.Add(Admin, "Administrator");
                RoleToName.Add(Worker, "Pracovník");
                RoleToName.Add(Manager, "Vedoucí");
            }

            return RoleToName[role];
        }
    }
}
