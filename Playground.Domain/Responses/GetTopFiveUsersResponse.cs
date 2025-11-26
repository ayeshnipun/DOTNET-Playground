namespace Playground.Domain.Responses;

public record GetTopFiveUsersResponse
(
    int UserId,
    string UserName,
    int CountOfComments,
    GetPostsWithLikesResponse[] TopPostsWithLikes
);