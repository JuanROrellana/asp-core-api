using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TweetBook.Contracts.V1;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;
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
        
        [HttpPost(ApiRoutes.Posts.Create)]
        public ActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post {Id = postRequest.Id};
            post.Id = Guid.NewGuid();
            _posts.Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());
            
            return Created(locationUri, new PostResponse{Id = post.Id});
        }
    }
}