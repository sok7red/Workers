using Microsoft.Extensions.Configuration;

namespace QueueService
{
    public class Program
    {
        private static ILogger<Program>? Logger { get; set; }
        public static async Task Main(string[] args)
        {
            // Identify environment
            var appEnvironment = Environment.GetEnvironmentVariable("WorkerServiceVariable") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .AddJsonFile($"appsettings.{appEnvironment}.json")
                .Build();

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    builder.Sources.Clear();
                    builder.AddConfiguration(configuration);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IBackgroundTaskQueue>(x =>
                    {
                        if (!int.TryParse(context.Configuration.GetValue<string>("Settings:Capacity"),out var capacity) )
                        {
                            capacity = 10000;
                        }

                        return new DefaultBackgroundTaskQueue(capacity);
                    });

                    services.AddHostedService<Worker>();
                    services.AddSingleton<WorkItemGenerator>();
                })
                .Build();

            Logger = host.Services.GetRequiredService<ILogger<Program>>();

            Logger.LogInformation($"Application stating");

            Logger.LogInformation($"Loading configuration for environment: {appEnvironment}");
            
            try
            {
                var generator = host.Services.GetRequiredService<WorkItemGenerator>();
                generator.Generate();

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError($"INNER EXCEPTION  :   {ex.Message}");
                Logger.LogError($"STACK TRACE      :   {ex.StackTrace}");
                Environment.Exit(1);
            }

            // Success .. exit 
            Logger.LogInformation("Program successfully completed!");
            Environment.Exit(0);
        }
    }
}