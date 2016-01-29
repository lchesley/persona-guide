using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonaManager.Models;
using System.IO;

namespace PersonaManager.Tests.Models
{
    [TestClass]
    public class SkillModelTests
    {
        SkillModel model;

        [TestInitialize]
        [DeploymentItem(@"App_Data\", @"App_Data\")]
        public void Initialize()
        {
            StreamReader skillList = new StreamReader("App_Data\\SkillList.csv");
            model = new SkillModel(skillList);
        }

        [TestMethod]
        public void GetSkillList()
        {
            //Arrange                

            //Act
            var result = model.GetSkillList();

            //Assert
            Assert.IsNotNull(result); 
        }

        [TestMethod]
        public void GetSkillList_CountGreaterThanZero()
        {
            //Arrange       
            int actualCount = 0;

            //Act
            var result = model.GetSkillList();

            //Assert
            Assert.AreNotEqual(result.Count, actualCount);
        }
    }
}
