﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Vts.Api.Services;
using Vts.Common;

namespace Vts.Api.Test.Services
{
    class InverseSolverServiceTests
    {
        private InverseSolverService inverseSolverService;
        private ILoggerFactory factory;
        private ILogger<InverseSolverService> logger;

        [Test]
        public void Test_get_plot_data()
        {
            //var serviceProvider = new ServiceCollection()
            //    .AddLogging()
            //    .BuildServiceProvider();
            //factory = serviceProvider.GetService<ILoggerFactory>()
            //    .AddConsole();
            //logger = factory.CreateLogger<InverseSolverService>();
            //inverseSolverService = new InverseSolverService();
            //    // solutionDomain: SolutionDomain = { value: 'ROfRho' };
            //string results = inverseSolverService.GetPlotData("{"solutionDomain"='ROfRho'}");
            //Assert.AreEqual("", results);
        }
    }
}