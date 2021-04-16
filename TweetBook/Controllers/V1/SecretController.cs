using Microsoft.AspNetCore.Mvc;
using TweetBook.Filters;

namespace TweetBook.Controllers.V1
{
    [ApiKeyAuth]
    public class SecretController : ControllerBase
    {
        [HttpGet("test")]
        public string GetTest()
        {
            return "test";
        }
    }
}