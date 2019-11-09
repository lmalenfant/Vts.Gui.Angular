using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Vts.Api.Services;

namespace Vts.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InverseController : ControllerBase
    {
        // GET: api/v1/Inverse
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Controller", "Inverse" };
        }

        // GET: api/v1/Inverse/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/v1/Inverse
        [HttpPost]
        public string Post([FromBody] dynamic value)
        {
            var inverseSolverService = new InverseSolverService();
            return inverseSolverService.GetPlotData(value);
        }

        // PUT: api/v1/Inverse/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/v1/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
