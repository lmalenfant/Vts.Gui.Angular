using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Vts.Api.Models;
using Vts.Api.Services;

namespace Vts.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ForwardController : ControllerBase
    {
        private readonly IForwardSolverService _forwardSolverService;

        public ForwardController(IForwardSolverService forwardSolverService)
        {
            _forwardSolverService = forwardSolverService;
        }

        // GET: api/v1/Forward
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Controller", "Forward" };
        }

        // POST: api/v1/Forward
        [HttpPost]
        public string Post([FromBody] SolutionDomainPlotParameters PlotParameters)
        {
            return _forwardSolverService.GetPlotData(PlotParameters);
        }
    }
}
