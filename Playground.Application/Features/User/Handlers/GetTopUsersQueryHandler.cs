using MediatR;
using Microsoft.EntityFrameworkCore;
using Playground.Application.Features.User.Queries;
using Playground.Domain.Common;
using Playground.Domain.Entities;
using Playground.Domain.Interfaces;
using Playground.Domain.Responses;
using Playground.Persistance;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Playground.Application.Features.User.Handlers;

public class GetTopUsersQueryHandler : IRequestHandler<GetTopUsersQuery, BaseResponse<List<GetTopFiveUsersResponse>>>
{
    private readonly IUserRepository _userRepository;
    private readonly PlaygroundDbContext _dbContext;

    public GetTopUsersQueryHandler(IUserRepository userRepository, PlaygroundDbContext dbContext)
    {
        _userRepository = userRepository;
        _dbContext = dbContext;
    }

    public async Task<BaseResponse<List<GetTopFiveUsersResponse>>> Handle(GetTopUsersQuery request, CancellationToken cancellationToken)
    {
        // Select the top 5 users who made the most comments in the last 7 days on posts in the ".NET" category.

        var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);

        // // posts which has comments on .NET Category on last 7 days
        // var dotNetCategoryPosts = _dbContext.Comments
        //     .Where(c => c.CreatedAt >= sevenDaysAgo)
        //     .Include(c => c.User)
        //     .Include(c => c.Post)
        //     .ThenInclude(p => p.Category)
        //     .Where(c => c.Post.Category.Id == 1) // assuming .NET category has Id = 1
        //     .GroupBy(c => c.User.Id)
        //     .Select(g => new GetTopFiveUsersResponse(
        //         g.Key,                                   // UserId
        //         g.First().User.Username,                 // UserName
        //         g.Count(),                               // CountOfComments
        //         g.GroupBy(c => c.Post.Id)
        //         .Select(pg => new GetPostsWithLikesResponse(
        //             pg.Key,                             // PostId
        //             pg.First().Post.Likes.Count         // LikesCount
        //         ))
        //         .OrderByDescending(p => p.LikesCount)
        //         .Take(5)
        //         .ToArray()                              // TopPostsWithLikes
        //     ));

        // var topUsers = await dotNetCategoryPosts
        //     .OrderByDescending(u => u.CountOfComments)
        //     .Take(5)
        //     .ToListAsync(cancellationToken);


        // Fetch all posts in a category
        var postsInReactCategory = _dbContext.Posts
            .Where(p => p.CategoryId == 1).ToListAsync(); // Assuming that React category is 1

        // Get usernames of all users who have posted at least once
        var userNames = _dbContext.Users.Where(u => u.Posts.Any()).Select(u => u.Username).ToListAsync();

        // Get comments created in the last 7 days
        var commentsCreatedInLastSevenDays = _dbContext.Comments.Where(c => c.CreatedAt >= sevenDaysAgo).ToList();

        // 2.Sorting & Pagination
        // Top 5 latest posts
        var topFiveLatestPosts = _dbContext.Posts.OrderByDescending(p => p.Id).Take(5).ToList();

        // Paginate comments for a post
        var paginatedCommentsForAPost = _dbContext.Comments
            .Where(c => c.PostId == 1)
            .OrderByDescending(c => c.CreatedAt)
            .Skip(10)
            .Take(5)
            .ToList();

        // 3.Aggregates
        // Count of comments per post
        var countOfCommentsPerPost = _dbContext.Posts
            .Include(p => p.Comments)
            .GroupBy(p => p.Id)
            .Select(g => new
            {
                PostId = g.Key,
                CommentCount = g.First().Comments.Count
            })
            .ToList();

        var commentCount = _dbContext.Comments
            .GroupBy(c => c.PostId)
            .Select(g => new
            {
                PostId = g.Key,
                Count = g.Count()
            }).ToList();

        // likes per post (more than 0 likes)
        var likesPerPost = _dbContext.Likes
            .GroupBy(l => l.PostId)
            .Select(g => new
            {
                PostId = g.Key,
                LikesCount = g.Count()
            }).ToList();

        // likes per post including 0 likes
        var likesPerPostWithZero = _dbContext.Posts
            .Select(g => new
            {
                PostId = g.Id,
                LikeCount = g.Likes.Count()
            })
            .ToList();

        // User with most comments
        var userWithMostComments = _dbContext.Users.OrderByDescending(u => u.Comments.Count).First();

        // 4.Complex Queries(Joins, Nested, Multiple Levels)

        // Posts with comments and the usernames who commented
        var postsWithCommentsAndUserNames = _dbContext.Posts
            .Where(p => p.Comments.Any())
            .Include(p => p.Comments)
                .ThenInclude(c => c.User)
            .Select(s => new
            {
                PostId = s.Id,
                Usernames = s.Comments.Select(c => c.User.Username).ToList()
            })
            .ToList();

        // Top 3 posts with most likes along with author
        var topThreePostsWithMostLikes = _dbContext.Posts
            .Include(p => p.User)
            .OrderByDescending(p => p.Likes.Count)
            .Take(3)
            .Select(s => new
            {
                PostId = s.Id,
                Author = s.User.Username
            })
            .ToList();

        // Users and the posts they commented on (distinct posts only)
        var usersAndPostsTheyCommented = _dbContext.Users.Select(u => new
        {
            u.Username,
            Posts = u.Comments.Select(c => c.PostId).Distinct().ToList()
        }).ToList();

        // 5.Multi - level Aggregates & Grouping

        // Top 5 users with most comments, including their top 3 posts by likes
        var topFiveUsersWithMostComments = _dbContext.Users
            .OrderByDescending(u => u.Comments.Count)
            .Take(5)
            .Select(u => new
            {
                UserId = u.Id,
                u.Username,
                CommentCount = u.Comments.Count,
                TopThreePostsByLikes = u.Posts
                    .OrderByDescending(p => p.Likes.Count)
                    .Take(3)
                    .Select(p => new
                    {
                        PostId = p.Id,
                        p.Content,
                        LikesCount = p.Likes.Count
                    }).ToList()
            })
            .ToList();

        // Most popular category (by total likes of posts)
        var mostPopularCategory = _dbContext.Posts
            .Include(p => p.Category)
            .OrderByDescending(p => p.Likes.Count)
            .Take(1)
            .Select(p => new
            {
                Category = p.CategoryId
            });

        var popularCategory = _dbContext.Categories
            .Select(c => new
            {
                Category = c.Id,
                TotalLikes = c.Posts.Sum(p => p.Likes.Count)
            })
            .OrderByDescending(x => x.TotalLikes)
            .FirstOrDefault();

        // Posts with no comments
        var postsWithNoComments = _dbContext.Posts
            .Where(p => !p.Comments.Any())
            .ToList();

        return BaseResponse<List<GetTopFiveUsersResponse>>.Ok(new List<GetTopFiveUsersResponse>());
    }
}

// Most popular category(by total likes of posts)
// Posts with no comments
// Users who liked their own posts
// Daily comment activity over the last month