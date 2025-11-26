using MediatR;
using Playground.Application.Features.User.Queries;
using Playground.Domain.DTOs;
using Playground.Domain.Interfaces;
using System.Linq;

namespace Playground.Application.Features.User.Handlers;

public class GetTopUsersQueryHandler : IRequestHandler<GetTopUsersQuery, List<GetTopFiveUsersDto>>
{
    private readonly IUserRepository _userRepository;

    public GetTopUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<GetTopFiveUsersDto>> Handle(GetTopUsersQuery request, CancellationToken cancellationToken)
    {
        var topUsers = await _userRepository.GetTopUsersAsync(5);

        return topUsers.Select(user => new GetTopFiveUsersDto
        (
            UserId: user.Id,
            UserName: user.Username,
            CountOfComments: user.Comments.Count,
            TopPostsWithLikes: user.Posts
                .OrderByDescending(post => post.Likes.Count)
                .Take(5)
                .Select(post => new GetPostsWithLikesDto
                (
                    PostId: post.Id,
                    LikesCount: post.Likes.Count
                ))
                .ToArray()
        )).ToList();
    }
}