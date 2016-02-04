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
        SkillModel skillModel;
        InheritanceModel inheritanceModel;

        [TestInitialize]
        [DeploymentItem(@"App_Data\", @"App_Data\")]
        public void Initialize()
        {
            StreamReader fusionGuide = new StreamReader("App_Data\\FusionGuide.csv");
            StreamReader skillList = new StreamReader("App_Data\\SkillList.csv");

            skillModel = new SkillModel(skillList);
            inheritanceModel = new InheritanceModel();
            model = new PersonaModel(fusionGuide, skillModel, inheritanceModel);            
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
