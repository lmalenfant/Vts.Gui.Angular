using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Vts.Api.Models;
using Vts.Api.Services;

namespace Vts.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InverseController : ControllerBase
    {
        private readonly IInverseSolverService _inverseSolverService;

        public InverseController(IInverseSolverService inverseSolverService)
        {
            _inverseSolverService = inverseSolverService;
        }

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
        public string Post([FromBody] SolutionDomainPlotParameters PlotParameters)
        {
            return _inverseSolverService.GetPlotData(PlotParameters);
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
