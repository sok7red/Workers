using System.Reflection.Emit;

namespace QueueService
{
    public class Worker : BackgroundService
    {
        public IBackgroundTaskQueue TaskQueue { get; }
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, IBackgroundTaskQueue taskQueue)
        {
            TaskQueue = taskQueue;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Thread.Sleep(20);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    Func<CancellationToken, string>? workItem =
                        await TaskQueue.DequeueAsync(stoppingToken);

                    _logger.LogInformation($"Dequeuing : {workItem(stoppingToken)}");

                }
                catch (OperationCanceledException)
                {
                    // Prevent throwing if stoppingToken was signaled
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing task work item.");
                }
            }
        }
    }
}