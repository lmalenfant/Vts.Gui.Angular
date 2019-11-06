using System;
using Vts.Common;

namespace Vts.Api.Services
{
    public class ForwardSolverService : IForwardSolverService
    {
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
                msg = PlotResultsService.PlotBasedOnSolutionDomain(
                    Enum.Parse(typeof(ForwardSolverType), fs.ToString()),
                    sd.Value,
                    xaxis,
                    op,
                    independentAxis.Value,
                    independentValue,
                    noise);
                return msg;
            }
            catch (Exception e)
            {
                throw new Exception("Error during action: " + e.Message);
            }
        }
    }
}
