using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using Microsoft.Owin.Security;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using System.Web;

[assembly: OwinStartup(typeof(LibrarySystem.Web.Startup))]

namespace LibrarySystem.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();            

            ConfigureOAuth(app);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "https://localhost:44364/",
                    ValidAudience = "https://localhost:44364/",
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String("3q2+796tvuJOtRgv9fFsppCvnPjm3nTEZ+RbRtN+kNk="))
                }
            });

            app.UseWebApi(config);

            app.Use(async (context, next) =>
            {
                var httpContext = context.Get<HttpContextBase>(typeof(HttpContextBase).FullName);
                if (httpContext != null)
                {
                    HttpContext.Current = httpContext.ApplicationInstance.Context;
                }
                await next();
            });
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30)
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }
    }
}
