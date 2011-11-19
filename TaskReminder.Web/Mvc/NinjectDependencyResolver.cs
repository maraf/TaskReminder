using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Syntax;
using TaskReminder.Core.Domain.Repository;
using TaskReminder.Core.EntityFramework;
using TaskReminder.Web.Core;

namespace TaskReminder.Web.Mvc
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public IKernel Kernel
        {
            get { return kernel; }
        }

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            Kernel.Bind<IRepository>().To<Repository>().InRequestScope();
            Kernel.Bind<IDomainRepository>().To<Repository>().InRequestScope();
            Kernel.Bind<IUserRepository>().To<Repository>().InRequestScope();
            Kernel.Bind<ITaskRepository>().To<Repository>().InRequestScope();
            Kernel.Bind<ICompanyRepository>().To<Repository>().InRequestScope();
            Kernel.Bind<IOfficeRepository>().To<Repository>().InRequestScope();
            Kernel.Bind<IPropertyKeyRepository>().To<Repository>().InRequestScope();
            Kernel.Bind<IAuthProvider>().To<FormsAuthProvider>().InRequestScope();
        }

        public object GetService(Type serviceType)
        {
            //if (serviceType.IsAssignableFrom(typeof(IRepository)))
            //    return kernel.TryGet<IRepository>();

            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public IBindingToSyntax<T> Bind<T>()
        {
            return kernel.Bind<T>();
        }
    }
}