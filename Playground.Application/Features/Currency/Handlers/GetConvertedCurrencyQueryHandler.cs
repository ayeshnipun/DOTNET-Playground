using MediatR;
using Playground.Application.Features.Currency.Queries;
using Playground.Domain.Common;
using Playground.Domain.Responses;

public class GetConvertedCurrencyQueryHandler : IRequestHandler<GetConvertedCurrencyQuery, BaseResponse<List<GetCurrencyConversionResponse>>>
{
    private readonly ICurrencyService currencyService;

    public GetConvertedCurrencyQueryHandler(ICurrencyService currencyService)
    {
        this.currencyService = currencyService;
    }

    public async Task<BaseResponse<List<GetCurrencyConversionResponse>>> Handle(GetConvertedCurrencyQuery request, CancellationToken cancellationToken)
    {
        var currencies = await currencyService.GetCurrencyConversionAsync();

        var mapped = currencies.Select(c => new GetCurrencyConversionResponse(
            c.CreatedAt,
            c.Name,
            c.Avatar,
            c.Id
        )).ToList();

        return BaseResponse<List<GetCurrencyConversionResponse>>.Ok(mapped);
    }
}