using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Playground.Domain.Events;
using Playground.Domain.Responses;

namespace Playground.Application.Interfaces;

public interface INotificationService
{
    Task PublishOrderAsync(OrderCreatedEvent sseEvent);

    IAsyncEnumerable<ServerSentEvent> ReadEvents([EnumeratorCancellation] CancellationToken ct);
}