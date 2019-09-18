using NUnit.Framework;
using Vts.Gui.Angular.Controllers;

namespace Vts.Gui.Angular.Test.Controllers
{
    public class ForwardControllerTests
    {
        [Test]
        public void test_controller_get()
        {
            var forwardController = new ForwardController();
            var response = forwardController.Get();
            string[] array = { "Controller", "Forward" };
            Assert.AreEqual(array, response);
        }
    }
}
