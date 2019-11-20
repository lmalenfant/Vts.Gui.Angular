using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Vts.Common;

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

        public string GetPlotData(dynamic values)
        {
            _logger.LogInformation("Get the plot data for the Spectral Panel");

            dynamic spectralSettings = values;

            var plotType = (string)spectralSettings["plotType"];
            var plotParameters = new SpectralPlotParameters();
            plotParameters.PlotType = Enum.Parse<SpectralPlotType>(plotType.ToString(), true);
            plotParameters.TissueType = (string)spectralSettings["tissueType"].value;
            plotParameters.PlotName = (string)spectralSettings["plotName"];
            plotParameters.YAxis = plotParameters.PlotName;

            // set up the absorber concentrations
            var absorberConcentrations = spectralSettings["absorberConcentration"];
            List<SpectralMapping.IChromophoreAbsorber> chromophoreAbsorbers = new List<SpectralMapping.IChromophoreAbsorber>();
            foreach (var absorber in absorberConcentrations)
            {
                var chromophoreTypes = EnumHelper.GetValues<ChromophoreType>();
                foreach (var type in chromophoreTypes)
                {
                    if (type.ToString() == (string)absorber.label)
                    {
                        chromophoreAbsorbers.Add(new SpectralMapping.ChromophoreAbsorber(type, (double)absorber.value));
                    }
                }
            }

            // set up the scatterer
            var scatterType = (string)spectralSettings["scattererType"].value;
            SpectralMapping.IScatterer scatterer;
            switch (scatterType)
            {
                case "PowerLaw":
                    scatterer = new SpectralMapping.PowerLawScatterer((double)spectralSettings["powerLaw"].a, (double)spectralSettings["powerLaw"].b);
                    break;
                case "Intralipid":
                    scatterer = new SpectralMapping.IntralipidScatterer((double)spectralSettings["intralipid"].volumeFraction);
                    break;
                case "Mie":
                    scatterer = new SpectralMapping.MieScatterer((double)spectralSettings["mieParticle"].particleRadius, (double)spectralSettings["mieParticle"].particleN, (double)spectralSettings["mieParticle"].particlemediumN, (double)spectralSettings["mieParticle"].volumeFraction);
                    break;
                default:
                scatterer = new SpectralMapping.PowerLawScatterer();
                break;
            }

            // get the wavelength
            plotParameters.XAxis = new DoubleRange((double)spectralSettings["range"]["startValue"],
                                (double)spectralSettings["range"]["endValue"], (int)spectralSettings["range"]["numberValue"]);
            plotParameters.Wavelengths = plotParameters.XAxis.AsEnumerable().ToArray();
            // set up the tissue
            plotParameters.Tissue = new SpectralMapping.Tissue(chromophoreAbsorbers, scatterer, (string)spectralSettings["tissueType"].value);

            return _plotFactory.GetPlot(PlotType.Spectral, plotParameters);
        }
    }
}
