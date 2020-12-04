using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FundaScraperClient;
using Grpc.Net.Client;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            Stopwatch x = new Stopwatch();

            goto Http;

            var client = new FundaScraper.FundaScraperClient(channel);

            Task<GetObjectCountReply>[] tasks = Enumerable.Range(0, 1000)
                .Select(x => client.GetObjectCountAsync(new GetObjectCountRequest {Url = "http://boo.com"}).ResponseAsync)
                .ToArray();

            x.Start();

            await Task.WhenAll(tasks);

            x.Stop();

            Console.WriteLine($"gRPC Done in {x.Elapsed}");

Http:

            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000")
            };

            IEnumerable<Task<HttpResponseMessage>> httpTasks = Enumerable.Range(0, 100)
                .Select(x => httpClient.PostAsync("thing", new StringContent("{ \"url\" = \"http://boo.com\" }", Encoding.UTF8, "application/json")))
                .ToArray();

            x.Reset();

            x.Start();

            await Task.WhenAll(httpTasks);

            x.Stop();

            Console.WriteLine($"HTTP Done in {x.Elapsed}");

            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();
        }
    }
}