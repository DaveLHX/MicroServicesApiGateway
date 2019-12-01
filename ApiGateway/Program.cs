using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

public class Program
{
    public static int Main(string[] args)
    {
        ConfigureSerilog();
        try
        {
            Log.Information("Starting web host");
            new WebHostBuilder()
           .UseKestrel()
           .UseContentRoot(Directory.GetCurrentDirectory())
           .ConfigureAppConfiguration((hostingContext, config) =>
           {
               config
                   .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                   .AddJsonFile("appsettings.json", true, true)
                   .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                   .AddJsonFile("ocelot.json")
                   .AddEnvironmentVariables();
           })
           .ConfigureServices(s =>
           {
               s.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy());
               s.AddOcelot();
           })
           .UseIISIntegration()
           .UseSerilog()
           .Configure(app =>
           {
               app.UseHealthChecks("/hc", new HealthCheckOptions()
               {
                   Predicate = _ => true,
                   ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
               });
               app.UseOcelot().Wait();
           })
           .Build()
           .Run();
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

    private static void ConfigureSerilog() {
        //serilog
        Log.Logger = new LoggerConfiguration()
          .MinimumLevel.Debug()
          .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
          .Enrich.FromLogContext()
          .WriteTo.Console()
          .WriteTo.Seq("http://seq:5341", apiKey: "yvPGiQuCL4h1ZhxaloKB")
          .CreateLogger();
    }
}