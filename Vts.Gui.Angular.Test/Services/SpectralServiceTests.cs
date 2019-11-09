using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Vts.Api.Services;

namespace Vts.Api.Test.Services
{
    class SpectralServiceTests
    {
        private SpectralService spectralService;
        private ILoggerFactory factory;
        private ILogger<SpectralService> logger;

        [Test]
        public void Test_get_plot_data()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();
            factory = serviceProvider.GetService<ILoggerFactory>()
                .AddConsole();
            logger = factory.CreateLogger<SpectralService>();
            spectralService = new SpectralService(logger);
            var results = spectralService.GetPlotData("{ }");
            Assert.AreEqual("", results);
        }
    }
}
