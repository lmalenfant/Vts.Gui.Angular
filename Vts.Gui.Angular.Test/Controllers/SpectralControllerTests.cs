using NUnit.Framework;
using Vts.Gui.Angular.Controllers;

namespace Vts.Gui.Angular.Test.Controllers
{
    public class SpectralControllerTests
    {
        [Test]
        public void test_controller_get()
        {
            var spectralController = new SpectralController();
            var response = spectralController.Get();
            string[] array = { "Controller", "Spectral" };
            Assert.AreEqual(response, array);
        }
    }
}
