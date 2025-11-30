using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Playground.Application.Interfaces;
using Playground.Domain.Events;
using Playground.Domain.Requests;
using Playground.Domain.Responses;

namespace Playground.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly INotificationService notificationService;

    public OrderService(INotificationService notificationService)
    {
        this.notificationService = notificationService;
    }

    public async Task CreateOrderAsync(CreateOrderRequest request)
    {
        var evt = new OrderCreatedEvent
        {
            OrderId = 123,
            UserId = request.UserId,
            Total = 499.00,
            Time = DateTime.UtcNow
        };

        // Push notification to SSE
        await notificationService.PublishOrderAsync(evt);
    }
}