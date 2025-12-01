using Playground.Domain.ApiResponses;

public interface ICurrencyService
{
    Task<List<CurrencyResponse>> GetCurrencyConversionAsync(CancellationToken ct = default);
}