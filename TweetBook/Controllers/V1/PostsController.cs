using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TweetBook.Contracts.V1;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Domain;
using TweetBook.Services;

namespace TweetBook.Controllers.V1
{
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public ActionResult GetAl()
        {
            return Ok(_postService.GetAll());
        }
        
        [HttpGet(ApiRoutes.Posts.Get)]
        public ActionResult Get([FromRoute] Guid postId)
        {
            var post = _postService.GetById(postId);

            if (post == null)
                return NotFound();
            
            return Ok(post);
        }
        
        [HttpPut(ApiRoutes.Posts.Update)]
        public ActionResult Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };

            var updated = _postService.UpdatePost(post);

            if (updated)
                return Ok();
            
            return NotFound();
        }
        
        [HttpPost(ApiRoutes.Posts.Create)]
        public ActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post {Id = postRequest.Id};
            post.Id = Guid.NewGuid();
            _postService.GetAll().Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());
            
            return Created(locationUri, new PostResponse{Id = post.Id});
        }
    }
}