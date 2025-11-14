namespace Playground.Domain.DTOs;

public record GetTopFiveUsersDto
(
    int UserId,
    string UserName,
    int CountOfComments,
    GetPostsWithLikesDto[] TopPostsWithLikes
);