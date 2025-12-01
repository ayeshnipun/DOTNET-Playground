namespace Playground.Domain.ApiResponses;

public record CurrencyResponse
(
    DateTime CreatedAt,
    string Name,
    string Avatar,
    string Id
);