using System.Threading.Channels;

namespace QueueService
{
    public class DefaultBackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<Func<CancellationToken, string>> _queue;

        public DefaultBackgroundTaskQueue(int capacity)
        {
            _queue = Channel.CreateBounded<Func<CancellationToken,string>>(capacity);
        }

        public async Task QueueAsync(Func<CancellationToken, string> workItem)
        {
            await _queue.Writer.WriteAsync(workItem);
        }

        public async Task<Func<CancellationToken, string>> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
