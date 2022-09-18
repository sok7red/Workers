using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
    
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var runFrequency =  _configuration.GetValue<int>("WorkerConfig:RunFrequency");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker running at: {DateTimeOffset.Now}, next run in {runFrequency} msec");
                await Task.Delay(runFrequency, stoppingToken);
            }
        }
    }
}