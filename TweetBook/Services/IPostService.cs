using System;
using System.Collections.Generic;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public interface IPostService
    {
        List<Post> GetAll();
        Post GetById(Guid id);
        bool UpdatePost(Post postToUpdate);
    }
}