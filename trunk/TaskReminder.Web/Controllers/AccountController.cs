using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskReminder.Core.Domain;
using TaskReminder.Core.Domain.Repository;
using TaskReminder.Web.Core;
using TaskReminder.Web.Models;

namespace TaskReminder.Web.Controllers
{
    public class AccountController : TaskReminder.Web.Mvc.Controller
    {
        #region LOGIN/LOGOUT

        public IAuthProvider Authenticator { get { return LocateService<IAuthProvider>(); } }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IAuthResult result = Authenticator.Authenticate(CurrentDomain.Url, model.Username, model.Password);
                if (!result.IsAuthenticated)
                {
                    ModelState.AddModelError("", "Neplatné uživatelské jméno nebo heslo!");
                    model.Password = null;
                    return View(model);
                }

                if (result.IsNew)
                    return RedirectToAction("changepassword");
                else
                    return Redirect("~/");
            }    

            return View(model);
        }

        [AuthorizeUser]
        public ActionResult UserInfo()
        {
            return PartialView(UserContext.CurrentUser);
        }

        [HttpPost]
        [AuthorizeUser]
        public ActionResult Logout()
        {
            Authenticator.SignOut();
            return RedirectToAction("login");
        }

        #endregion

        #region CRUD

        [AuthorizeUser(Roles.Admin)]
        public ActionResult List()
        {
            IEnumerable<User> users = Repository.Users.Where(u => u.Domain.ID == CurrentDomain.ID).ToArray();

            if (!UserContext.IsSuperAdmin)
                users = users.Where(u => u.Role != Roles.SuperAdmin);

            return View(users);
        }

        [AuthorizeUser(Roles.Admin)]
        public ActionResult Edit(int id)
        {
            User user = Repository.Users.FirstOrDefault(u => u.Domain.ID == CurrentDomain.ID && u.ID == id);
            if (user != null)
            {
                return View(new UserEditViewModel(user, Repository.Users.Where(u => u.Domain.ID == CurrentDomain.ID && u.Role == Roles.Manager).ToArray(), UserContext));
            }
            return RedirectToAction("list");
        }

        [HttpPost]
        public ActionResult Edit([Bind(Prefix="User")] User user)
        {
            User original = Repository.Users.FirstOrDefault(u => u.ID == user.ID);
            if (original == null)
                original = new User();

            user.Boss = Repository.Users.FirstOrDefault(u => u.ID == user.BossID);

            if (String.IsNullOrEmpty(user.Username) || Repository.Users.FirstOrDefault(u => u.Username == user.Username && u.ID != user.ID && u.Domain.ID == CurrentDomain.ID) != null)
                ModelState.AddModelError("", "Uživatelské jméno je již použito jiným uživatelem.");

            if (!ModelState.IsValid)
                return View(new UserEditViewModel(user, Repository.Users.Where(u => u.Domain.ID == CurrentDomain.ID && u.Role == Roles.Manager).ToArray(), UserContext));

            original.Domain = CurrentDomain;
            original.FirstName = user.FirstName;
            original.LastName = user.LastName;
            original.Username = user.Username;
            original.Email = user.Email;
            original.Role = user.Role;
            original.BossID = user.BossID;
            original.Boss = user.Boss;
            original.Enabled = user.Enabled;

            Repository.Save(original);

            return RedirectToAction("list");
        }

        [AuthorizeUser(Roles.Admin)]
        public ActionResult Create()
        {
            return View("Edit", new UserEditViewModel(new User(), Repository.Users.Where(u => u.Domain.ID == CurrentDomain.ID && u.Role == Roles.Manager).ToArray(), UserContext));
        }

        [HttpPost]
        [AuthorizeUser(Roles.Admin)]
        public ActionResult ResetPassword(int userID, string returnUrl = null)
        {
            User user = Repository.Users.FirstOrDefault(u => u.ID == userID && u.Domain.ID == CurrentDomain.ID);
            if (user != null)
            {
                user.Password = null;
                Repository.Save(user);
                ShowMessage("Heslo uživatele vymazáno, při příštím přihlášení bude požádán o zadání nového.");
            }

            if (returnUrl != null)
                return Redirect(returnUrl);

            return RedirectToAction("list");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        [AuthorizeUser]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            UserContext.CurrentUser.Password = model.NewPassword;
            Repository.Save(UserContext.CurrentUser);

            return Redirect("~/");
        }

        [AuthorizeUser]
        public ActionResult Change()
        {
            return View(new UserChangeViewModel(UserContext.CurrentUser));
        }

        [HttpPost]
        [AuthorizeUser]
        public ActionResult Change(UserChangeViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            UserContext.CurrentUser.FirstName = user.FirstName;
            UserContext.CurrentUser.LastName = user.LastName;
            Repository.Save(UserContext.CurrentUser);

            return Redirect("~/");
        }

        #endregion
    }
}
