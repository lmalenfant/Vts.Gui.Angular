using Microsoft.AspNetCore.Mvc;

namespace Vts.Gui.Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiveController : ControllerBase
    {
        // GET: api/Live
        [HttpGet]
        public string Get()
        {
            return "200 OK";
        }
    }
}
