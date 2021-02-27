using Microsoft.AspNetCore.Mvc;

namespace TweetBook.Controllers
{
    public class TestController: ControllerBase
    {

        [HttpGet("api/user")]
        public ActionResult Test()
        {
            return Ok(new {name = "Juan Ramirez"});
        }
        
    }
}