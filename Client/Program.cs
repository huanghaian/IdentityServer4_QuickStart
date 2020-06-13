using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                var client = new HttpClient();
                var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
                if (disco.IsError)
                {
                    Console.WriteLine(disco.Error);
                    return;
                }
                var tokenRespose = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId="client",
                    ClientSecret = "secret",
                    Scope = "api1"
                
                });
                if (tokenRespose.IsError)
                {
                    Console.WriteLine(tokenRespose.Error);
                    return;
                }
                Console.WriteLine(tokenRespose.Json);
                Console.WriteLine("start api");
                var apiClient=new HttpClient();
                apiClient.SetBearerToken(tokenRespose.AccessToken);
                var response = await apiClient.GetAsync("https://localhost:6001/api/identity");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.Content);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(JArray.Parse(content));
                }
                Console.ReadKey();
            });
            Console.ReadKey();
        }
    }
}
