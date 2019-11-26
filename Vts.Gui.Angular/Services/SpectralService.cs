using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Vts.Api.Enums;
using Vts.Api.Factories;
using Vts.Api.Models;
using Vts.Common;
using Vts.SpectralMapping;

namespace Vts.Api.Services
{
    public class SpectralService : ISpectralService
    {
        private readonly ILogger<SpectralService> _logger;
        private readonly IPlotFactory _plotFactory;

        public SpectralService(ILogger<SpectralService> logger, IPlotFactory plotFactory)
        {
            _logger = logger;
            _plotFactory = plotFactory;
        }

        public string GetPlotData(SpectralPlotParameters plotParameters)
        {
            _logger.LogInformation("Get the plot data for the Spectral Panel");

            plotParameters.SpectralPlotType = Enum.Parse<SpectralPlotType>(plotParameters.PlotType, true);
            plotParameters.YAxis = plotParameters.PlotName;

            // set up the absorber concentrations
            List<IChromophoreAbsorber> chromophoreAbsorbers = new List<IChromophoreAbsorber>();
            foreach (var absorber in plotParameters.AbsorberConcentration)
            {
                var chromophoreTypes = EnumHelper.GetValues<ChromophoreType>();
                foreach (var type in chromophoreTypes)
                {
                    if (type.ToString() == (string)absorber.label)
                    {
                        chromophoreAbsorbers.Add(new ChromophoreAbsorber(type, (double)absorber.value));
                    }
                }
            }

            // set up the scatterer
            plotParameters.ScatteringType = Enum.Parse<ScatteringType>(plotParameters.ScattererType, true);
            IScatterer scatterer;
            switch (plotParameters.ScatteringType)
            {
                case ScatteringType.PowerLaw:
                    scatterer = plotParameters.PowerLawScatterer;
                    break;
                case ScatteringType.Intralipid:
                    scatterer = plotParameters.IntralipidScatterer;
                    break;
                case ScatteringType.Mie:
                    scatterer = plotParameters.MieScatterer;
                    break;
                default:
                scatterer = new PowerLawScatterer();
                break;
            }

            // get the wavelength
            plotParameters.XAxis = plotParameters.Range;
            plotParameters.Wavelengths = plotParameters.XAxis.AsEnumerable().ToArray();
            // set up the tissue
            plotParameters.Tissue = new Tissue(chromophoreAbsorbers, scatterer, plotParameters.TissueType);

            return _plotFactory.GetPlot(PlotType.Spectral, plotParameters);
        }
    }
}
