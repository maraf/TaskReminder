using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using TaskReminder.Core.Domain.Repository;
using TaskReminder.Core.EntityFramework;

namespace TaskReminder.Web.Mvc
{
    [Obsolete("Use NinjectDependencyResolver!")]
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            //ninjectKernel.Rebind<IDomainResolver>().To<RequestDomainResolver>().WithConstructorArgument("domainName", requestContext.HttpContext.Request.Url.Host);

            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            // Repositories
            //ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            ninjectKernel.Bind<IRepository>().To<Repository>();
            ninjectKernel.Bind<IDomainRepository>().To<Repository>();
            ninjectKernel.Bind<IUserRepository>().To<Repository>();
        }
    }
}