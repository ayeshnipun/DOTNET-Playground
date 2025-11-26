using MediatR;
using Playground.Application.Features.User.Queries;
using Playground.Domain.Common;
using Playground.Domain.Interfaces;
using Playground.Domain.Responses;
using System.Linq;

namespace Playground.Application.Features.User.Handlers;

public class GetTopUsersQueryHandler : IRequestHandler<GetTopUsersQuery, BaseResponse<List<GetTopFiveUsersResponse>>>
{
    private readonly IUserRepository _userRepository;

    public GetTopUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<List<GetTopFiveUsersResponse>>> Handle(GetTopUsersQuery request, CancellationToken cancellationToken)
    {
        var topUsers = await _userRepository.GetTopUsersAsync(5);

        return BaseResponse<List<GetTopFiveUsersResponse>>.Ok(topUsers.Select(user => new GetTopFiveUsersResponse
        (
            UserId: user.Id,
            UserName: user.Username,
            CountOfComments: user.Comments.Count,
            TopPostsWithLikes: user.Posts
                .OrderByDescending(post => post.Likes.Count)
                .Take(5)
                .Select(post => new GetPostsWithLikesResponse
                (
                    PostId: post.Id,
                    LikesCount: post.Likes.Count
                ))
                .ToArray()
        )).ToList());
    }
}