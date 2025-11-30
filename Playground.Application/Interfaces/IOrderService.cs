using Playground.Domain.Requests;

namespace Playground.Application.Interfaces;

public interface IOrderService
{
    Task CreateOrderAsync(CreateOrderRequest request);
}