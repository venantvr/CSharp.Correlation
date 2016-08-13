using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Contextable
{
    public class MyHttpClient : HttpClient
    {
        public MyHttpClient(string baseUri)
        {
            //using (new Context<Correlation>(new Correlation(Guid.NewGuid().ToString())))
            //{
            BaseAddress = new Uri(baseUri);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            DefaultRequestHeaders.Add(@"correlation-id", Context<Correlation>.Current.Value);
            //}
        }
    }
}