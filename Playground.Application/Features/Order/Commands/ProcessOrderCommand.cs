using MediatR;

public record ProcessOrderCommand(int OrderId, string NewStatus) : IRequest;