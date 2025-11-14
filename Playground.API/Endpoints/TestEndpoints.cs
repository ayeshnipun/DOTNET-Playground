using MediatR;

namespace Playground.API.Endpoints;

public static class TestEndpoints
{
    public static void MapTokenEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users", async (IMediator mediator) =>
        {
            return Results.Ok();
            // var result = await mediator.Send(new GetAdtTokenQuery());
            // return result.Success ? Results.Ok(result.Data) : Results.BadRequest(result.Message);
        });
    }
}