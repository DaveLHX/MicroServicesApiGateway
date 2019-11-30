using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace CadetApi
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace;

        // public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
        public static int Main(string[] args)
        {

            //added to get the config earlier.
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddEnvironmentVariables();

            var config = builder.Build();
            //not used for now because needed to troubleshoot the mssqlserver connection
            var connectionString = CustomExtensionMethods.GetConnectionString(config);

            Log.Logger = new LoggerConfiguration()
             //.MinimumLevel.Verbose()
             .MinimumLevel.Debug()
            // .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
             //.Enrich.WithProperty("CadetContext", AppName)
             .Enrich.FromLogContext()
             .WriteTo.Console()
             .WriteTo.MSSqlServer("Data Source=db;Initial Catalog=cadetapi;User Id=SA;Password=Duuu988@P4w0RD!", "SeriLog", autoCreateSqlTable: true)
             .WriteTo.Seq("http://seq:5341", apiKey: "bIkNLejR84naps0prwcF")
             //.ReadFrom.Configuration(config)
             .CreateLogger();

            try
            {
                Log.Information("Starting web host ({ApplicationContext})...", AppName);
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }


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