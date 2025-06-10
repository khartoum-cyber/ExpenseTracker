using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExpenseTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //instance of IHost, implements IDisposable
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<App>(); // Register your App class
                })
                .Build();

            // Run the app
            var app = host.Services.GetRequiredService<App>();
            app.Run();
        }
    }
}