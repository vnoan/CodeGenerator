using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using TreasuryChallenge.Repositories;
using TreasuryChallenge.Services;

namespace TreasuryChallenge
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = Host
                .CreateDefaultBuilder()
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("appsettings.json");
                })
                .ConfigureServices(service =>
                {
                    service
                        .AddLogging(logging =>
                        {
                            logging.ClearProviders();
                            logging.AddConsole();
                        })
                        .ConfigureServices()
                        .ConfigureRepositories()
                        .AddSingleton<Handler>();
                })
                .Build();

            var serviceProvider = host.Services.CreateScope().ServiceProvider;

            var handler = serviceProvider.GetService<Handler>();
            Directory.EnumerateFiles(".", "codes*").ToList().ForEach(f => File.Delete(f));
            Console.WriteLine("Tell me the number of lines do you need and press enter.");
            var count = int.Parse(Console.ReadLine());
            handler.Handle(count);
            Console.WriteLine("Done!");

            ////Código para observar o benchmark
            //var logger = serviceProvider.GetService<ILogger<Program>>();
            //for (int i = 1; i <= 1_000_000; i = i * 10)
            //{
            //    Directory.EnumerateFiles(".", "codes*").ToList().ForEach(f => File.Delete(f));
            //    var t = Stopwatch.StartNew();
            //    handler.Handle(i);
            //    t.Stop();
            //    logger.LogInformation($"{i} linhas em {t.ElapsedMilliseconds} ms");
            //}
        }
    }
}