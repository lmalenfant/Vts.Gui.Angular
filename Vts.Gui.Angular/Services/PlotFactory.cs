using System;


namespace Vts.Api.Services
{
    public class PlotFactory
    {
        public static string GetPlot(PlotType plotType, IPlotParameters plotParameters)
        {
            string msg = null;
            IPlotResultsService plotResultsService;
            switch (plotType)
            {
                case PlotType.SolutionDomain:
                    plotResultsService = new PlotSolutionDomainResultsService();
                    return plotResultsService.Plot((SolutionDomainPlotParameters)plotParameters);
                case PlotType.Spectral:
                    plotResultsService = new PlotSpectralResultsService();
                    return plotResultsService.Plot((SpectralPlotParameters)plotParameters);
            }
            return msg;
        }
    }
}
