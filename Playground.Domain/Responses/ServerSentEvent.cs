namespace Playground.Domain.Responses;

public record ServerSentEvent(
    IEnumerable<string>? Data = null,
    string? EventType = null,
    string? Id = null,
    int? Retry = null
);
