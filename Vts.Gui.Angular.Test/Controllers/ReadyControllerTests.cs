using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Vts.Gui.Angular.Controllers;

namespace Vts.Gui.Angular.Test.Controllers
{
    class ReadyControllerTests
    {
        [Test]
        public void test_controller_get()
        {
            var readyController = new ReadyController();
            readyController.ControllerContext = new ControllerContext {
                HttpContext = new DefaultHttpContext()
            };
            var response = readyController.Get();
            Assert.AreEqual("200 OK", response);
            Assert.AreEqual(200, readyController.HttpContext.Response.StatusCode);
        }
    }
}
