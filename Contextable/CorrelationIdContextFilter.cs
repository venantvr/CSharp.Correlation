using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Contextable
{
    public class CorrelationIdContextFilter : ActionFilterAttribute
    {
        private readonly string _key;
        private Context<Correlation> _context;

        public CorrelationIdContextFilter(string key)
        {
            _key = key;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var headerValues = actionContext.Request.Headers.GetValues(_key);

            var id = headerValues.FirstOrDefault();

            _context = new Context<Correlation>(new Correlation(id));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (!actionExecutedContext.Response.Headers.Contains(_key))
            {
                actionExecutedContext.Response.Headers.Add(_key, Context<Correlation>.Current.Value);
            }

            _context.Dispose();
        }
    }
}