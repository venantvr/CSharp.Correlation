using System;
using System.Linq;
using System.Web.Http;

namespace CustomWebApi.Controllers
{
    public class Output<T> // where C : ApiController 
    {
        public Output(T output, ApiController controller)
        {
            Date = DateTime.Now;
            Message = output;
            Controller = controller;

            var type = controller.GetType();
            var methods = type.GetMethods().Where(m => m.GetCustomAttributes(typeof (HyperLinkAttribute), false).Any());
        }

        public ApiController Controller { get; set; }
        public T Message { get; set; }
        public DateTime Date { get; set; }
    }
}