using Common.Logging;
using Contact.Infrastructure.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Linq;

namespace Contact.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().MigrateDatabase().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(SeriLogger.Configure)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.Sources.Remove(
                    configurationBuilder.Sources.First(source =>
                    source.GetType() == typeof(EnvironmentVariablesConfigurationSource))); //remove the default one first
                    configurationBuilder.AddEnvironmentVariables();
                });
    }
}
