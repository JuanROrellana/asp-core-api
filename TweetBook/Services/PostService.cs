using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TweetBook.Data;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class PostService: IPostService
    {
        private readonly DataContext _dataContext;

        public PostService(DataContext dataContext)
        {
             _dataContext = dataContext;
        }
        
        public async Task<List<Post>> GetAllAsync()
        {
            return await _dataContext.Posts.ToListAsync();
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            return await _dataContext.Posts.SingleOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<bool> CreatePostAsync(Post post)
        {
            await _dataContext.Posts.AddAsync(post);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UserOwnsPostAsync(Guid postId, string userId)
        {
            var post  = await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId);

            if (post == null)
                return false;

            if (post.UserId != userId)
                return false;

            return true;
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
             _dataContext.Posts.Update(postToUpdate);
             var update = await _dataContext.SaveChangesAsync();
            return update > 0;
        }
        
        public async Task<bool> DeletePostAsync(Guid id)
        {
            var post = await GetByIdAsync(id);
            if (post == null)
                return false;

            _dataContext.Posts.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}