using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Contextable
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            Debug.WriteLine(Context<Correlation>.Current.Value);
        }
    }
}