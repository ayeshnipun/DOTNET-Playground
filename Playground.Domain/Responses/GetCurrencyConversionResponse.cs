namespace Playground.Domain.Responses;

public record GetCurrencyConversionResponse
(
    DateTime CreatedAt,
    string Name,
    string Avatar,
    string Id
);