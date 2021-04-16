using System;
using System.Threading.Tasks;

namespace TweetBook.Services
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeTimeLive);
        Task<string> GetCacheResponseAsync(string cacheKey);
    }
}