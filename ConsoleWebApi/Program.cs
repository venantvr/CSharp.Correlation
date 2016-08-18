using System.Threading.Tasks;
using Contextable.Tools;

namespace ConsoleWebApi
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var client = new MyHttpClient("http://localhost:1913/"))
            {
                // ReSharper disable once AccessToDisposedClosure
                var response = Task.Run(() => client.GetAsync("bye"));
                var headers = response.Result.Headers;
                var errorMessage = response.Result.Content.ReadAsStringAsync().Result;
            }
        }
    }
}