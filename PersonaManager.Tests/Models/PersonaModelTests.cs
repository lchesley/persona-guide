using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonaManager.Models;
using System.IO;

namespace PersonaManager.Tests.Models
{
    [TestClass]
    public class PersonaModelTests
    {
        PersonaModel model;

        [TestInitialize]
        [DeploymentItem(@"App_Data\", @"App_Data\")]
        public void Initialize()
        {
            StreamReader fusionGuide = new StreamReader("App_Data\\FusionGuide.csv");
            model = new PersonaModel(fusionGuide);            
        }

        [TestMethod]
        public void GetPersonaList()
        {
            //Arrange                

            //Act
            var result = model.GetPersonaList();

            //Assert
            Assert.IsNotNull(result);            
        }

        [TestMethod]
        public void GetPersonaList_CountGreaterThanZero()
        {
            //Arrange
            int actualCount = 0;
            
            //Act
            var result = model.GetPersonaList();

            //Assert
            Assert.AreNotEqual(result.Count, actualCount);              
        }
    }
}
