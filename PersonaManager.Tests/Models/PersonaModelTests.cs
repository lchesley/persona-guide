using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonaManager.Models;

namespace PersonaManager.Tests.Models
{
    [TestClass]
    public class PersonaModelTests
    {
        PersonaModel model;

        [TestInitialize]        
        public void Initialize()
        {            
            model = new PersonaModel();
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
