using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Hlyt.CommandProcessor.Command;
using Hlyt.CommandProcessor.Dispatcher;
using Hlyt.Data.Infrastructure;
using Hlyt.Data.Repositories;
using Hlyt.Web.Core.Authentication;
using Hlyt.Web.Mappers;


namespace Hlyt.Web.AppStart
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().InstancePerHttpRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(typeof (UserRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerHttpRequest();

            //builder.RegisterAssemblyTypes(typeof (CategoryRepository).Assembly)
            //    .Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces().InstancePerHttpRequest();

            Assembly services = Assembly.Load("Hlyt.Data");

            builder.RegisterAssemblyTypes(services)
                .AsClosedTypesOf(typeof (ICommandHandler<>)).InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(services)
                .AsClosedTypesOf(typeof (IValidationHandler<>)).InstancePerHttpRequest();

            builder.RegisterType<DefaultFormsAuthentication>().As<IFormsAuthentication>().InstancePerHttpRequest();

            builder.RegisterFilterProvider();

            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
