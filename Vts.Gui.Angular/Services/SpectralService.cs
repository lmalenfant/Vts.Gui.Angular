using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Vts.Api.Data;
using Vts.Common;

namespace Vts.Api.Services
{
    public class SpectralService : ISpectralService
    {
        private readonly ILogger<SpectralService> _logger;

        public SpectralService(ILogger<SpectralService> logger)
        {
            _logger = logger;
        }

        public string GetPlotData(dynamic values)
        {
            _logger.LogInformation("Get the plot data for the Spectral Panel");
            dynamic spectralSettings = values;

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
            var scatterType = spectralSettings["scattererType"].value;
            SpectralMapping.IScatterer scatterer;
            switch (scatterType)
            {
                case "PowerLaw":
                    scatterer = new SpectralMapping.PowerLawScatterer(spectralSettings["powerLaw"].a, spectralSettings["powerLaw"].b);
                    break;
                case "Intralipid":
                    scatterer = new SpectralMapping.IntralipidScatterer(spectralSettings["intralipid"].volumeFraction);
                    break;
                case "Mie":
                    scatterer = new SpectralMapping.MieScatterer(spectralSettings["mieParticle"].particleRadius, spectralSettings["mieParticle"].particleN, spectralSettings["mieParticle"].particlemediumN, spectralSettings["mieParticle"].volumeFraction);
                    break;
                default:
                scatterer = new SpectralMapping.PowerLawScatterer();
                break;
            }

            // get the wavelength
            var wavelengths = new DoubleRange((double)spectralSettings["range"]["startValue"],
                                (double)spectralSettings["range"]["endValue"], (int)spectralSettings["range"]["numberValue"]);
            var wvs = wavelengths.AsEnumerable().ToArray();
            // set up the tissue
            var tissue = new SpectralMapping.Tissue(chromophoreAbsorbers, scatterer, (string)spectralSettings["tissueType"].value);

            List<Point> xyPoints = new List<Point>();
            PlotData plotData;
            Plots plot;

            var plotType = (string)spectralSettings["plotType"];

            foreach (var wv in wvs)
            {
                if (plotType == "μa")
                {
                    xyPoints.Add(new Point(wv, tissue.GetMua(wv)));
                }
                else
                {
                    xyPoints.Add(new Point(wv, tissue.GetMusp(wv)));
                }
            }
            plotData = new PlotData { Data = xyPoints, Label = spectralSettings["tissueType"].value };
            plot = new Plots {
                Id = "Spectral",
                Detector = "Spectral",
                Legend = "Spectral",
                XAxis = "λ",
                YAxis = plotType,
                PlotList = new List<PlotDataJson>()
            };
            plot.PlotList.Add(new PlotDataJson {
                Data = plotData.Data.Select(item => new List<double> { item.X, item.Y }).ToList(),
                Label = spectralSettings["tissueType"].value + " " + plotType
            });
            return JsonConvert.SerializeObject(plot);
        }
    }
}
