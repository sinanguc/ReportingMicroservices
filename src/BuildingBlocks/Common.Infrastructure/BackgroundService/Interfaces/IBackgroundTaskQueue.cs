using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Infrastructure.BackgroundService.Interfaces
{
    /// <summary>
    /// Reference: https://docs.microsoft.com/en-us/dotnet/core/extensions/queue-service
    /// </summary>
    public interface IBackgroundTaskQueue
    {
        ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> workItem);
        ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken);
    }
}
