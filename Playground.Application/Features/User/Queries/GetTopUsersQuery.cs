using MediatR;
using Playground.Domain.DTOs;

namespace Playground.Application.Features.User.Queries;

public record GetTopUsersQuery : IRequest<List<GetTopFiveUsersDto>>;