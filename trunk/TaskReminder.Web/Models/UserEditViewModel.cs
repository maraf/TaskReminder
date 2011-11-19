using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskReminder.Core.Domain;
using TaskReminder.Web.Core;

namespace TaskReminder.Web.Models
{
    public class UserEditViewModel
    {
        public User User { get; set; }

        public DropDownModel<User> Users { get; set; }

        public List<SelectListItem> Roles { get; set; }

        public UserEditViewModel(User user, IEnumerable<User> users, UserContext context)
        {
            User = user;
            Users = new DropDownModel<User>(users, RenderHelper.UserToString(), true);

            Roles = TaskReminder.Core.Domain.Roles.AsSelectList(context.CurrentUser);
            if (!context.IsSuperAdmin)
                users.Where(u => u.Role != TaskReminder.Core.Domain.Roles.SuperAdmin);
        }
    }
}