using NUnit.Framework;
using Vts.Gui.Angular.Controllers;

namespace Vts.Gui.Angular.Test.Controllers
{
    class LiveControllerTests
    {
        [Test]
        public void test_controller_get()
        {
            var liveController = new LiveController();
            var response = liveController.Get();
            Assert.AreEqual(response, "200 OK");
        }
    }
}
