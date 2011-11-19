using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskReminder.Core.Domain;
using TaskReminder.Core.Domain.Repository;
using TaskReminder.Web.Core;

namespace TaskReminder.Web.Mvc
{
    public class Controller : System.Web.Mvc.Controller
    {
        public IRepository Repository { get { return LocateService<IRepository>(); } }

        #region Oprávnění

        private Permissions permissions;

        /// <summary>
        /// Přístup k jednotlivým prvkům.
        /// </summary>
        public Permissions Permissions
        {
            get
            {
                if (permissions == null)
                    permissions = new Permissions { UserContext = UserContext, CurrentDomain = CurrentDomain };

                return permissions;
            }
        }

        #endregion

        #region Nastavení domény

        private Domain currentDomain;

        /// <summary>
        /// Zobrazená doména.
        /// </summary>
        public Domain CurrentDomain
        {
            get
            {
                //TODO: Odebrat ...OrDefault
                if (currentDomain == null)
                    currentDomain = Repository.Domains.FirstOrDefault(d => d.Url == Request.Url.Host);

                return currentDomain;
            }
        }

        #endregion

        #region Přihlášený uživatel

        private UserContext userContext;

        /// <summary>
        /// Vrací instanci aktuálně přihlášeného uživatele.
        /// Pokud není přihlášen, vrací null!
        /// </summary>
        public UserContext UserContext
        {
            get
            {
                if(userContext == null)
                    userContext = new UserContext(Repository.Users.FirstOrDefault(u => u.Username == User.Identity.Name));

                return userContext;
            }
        }

        #endregion

        #region Podpora pro DI

        private Dictionary<Type, object> services = new Dictionary<Type,object>();

        /// <summary>
        /// Vratí implementaci požadované služby.
        /// </summary>
        /// <typeparam name="T">Typ služby</typeparam>
        /// <param name="reuse">Pokud je true, pak se pokusí znovu použít již vytvořenou instanci</param>
        protected T LocateService<T>(bool reuse = true)
        {
            object service;
            if (reuse && services.TryGetValue(typeof(T), out service))
                return (T)service;

            service = DependencyResolver.Current.GetService<T>();
            services.Add(typeof(T), service);

            return (T)service;
        }

        #endregion

        public void ShowMessage(string content, HtmlMessageType type = HtmlMessageType.Success)
        {
            TempData["Message"] = HtmlMessage.Create(content, type);
        }
    }
}