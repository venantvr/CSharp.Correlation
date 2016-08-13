using System;

namespace Contextable
{
    public class Context<T> : IDisposable where T : class, new()
    {
        [ThreadStatic] private static T _contextualizableVariable;
        //[ThreadStatic]
        //private static bool _ownsContextualizableVariable = false;

        private int _thread;

        public Context(T obj)
        {
            //_thread = System.Threading.Thread.CurrentContext.ContextID;

            if (Current == default(T))
            {
                _contextualizableVariable = obj;
                //_ownsContextualizableVariable = true;
            }
        }

        public static T Current => _contextualizableVariable;

        public void Dispose()
        {
            //if (_ownsContextualizableVariable == true)
            //{
            //_contextualizableVariable = default(T);
            //}
        }
    }

    //public class Correlation
    //{
    //    private readonly string _id;

    //    public string Value => _id;

    //    public Correlation()
    //    {

    //    }

    //    public Correlation(string id)
    //    {
    //        _id = id;
    //    }
    //}

    //public class LoggingFilterAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(HttpActionContext filterContext)
    //    {
    //        Debug.WriteLine(Context<Correlation>.Current.Value);
    //    }
    //}

    //public class CorrelationIdContextFilter : ActionFilterAttribute
    //{
    //    private Context<Correlation> _context;
    //    private readonly string _key;

    //    public CorrelationIdContextFilter(string key)
    //    {
    //        _key = key;
    //    }

    //    public override void OnActionExecuting(HttpActionContext actionContext)
    //    {
    //        IEnumerable<string> headerValues = actionContext.Request.Headers.GetValues(_key);

    //        var id = headerValues.FirstOrDefault();

    //        _context = new Context<Correlation>(new Correlation(id));
    //    }

    //    public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
    //    {
    //        if (!actionExecutedContext.Response.Headers.Contains(_key))
    //        {
    //            actionExecutedContext.Response.Headers.Add(_key, Context<Correlation>.Current.Value);
    //        }

    //        _context.Dispose();
    //    }
    //}
}