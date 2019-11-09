using NUnit.Framework;
using Vts.Api.Controllers;

namespace Vts.Api.Test.Controllers
{
    public class SpectralControllerTests
    {
        [Test]
        public void Test_controller_get()
        {
            var spectralController = new SpectralController();
            var response = spectralController.Get();
            string[] array = { "Controller", "Spectral" };
            Assert.AreEqual(array, response);
        }
    }
}
