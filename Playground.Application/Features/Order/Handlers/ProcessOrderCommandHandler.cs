using MediatR;
using Playground.Application.Interfaces;
using Playground.Domain.Interfaces;

public class ProcessOrderCommandHandler : IRequestHandler<ProcessOrderCommand>
{
    private readonly IOrderService _notificationService;

    public ProcessOrderCommandHandler(IOrderService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Handle(ProcessOrderCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(200);

        // await _notificationService.PublishOrderUpdateAsync(request.OrderId, request.NewStatus);
    }
}