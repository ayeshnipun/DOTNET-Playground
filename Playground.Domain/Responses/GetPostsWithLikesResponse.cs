namespace Playground.Domain.Responses;

public record GetPostsWithLikesResponse
(
    int PostId,
    int LikesCount
);