namespace Playground.Domain.DTOs;

public record GetPostsWithLikesDto
(
    int PostId,
    int LikesCount
);