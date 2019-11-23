using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Vts.Api.Enums;
using Vts.Api.Factories;
using Vts.Api.Models;
using Vts.Api.Services;

namespace Vts.Api.Test.Services
{
    class SpectralServiceTests
    {
        private SpectralService spectralService;
        private ILoggerFactory factory;
        private ILogger<SpectralService> logger;
        private Mock<IPlotFactory> plotFactoryMock;

        [Test]
        public void Test_get_plot_data()
        {
            plotFactoryMock = new Mock<IPlotFactory>();
            plotFactoryMock.Setup(x => x.GetPlot(PlotType.Spectral, new SpectralPlotParameters())).Returns("");
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();
            factory = serviceProvider.GetService<ILoggerFactory>()
                .AddConsole();
            logger = factory.CreateLogger<SpectralService>();
            spectralService = new SpectralService(logger, plotFactoryMock.Object);
            var postData = "{\"plotType\":\"musp\",\"plotName\":\"μs'\",\"tissueType\":{\"value\":\"Skin\",\"display\":\"Skin\"},\"absorberConcentration\":[{\"label\":\"Hb\",\"value\":28.4,\"units\":\"μM\"},{\"label\":\"HbO2\",\"value\":22.4,\"units\":\"μM\"},{\"label\":\"H2O\",\"value\":0.7,\"units\":\"vol. frac.\"},{\"label\":\"Fat\",\"value\":0,\"units\":\"vol. frac.\"},{\"label\":\"Melanin\",\"value\":0.0051,\"units\":\"vol. frac.\"}],\"bloodConcentration\":{\"totalHb\":50.8,\"bloodVolume\":0.021844,\"stO2\":0.4409448818897638,\"visible\":true},\"scattererType\":{\"value\":\"PowerLaw\",\"display\":\"PowerLaw [A*λ^(-b)]\"},\"powerLaw\":{\"a\":1.2,\"b\":1.42,\"show\":true},\"intralipid\":{\"volumeFraction\":0.01,\"show\":false},\"mieParticle\":{\"particleRadius\":0.5,\"particleN\":1.4,\"mediumN\":1,\"volumeFraction\":0.01,\"show\":false},\"range\":{\"title\":\"Wavelength Range\",\"startLabel\":\"Begin\",\"startLabelUnits\":\"nm\",\"startValue\":650,\"endLabel\":\"End\",\"endLabelUnits\":\"nm\",\"endValue\":1000,\"numberLabel\":\"Number\",\"numberValue\":36}}";
            var results = spectralService.GetPlotData(JsonConvert.DeserializeObject<dynamic>(postData));
            Assert.IsNull(results);
        }
    }
}
