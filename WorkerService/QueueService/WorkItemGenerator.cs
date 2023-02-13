using Microsoft.Extensions.Hosting.Internal;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueService
{
    public class WorkItemGenerator
    {
        private readonly IBackgroundTaskQueue _taksqueue;
        private static ILogger<WorkItemGenerator> Logger { get; set; }
        
        private readonly CancellationToken _cancellationToken = CancellationToken.None;
        private int Counter { get; set; }

        public WorkItemGenerator(IBackgroundTaskQueue taksqueue,
            ILogger<WorkItemGenerator> logger,
            IHostApplicationLifetime applicationLifetime)
        {
            _taksqueue = taksqueue;
            Logger = logger;
            _cancellationToken = applicationLifetime.ApplicationStopping;
            Counter = 0;
        }

        public void Generate()
        {
            Logger.LogInformation($"{nameof(GenerateAsync)} loop is starting.");

            Task.Run(async () => await GenerateAsync());
        }

        private async Task GenerateAsync()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                Logger.LogInformation($"Press W to add an item in Queue");

                var keyStroke = Console.ReadKey();
                if (keyStroke.Key == ConsoleKey.W)
                {
                    await _taksqueue.QueueAsync(GenerateWorkItem);
                }
                else if (keyStroke.Key == ConsoleKey.Q)
                {
                    Logger.LogInformation($"Quit was requested. Program is exiting");
                    Environment.Exit(0);
                }
            }
        }

        public string GenerateWorkItem(CancellationToken arg)
        {
            var response = $"Queue item {Counter}";
            Counter++;

            return response;
        }
    }
}
