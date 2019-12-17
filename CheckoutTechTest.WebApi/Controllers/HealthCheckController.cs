using Microsoft.AspNetCore.Mvc;

namespace CheckoutTechTest.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
