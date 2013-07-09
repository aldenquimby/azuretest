using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Chatter
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthorizeAttribute());
            //filters.Add(new RequireHttpsAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        public static void RegisterAuth()
        {
            //OAuthWebSecurity.RegisterGoogleClient();
            //OAuthWebSecurity.RegisterYahooClient();
            //OAuthWebSecurity.RegisterFacebookClient();
            //OAuthWebSecurity.RegisterLinkedInClient();
            //OAuthWebSecurity.RegisterTwitterClient();
            //OAuthWebSecurity.RegisterMicrosoftClient();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterAuth();
        }
    }
}