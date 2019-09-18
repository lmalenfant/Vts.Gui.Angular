using NUnit.Framework;
using Vts.Gui.Angular.Controllers;

namespace Vts.Gui.Angular.Test.Controllers
{
    public class InverseControllerTests
    {
        [Test]
        public void test_controller_get()
        {
            var inverseController = new InverseController();
            var response = inverseController.Get();
            string[] array = { "Controller", "Inverse" };
            Assert.AreEqual(array, response);
        }
    }
}
