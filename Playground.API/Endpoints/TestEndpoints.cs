using MediatR;
using Playground.Application.Features.User.Queries;

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
    }
}