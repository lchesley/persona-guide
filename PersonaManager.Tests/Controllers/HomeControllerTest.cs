using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonaManager;
using PersonaManager.Controllers;
using PersonaManager.Models;

namespace PersonaManager.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        PersonaRepository repository;

        [TestInitialize]        
        public void Initialize()
        {
            repository = new PersonaRepository();
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }        
    }
}
