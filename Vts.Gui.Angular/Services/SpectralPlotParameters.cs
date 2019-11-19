
using Vts.Common;
using Vts.SpectralMapping;

namespace Vts.Api.Services
{
    public class SpectralPlotParameters : IPlotParameters
    {
        // XAxis = Wavelength
        public DoubleRange XAxis { get; set; }
        public string YAxis {  get; set; }
        /// <summary>
        /// Spectral plot type = Mua or Musp
        /// </summary>
        public SpectralPlotType PlotType { get; set; }
        public string PlotName { get; set; }
        public Tissue Tissue { get; set; }
        public double[] Wavelengths { get; set; }
        public string TissueType {  get; set; }
    }
}
