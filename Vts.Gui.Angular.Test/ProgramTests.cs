﻿using NUnit.Framework;
using Vts.Gui.Angular;

namespace Vts.Api.Tests
{
    class ProgramTests
    {
        [Test]
        public void Test_create_web_host_builder()
        {
            string[] args = { };
            var webhost = Program.CreateWebHostBuilder(args);
            Assert.IsNotNull(webhost);
        }
    }
}
