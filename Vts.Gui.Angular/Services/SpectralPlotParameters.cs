
using Vts.Common;

namespace Vts.Api.Services
{
    public class SpectralPlotParameters : IPlotParameters
    {
        // XAxis = Wavelength
        public DoubleRange XAxis { get; set; }
        /// <summary>
        /// Spectral plot type = Mua or Musp
        /// </summary>
        public SpectralPlotType PlotType { get; set; }
        // other parameters to be added
    }
}
