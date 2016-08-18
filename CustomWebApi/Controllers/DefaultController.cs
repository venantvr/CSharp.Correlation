using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Contextable;
using Contextable.Tools;
using Contracts;
using CustomMassTransit;

namespace CustomWebApi.Controllers
{
    public class DefaultController : ApiController
    {
        [HyperLink("hello")]
        [Route("hello")]
        [HttpGet]
        public async Task<HttpResponseMessage> Hello()
        {
            var id = Context<Correlation>.Current.Value;

            Debug.WriteLine(@"Hello : " + id);

            using (var client = new MyHttpClient("http://localhost:1913/"))
            {
                var response = await client.GetAsync("bye");
            }

            var bus = new MyPublisher();

            bus.Publish(new YourMessage1 {Text = "Hi1", Date = DateTime.Now});

            return Request.CreateResponse(HttpStatusCode.OK,
                @"hello, l'identifiant de corrélation est : " + Context<Correlation>.Current.Value);
        }

        [HyperLink("bye")]
        [Route("bye")]
        [HttpGet]
        public HttpResponseMessage Bye()
        {
            var id = Context<Correlation>.Current.Value;

            Debug.WriteLine(@"Bye : " + id);

            return Request.CreateResponse(HttpStatusCode.OK, @"bye");
        }
    }
}