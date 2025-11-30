using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Channels;
using Playground.Application.Interfaces;
using Playground.Domain.Events;
using Playground.Domain.Responses;

public class NotificationService : INotificationService
{
    private readonly Channel<ServerSentEvent> _channel =
        Channel.CreateUnbounded<ServerSentEvent>();

    public ChannelReader<ServerSentEvent> Reader => _channel.Reader;

    public Task PublishOrderAsync(OrderCreatedEvent sseEvent)
    {
        var json = JsonSerializer.Serialize(sseEvent);

        var sse = new ServerSentEvent(Data: new[] { json }, EventType: "order");

        return _channel.Writer.WriteAsync(sse).AsTask();
    }

    public async IAsyncEnumerable<ServerSentEvent> ReadEvents([EnumeratorCancellation] CancellationToken ct)
    {
        var reader = _channel.Reader;

        while (await reader.WaitToReadAsync(ct))
        {
            while (reader.TryRead(out var sse))
            {
                Console.WriteLine("--------Got SSE event");
                yield return sse;
            }
        }
    }

}