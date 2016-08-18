using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using Contextable.Attributes;

namespace CustomWebApi
{
    public class WebApiApplication : HttpApplication
    {
        public static void RegisterWebApiFilters(HttpFilterCollection filters)
        {
            filters.Add(new CorrelationFilterAttribute(@"correlation-id"));
            filters.Add(new LoggingFilterAttribute());
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);
        }
    }
}