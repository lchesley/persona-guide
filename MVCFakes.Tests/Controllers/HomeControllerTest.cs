using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCFakes.Controllers;
using MVCFakes;

namespace MVCFakes.Controllers
{

    [TestClass]
    public class HomeControllerTest
    {

        [TestMethod]
        public void TestFakeFormParams()
        {
            // Create controller
            var controller = new HomeController();

            // Create fake controller context
            var formParams = new NameValueCollection { { "firstName", "Stephen" }, {"lastName", "Walther"} };
            controller.ControllerContext = new FakeControllerContext(controller, formParams);

            // Act
            var result = controller.Insert() as ViewResult;
            Assert.AreEqual("Stephen", result.ViewData["firstName"]);
            Assert.AreEqual("Walther", result.ViewData["lastName"]); 
            Assert.AreEqual(formParams.Count, result.ViewData["count"]);
        }



        [TestMethod]
        public void TestFakeQueryStringParams()
        {
            // Create controller
            var controller = new HomeController();

            // Create fake controller context
            var queryStringParams = new NameValueCollection { { "key1", "a" }, { "key2", "b" } };
            controller.ControllerContext = new FakeControllerContext(controller, null, queryStringParams);

            // Act
            var result = controller.Details() as ViewResult;
            Assert.AreEqual("a", result.ViewData["key1"]);
            Assert.AreEqual("b", result.ViewData["key2"]);
            Assert.AreEqual(queryStringParams.Count, result.ViewData["count"]);
        }



        [TestMethod]
        public void TestFakeUser()
        {
            // Create controller
            var controller = new HomeController();

            // Check what happens for authenticated user
            controller.ControllerContext = new FakeControllerContext(controller, "Stephen");
            var result = controller.Secret() as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewDataDictionary viewData = ((ViewResult) result).ViewData;
            Assert.AreEqual("Stephen", viewData["userName"]);

            // Check what happens for anonymous user
            controller.ControllerContext = new FakeControllerContext(controller);            
            result = controller.Secret() as ActionResult;
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }


        [TestMethod]
        public void TestFakeUserRoles()
        {
            // Create controller
            var controller = new HomeController();

            // Check what happens for Admin user
            controller.ControllerContext = new FakeControllerContext(controller, "Stephen", new string[] {"Admin"});
            var result = controller.Admin() as ActionResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            // Check what happens for anonymous user
            controller.ControllerContext = new FakeControllerContext(controller);
            result = controller.Admin() as ActionResult;
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }



        [TestMethod]
        public void TestCookies()
        {
            // Create controller
            var controller = new HomeController();

            // Create fake Controller Context
            var cookies = new HttpCookieCollection();
            cookies.Add( new HttpCookie("key", "a"));
            controller.ControllerContext = new FakeControllerContext(controller, cookies);
            var result = controller.TestCookie() as ViewResult;

            // Assert
            Assert.AreEqual("a", result.ViewData["key"]);
        }

        [TestMethod]
        public void TestSessionState()
        {
            // Create controller
            var controller = new HomeController();

            // Create fake Controller Context
            var sessionItems = new SessionStateItemCollection();
            sessionItems["item1"] = "wow!";
            controller.ControllerContext = new FakeControllerContext(controller, sessionItems);
            var result = controller.TestSession() as ViewResult;

            // Assert
            Assert.AreEqual("wow!", result.ViewData["item1"]);

            // Assert
            Assert.AreEqual("cool!", controller.HttpContext.Session["item2"]);
        }


    }
}
