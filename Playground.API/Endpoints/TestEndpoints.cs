using System.Runtime.CompilerServices;
using System.Threading.Channels;
using MediatR;
using Playground.Application.Features.User.Queries;
using Playground.Application.Interfaces;
using Playground.Domain.Interfaces;
using Playground.Domain.Requests;
using Playground.Domain.Responses;

namespace Playground.API.Endpoints;

public static class TestEndpoints
{
    public static void MapTokenEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetTopUsersQuery());
            return result.Success ? Results.Ok(result.Data) : Results.BadRequest(result.Message);
        });

        app.MapGet("/api/orders", (INotificationService service, CancellationToken ct) =>
        {
            return TypedResults.ServerSentEvents(
        service.ReadEvents(ct),
        eventType: "order"
    );
        });

        app.MapPost("/api/orders/create", async (
            CreateOrderRequest request,
            IOrderService service) =>
        {
            await service.CreateOrderAsync(request);

            return Results.Ok();
        });

    }
}