using LibrarySystem.Services.Database;
using LibrarySystem.Services.Interfaces;
using LibrarySystem.Services.Repositories;
using LibrarySystem.Services.Services;
using LibrarySystem.Web.App_Start;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
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
            GlobalConfiguration.Configure(WebApiConfig.Register);            
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
            var container = new UnityContainer();
            RegisterComponents(container);

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            DependencyResolver.SetResolver(new Unity.AspNet.Mvc.UnityDependencyResolver(container));
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.Url.LocalPath.StartsWith("/api/"))
            {
                return;
            }
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
            HttpCookie authCookie = Request.Cookies["AuthToken"];
            if (authCookie != null)
            {
                var token = authCookie.Value;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    var identity = new ClaimsIdentity(jsonToken.Claims, "JWT");
                    HttpContext.Current.User = new ClaimsPrincipal(identity);
                }
            }
        }

        private void RegisterComponents(IUnityContainer container)
        {
            container.RegisterType<LibraryDbContext>();

            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IBookRepository, BookRepository>();
            container.RegisterType<IRentalRepository, RentalRepository>();
            container.RegisterType<IAuthService, AuthService>();

            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IBookService, BookService>();
            container.RegisterType<IRentalService, RentalService>();
        }
    }
}
