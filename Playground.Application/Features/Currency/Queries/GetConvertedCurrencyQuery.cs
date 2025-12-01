using MediatR;
using Playground.Domain.Common;
using Playground.Domain.Responses;

namespace Playground.Application.Features.Currency.Queries;

public record GetConvertedCurrencyQuery() : IRequest<BaseResponse<List<GetCurrencyConversionResponse>>>;