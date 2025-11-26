using MediatR;
using Playground.Domain.Common;
using Playground.Domain.Responses;

namespace Playground.Application.Features.User.Queries;

public record GetTopUsersQuery : IRequest<BaseResponse<List<GetTopFiveUsersResponse>>>;