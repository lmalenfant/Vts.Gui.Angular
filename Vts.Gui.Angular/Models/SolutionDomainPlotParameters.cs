using Vts.Common;

namespace Vts.Api.Models
{
    public class SolutionDomainPlotParameters : IPlotParameters
    {
        public ForwardSolverType ForwardSolverType { get; set; }
        public string SolutionDomain { get; set; }
        public DoubleRange XAxis { get; set; }
        public OpticalProperties OpticalProperties { get; set; }
        public string IndependentAxis { get; set; }
        public double IndependentValue { get; set; }
        public double Noise { get; set; }
    }
}
