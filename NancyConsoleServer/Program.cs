using CommandLine;
using Nancy;
using Nancy.Hosting.Self;
using System;

namespace NancyStandalone
{
    class Program
    {
        static void Main(string[] args) =>
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    var hostConfiguration = new HostConfiguration();
                    var url = $"http://{o.Url}:{o.Port}";
                    hostConfiguration.UrlReservations.CreateAutomatically = true;
                    using (var host = new NancyHost(hostConfiguration, new Uri(url)))
                    {
                        host.Start();

                        Console.WriteLine("NancyFX Stand alone test application.");
                        Console.WriteLine($"Server running on {url}");
                        Console.WriteLine("Press enter to exit the application");
                        Console.ReadLine();
                    }
                });
    }

    class Options
    {
        [Option('u', "url", Required = false, HelpText = "Url to use. Default is localhost.", Default = "localhost")]
        public string Url { get; set; }

        [Option('p', "port", Required = false, HelpText = "Port to use. Default is 80", Default = 80)]
        public int Port { get; set; }
    }
}

public class HelloModule : NancyModule
{
    public HelloModule()
    {
        Get("/", _ => "Hello World");
    }
}