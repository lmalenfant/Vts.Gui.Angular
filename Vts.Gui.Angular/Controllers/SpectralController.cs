using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Vts.Gui.Angular.Services;

namespace Vts.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SpectralController : ControllerBase
    {
        // GET: api/v1/Spectral
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Controller", "Spectral" };
        }

        // POST: api/v1/Spectral
        [HttpPost]
        public string Post([FromBody] dynamic value)
        {
            var spectralService = new SpectralService();
            return spectralService.GetPlotData(value);
        }
    }
}
