using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;

namespace CadetApi
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace;

        // public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
        public static void Main(string[] args)
        {
            //all this code added for serilog based on eshop demo project
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddEnvironmentVariables();

            var config = builder.Build();
            var connectionString = CustomExtensionMethods.GetConnectionString(config);
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Verbose()
              .Enrich.WithProperty("CadetContext", AppName)
              .Enrich.FromLogContext()
              .WriteTo.Console()
             //.WriteTo.MSSqlServer(connectionString, "SeriLog", autoCreateSqlTable: true)
              //.ReadFrom.Configuration(config)
              .CreateLogger();
            Log.Information("Starting web host ({ApplicationContext})...", AppName);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}