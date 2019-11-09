using NUnit.Framework;
using Vts.Api.Controllers;

namespace Vts.Api.Test.Controllers
{
    public class InverseControllerTests
    {
        [Test]
        public void Test_controller_get()
        {
            var inverseController = new InverseController();
            var response = inverseController.Get();
            string[] array = { "Controller", "Inverse" };
            Assert.AreEqual(array, response);
        }
    }
}
