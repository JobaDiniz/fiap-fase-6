using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iFood.Reviews.Api.Controllers
{
    [ApiController]
    [Route("store")]
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> logger;
        private readonly IStoreRepository storeRepository;

        public StoreController(ILogger<StoreController> logger, IStoreRepository storeRepository)
        {
            this.logger = logger;
            this.storeRepository = storeRepository;
        }

        [HttpGet]
        [Route("{id:guid}/review")]
        public async Task<IActionResult> GetReviews(Guid id, CancellationToken cancellation)
        {
            var store = await storeRepository.GetById(id, cancellation);
            return Ok(store);
        }
    }
}
