using System.Threading.Tasks;
using Contextable;

namespace ConsoleWebApi
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var client = new MyHttpClient("http://localhost:1913/"))
            {
                var response = Task.Run(() => client.GetAsync("bye"));
                var headers = response.Result.Headers;
                var errorMessage = response.Result.Content.ReadAsStringAsync().Result;
            }
        }
    }
}