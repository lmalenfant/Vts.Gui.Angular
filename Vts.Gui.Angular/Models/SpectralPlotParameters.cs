using Vts.Api.Enums;
using Vts.Common;
using Vts.SpectralMapping;

namespace Vts.Api.Models
{
    public class SpectralPlotParameters : IPlotParameters
    {
        // XAxis = Wavelength
        public DoubleRange XAxis { get; set; }
        public string YAxis {  get; set; }
        /// <summary>
        /// Spectral plot type = Mua or Musp
        /// </summary>
        public SpectralPlotType SpectralPlotType { get; set; }
        public string PlotType { get; set; }
        public string PlotName { get; set; }
        public Tissue Tissue { get; set; }
        public double[] Wavelengths { get; set; }
        public string TissueType { get; set; }
        public DoubleRange Range { get; set; }
        public string ScattererType { get; set; }
    }
}
