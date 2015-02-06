using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using PersonaGuide.Models;
using PersonaGuide.Entities;

namespace PersonaManager.Tests.Models
{
    [TestClass]
    public class PersonaModelTest
    {
        SkillModel skillModel;
        PersonaModel model;

        [TestInitialize]
        [DeploymentItem(@"App_Data\", @"App_Data\")]
        public void Initialize()
        {
            StreamReader fusionGuide = new StreamReader("App_Data\\FusionGuide.csv");
            StreamReader skillList = new StreamReader("App_Data\\SkillList.csv");            

            skillModel = new SkillModel(skillList);
            model = new PersonaModel(fusionGuide, skillModel);            
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
        public void GetPersonaListByArcana()
        {
            //Arrange            
            
            //Act
            var result = model.GetPersonaList(Arcana.Chariot);

            //Assert
            Assert.IsNotNull(result);            
        }        

        [TestMethod]        
        public void GetPersonaListByName()
        {
            //Arrange            

            //Act
            var result = model.GetPersonaByName("Gozuki");

            //Assert
            Assert.IsNotNull(result);            
        }

        [TestMethod]
        public void GetPersonaListBySkillName()
        {
            //Arrange                        
            Skill skill = skillModel.GetSkillBySkillName("Golden Link");

            //Act
            var result = model.GetPersonaList(skill);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]        
        public void GetPersonaListByName_NotFound()
        {
            //Arrange            

            //Act
            var result = model.GetPersonaByName("Invalid");

            //Assert
            Assert.IsNull(result);
        }    
    }
}
