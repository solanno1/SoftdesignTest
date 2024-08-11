using LibrarySystem.Services.Database;
using LibrarySystem.Services.Interfaces;
using LibrarySystem.Services.Repositories;
using LibrarySystem.Services.Services;
using LibrarySystem.Web.App_Start;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Unity;
namespace LibrarySystem.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var container = new UnityContainer();
            RegisterComponents(container);
            DependencyResolver.SetResolver(new Unity.AspNet.Mvc.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
        private void RegisterComponents(IUnityContainer container)
        {
            container.RegisterType<LibraryDbContext>();

            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IBookRepository, BookRepository>();
            container.RegisterType<IRentalRepository, RentalRepository>();

            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IBookService, BookService>();
            container.RegisterType<IRentalService, RentalService>();

        }
    }
}
