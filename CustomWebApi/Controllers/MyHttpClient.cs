using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Contextable;

namespace CustomWebApi.Controllers
{
    public class MyHttpClient : HttpClient
    {
        public MyHttpClient(string baseUri) : base()
        {
            //using (new Context<Correlation>(new Correlation(Guid.NewGuid().ToString())))
            //{
            this.BaseAddress = new Uri(baseUri);
            this.DefaultRequestHeaders.Accept.Clear();
            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.DefaultRequestHeaders.Add(@"correlation-id", Context<Correlation>.Current.Value);
            //}
        }
    }
}