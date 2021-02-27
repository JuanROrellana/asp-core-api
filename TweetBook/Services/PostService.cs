using System;
using System.Collections.Generic;
using System.Linq;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class PostService: IPostService
    {
        private readonly List<Post> _posts;

        public PostService()
        {
            _posts = new List<Post>();
            for (int i = 0; i < 20; i++)
            {
                _posts.Add(new Post
                {
                    Id = Guid.NewGuid(),
                    Name = $"Post name {i}"
                });
            }
        }
        
        public List<Post> GetAll()
        {
            return _posts;
        }

        public Post GetById(Guid id)
        {
            return _posts.SingleOrDefault(x => x.Id == id);
        }

        public bool UpdatePost(Post postToUpdate)
        {
            var post = GetById(postToUpdate.Id) != null;
            if (!post)
                return false;
            var index = _posts.FindIndex(x => x.Id == postToUpdate.Id);
            _posts[index] = postToUpdate;

            return true;
        }
        
        public bool DeletePosst(Guid id)
        {
            var post = GetById(id);
            if (post == null)
                return false;

            _posts.Remove(post);
            return true;

        }
    }
}