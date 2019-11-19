using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Vts.Api.Services
{
    public class PlotFactory : IPlotFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PlotFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string GetPlot(PlotType plotType, IPlotParameters plotParameters)
        {
            switch (plotType)
            {
                case PlotType.SolutionDomain:
                    var plotSolutionDomainResultsService = _serviceProvider.GetService<PlotSolutionDomainResultsService>();
                    return plotSolutionDomainResultsService.Plot((SolutionDomainPlotParameters)plotParameters);
                case PlotType.Spectral:
                    var plotSpectralResultsService = _serviceProvider.GetService<PlotSpectralResultsService>();
                    return plotSpectralResultsService.Plot((SpectralPlotParameters)plotParameters);
                default:
                    return null;
            }
        }
    }
}
