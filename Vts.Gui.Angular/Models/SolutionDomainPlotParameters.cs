using System.Collections.Generic;
using Vts.Common;

namespace Vts.Api.Models
{
    public class SolutionDomainPlotParameters : IPlotParameters
    {
        public ForwardSolverType ForwardSolverType { get; set; }
        public string ForwardSolverEngine { get; set; }
        public string InverseSolverEngine { get; set; }
        public string SolutionDomain { get; set; }
        public DoubleRange XAxis { get; set; }
        public OpticalProperties OpticalProperties { get; set; }
        public string OptimizerType { get; set; }
        public string OptimizationParameters { get; set; }
        public IndependentAxes IndependentAxes { get; set; }
        public double NoiseValue { get; set; }
        public DoubleRange Range { get; set; }
        public string ModelAnalysis { get; set; }
        public List<double[]> MeasuredData { get; set; }
    }
}
