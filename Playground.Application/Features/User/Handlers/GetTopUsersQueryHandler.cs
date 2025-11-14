using MediatR;
using Playground.Application.Features.User.Queries;

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
            UserName: user.UserName,
            CountOfComments: user.Comments.Count,
            TopPostsWithLikes: user.Posts
                .OrderByDescending(post => post.Likes.Count)
                .Take(3)
                .Select(post => new GetPostsWithLikesDto
                (
                    PostId: post.Id,
                    PostTitle: post.Title,
                    CountOfLikes: post.Likes.Count
                ))
                .ToArray()
        )).ToList();
    }
}