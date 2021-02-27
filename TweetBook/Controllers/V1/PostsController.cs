using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TweetBook.Contracts.V1;
using TweetBook.Domain;

namespace TweetBook.Controllers.V1
{
    public class PostsController : ControllerBase
    {
        private readonly List<Post> _posts;
        
        public PostsController()
        {
            _posts = new List<Post>();
            for (int i = 0; i < 20; i++)
            {
                _posts.Add(new Post
                {
                    Id = Guid.NewGuid()
                });
            }
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public ActionResult GetAl()
        {
            return Ok(_posts);
        }
            
        
    }
}