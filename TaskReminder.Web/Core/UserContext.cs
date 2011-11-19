using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskReminder.Core.Domain;

namespace TaskReminder.Web.Core
{
    public class UserContext
    {
        public User CurrentUser { get; protected set; }

        public UserContext(User currentUser)
        {
            CurrentUser = currentUser;
        }

        public bool Is(string role)
        {
            switch (role)
            {
                case null: return true;
                case Roles.SuperAdmin: return IsSuperAdmin;
                case Roles.Admin: return IsAdmin;
                case Roles.Manager: return IsManager;
                case Roles.Worker: return IsWorker;
                case Roles.BookKeeper: return IsBookKeeper;
            }
            return false;
        }

        public bool IsSuperAdmin
        {
            get { return IsUserSuperAdmin(CurrentUser); }
        }

        public bool IsAdmin
        {
            get { return IsUserAdmin(CurrentUser); }
        }

        public bool IsManager
        {
            get { return IsUserManager(CurrentUser); }
        }

        public bool IsWorker
        {
            get { return IsUserWorker(CurrentUser); }
        }

        public bool IsBookKeeper
        {
            get { return IsUserBookKeeper(CurrentUser); }
        }


        public static bool IsUserSuperAdmin(User user)
        {
            if (user == null)
                return false;

            return user.Role == Roles.SuperAdmin;
        }

        public static bool IsUserAdmin(User user)
        {
            if (user == null)
                return false;

            return IsUserSuperAdmin(user) || user.Role == Roles.Admin;
        }

        public static bool IsUserManager(User user)
        {
            if (user == null)
                return false;

            return IsUserAdmin(user) || user.Role == Roles.Manager;
        }

        public static bool IsUserWorker(User user)
        {
            if (user == null)
                return false;

            return IsUserManager(user) || user.Role == Roles.Worker;
        }

        public static bool IsUserBookKeeper(User user)
        {
            if (user == null)
                return false;

            return IsUserWorker(user) || user.Role == Roles.BookKeeper;
        }
    }
}