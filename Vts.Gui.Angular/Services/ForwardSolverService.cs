using System;
using Microsoft.Extensions.Logging;
using Vts.Api.Enums;
using Vts.Api.Factories;
using Vts.Api.Models;
using Vts.Common;

namespace Vts.Api.Services
{
    public class ForwardSolverService : IForwardSolverService
    {
        private readonly ILogger<ForwardSolverService> _logger;
        private readonly IPlotFactory _plotFactory;

        public ForwardSolverService(ILogger<ForwardSolverService> logger, IPlotFactory plotFactory)
        {
            _logger = logger;
            _plotFactory = plotFactory;
        }

        public string GetPlotData(dynamic values)
        {
            try
            {
                dynamic vtsSettings = values;
                var sd = vtsSettings["solutionDomain"];
                var fs = vtsSettings["forwardSolverEngine"];
                var msg = "";
                var op = new OpticalProperties((double)vtsSettings["opticalProperties"]["mua"], (double)vtsSettings["opticalProperties"]["musp"], (double)vtsSettings["opticalProperties"]["g"], (double)vtsSettings["opticalProperties"]["n"]);
                var xaxis = new DoubleRange((double) vtsSettings["range"]["startValue"],
                    (double) vtsSettings["range"]["endValue"], (int) vtsSettings["range"]["numberValue"]);
                //var independentValues = xaxis.AsEnumerable().ToArray();
                var noise = (double)vtsSettings["noiseValue"];
                var independentAxis = vtsSettings["independentAxes"]["label"];
                var independentValue = (double)vtsSettings["independentAxes"]["value"];
                var plotParameters = new SolutionDomainPlotParameters();
                //msg = PlotResultsService.Plot(
                plotParameters.ForwardSolverType = Enum.Parse(typeof(ForwardSolverType), fs.ToString());
                plotParameters.SolutionDomain = sd.Value;
                plotParameters.XAxis = xaxis;
                plotParameters.OpticalProperties = op;
                plotParameters.IndependentAxis = independentAxis.Value;
                plotParameters.IndependentValue = independentValue;
                plotParameters.Noise = noise;
                msg = _plotFactory.GetPlot(PlotType.SolutionDomain, plotParameters);
                return msg;
            }
            catch (Exception e)
            {
                throw new Exception("Error during action: " + e.Message);
            }
        }
    }
}
