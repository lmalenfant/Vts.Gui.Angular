using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Vts.Api.Services;

namespace Vts.Gui.Angular.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ForwardController : ControllerBase
    {
        // GET: api/v1/Forward
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Controller", "Forward" };
        }

        // GET: api/v1/Forward/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/v1/Forward
        [HttpPost]
        public string Post([FromBody] dynamic value)
        {
            var forwardSolverService = new ForwardSolverService();
            return forwardSolverService.GetPlotData(value);
        }
    }
}
