using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;

namespace TweetBook.Sdk
{
    [Headers("Authorization: Bearer")]
    public interface IPostApi
    {
        [Get("/api/v1/posts")]
        Task<ApiResponse<List<PostResponse>>> GetPostsAsync();
        
        [Get("/api/v1/posts/{postId}")]
        Task<ApiResponse<List<PostResponse>>> GetPostAsync(Guid postId);
    }
}