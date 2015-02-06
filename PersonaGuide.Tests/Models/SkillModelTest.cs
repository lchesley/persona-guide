using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using PersonaGuide.Models;

namespace PersonaManager.Tests.Models
{
    [TestClass]
    public class SkillModelTest
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
        public void GetSkillBySkillName()
        {            
            var result = model.GetSkillBySkillName("Impure Reach");

            Assert.IsNotNull(result);
        }

        [TestMethod]        
        public void GetSkillBySkillName_NotFound()
        {         
            var result = model.GetSkillBySkillName("Invalid Value");

            Assert.IsNull(result);
        }

        [TestMethod]        
        public void BuildSkillLevelsFromSkillList()
        {            
            var result = model.BuildSkillLevelsFromSkillList("Impure Reach, Circle Recovery(25)");

            Assert.IsNotNull(result);
        }      
    }
}
