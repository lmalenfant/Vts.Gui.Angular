using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Vts.Api.Services;

namespace Vts.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IForwardSolverService _forwardSolverService;

        public ValuesController(IForwardSolverService forwardSolverService)
        {
            _forwardSolverService = forwardSolverService;
        }

        // GET api/v1/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/v1/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/v1/values
        [HttpPost]
        public string Post([FromBody] dynamic value)
        {
            return _forwardSolverService.GetPlotData(value);
        }
    }
}
