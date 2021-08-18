using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iFood.Reviews.Api.Controllers
{
    [ApiController]
    [Route("review")]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> logger;

        public ReviewController(ILogger<ReviewController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetPaged()
        {
            return Ok();
        }
    }
}
