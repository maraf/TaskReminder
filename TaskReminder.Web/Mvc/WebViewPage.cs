using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using TaskReminder.Core.Domain.Repository;
using TaskReminder.Web.Core;

namespace TaskReminder.Web.Mvc
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public IRepository Repository { get { return LocateService<IRepository>(); } }

        public UIHelper<TModel> UI { get; protected set; }

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
                if (userContext == null)
                    userContext = new UserContext(Repository.Users.FirstOrDefault(u => u.Username == User.Identity.Name));

                return userContext;
            }
        }

        #endregion

        public WebViewPage()
            : base()
        {
            UI = new UIHelper<TModel>(this);
        }

        #region Podpora pro DI

        private Dictionary<Type, object> services = new Dictionary<Type, object>();

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
    }
}