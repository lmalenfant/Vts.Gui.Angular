using Microsoft.Extensions.Logging;

namespace Vts.Api.Services
{
    public class SpectralService : ISpectralService
    {
        private readonly ILogger<SpectralService> _logger;

        public SpectralService(ILogger<SpectralService> logger)
        {
            _logger = logger;
        }

        public string GetPlotData(dynamic values)
        {
            _logger.LogInformation("Get the plot data for the Spectral Panel");
            return "";
        }
    }
}
