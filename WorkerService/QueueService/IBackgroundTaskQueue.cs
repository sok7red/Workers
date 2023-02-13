using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueService
{
    public interface IBackgroundTaskQueue
    {
        Task QueueAsync(Func<CancellationToken, string> cancellationToken);

        Task<Func<CancellationToken, string>> DequeueAsync(CancellationToken cancellationToken);
    }
}
