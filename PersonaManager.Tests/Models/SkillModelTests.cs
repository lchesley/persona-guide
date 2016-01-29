using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonaManager.Models;
using System.IO;
using PersonaManager.Entities;
using System.Collections.Generic;

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

        [TestMethod]
        public void GetSkillBySkillName()
        {
            //Arrange
            string skillName = "Dia";
            Skill fetchedSkill = new Skill();

            //Act
            fetchedSkill = model.GetSkillBySkillName(skillName);

            //Assert
            Assert.IsNotNull(fetchedSkill);
        }

        [TestMethod]
        public void GetSkillBySkillName_NotFound()
        {
            //Arrange
            string skillName = "Incorrect";
            Skill fetchedSkill = new Skill();

            //Act
            fetchedSkill = model.GetSkillBySkillName(skillName);

            //Assert
            Assert.IsNull(fetchedSkill);
        }

        [TestMethod]
        public void GetSkillBySkillName_CheckReturnedValuesAreCorrect()
        {            
            //Arrange
            string skillName = "Twin Slash";

            Skill twinslash = new Skill();
            twinslash.CanPassDown = true;
            twinslash.Cost = "18 HP";
            twinslash.Description = "Deals light Cut damage to one foe (2 hits)";
            twinslash.Name = "Twin Slash";
            twinslash.SkillType = SkillInheritanceType.Physical;
            twinslash.Type = "Cut";
            
            Skill fetchedSkill = new Skill();

            //Act
            fetchedSkill = model.GetSkillBySkillName(skillName);

            //Assert
            Assert.AreEqual(twinslash.CanPassDown, fetchedSkill.CanPassDown);
            Assert.AreEqual(twinslash.Cost, fetchedSkill.Cost);
            Assert.AreEqual(twinslash.Description, fetchedSkill.Description);
            Assert.AreEqual(twinslash.Name, fetchedSkill.Name);
            Assert.AreEqual(twinslash.SkillType, fetchedSkill.SkillType);
            Assert.AreEqual(twinslash.Type, fetchedSkill.Type);
        }

        [TestMethod]
        public void GetLearnedSkillsFromSkillList()
        {
            //Arrange
            string skillList = "Dia, Zio, Patra(3), Pulinpa(4), Clairvoyance(5)";
            List<LearnedSkill> learnedSkills = new List<LearnedSkill>();

            //Act
            learnedSkills = model.GetLearnedSkillsFromSkillList(skillList);

            //Assert
            Assert.IsNotNull(learnedSkills);
        }

        [TestMethod]
        public void GetLearnedSkillsFromSkillList_CountGreaterThanZero()
        {
            //Arrange
            string skillList = "Dia, Zio, Patra(3), Pulinpa(4), Clairvoyance(5)";
            List<LearnedSkill> learnedSkills = new List<LearnedSkill>();
            int actualCount = 0;

            //Act
            learnedSkills = model.GetLearnedSkillsFromSkillList(skillList);

            //Assert
            Assert.AreNotEqual(learnedSkills.Count, actualCount);
        }

        [TestMethod]
        public void GetLearnedSkillsFromSkillList_CheckReturnedValuesAreCorrect()
        {            
            //Arrange
            string skillList = "Dia, Zio, Patra(3), Pulinpa(4), Clairvoyance(5)";

            List<LearnedSkill> actualSkills = new List<LearnedSkill>();
            actualSkills.Add(new LearnedSkill { Skill = model.GetSkillBySkillName("Dia"), LevelLearned = 1 });
            actualSkills.Add(new LearnedSkill { Skill = model.GetSkillBySkillName("Zio"), LevelLearned = 1 });
            actualSkills.Add(new LearnedSkill { Skill = model.GetSkillBySkillName("Patra"), LevelLearned = 3 });
            actualSkills.Add(new LearnedSkill { Skill = model.GetSkillBySkillName("Pulinpa"), LevelLearned = 4 });
            actualSkills.Add(new LearnedSkill { Skill = model.GetSkillBySkillName("Clairvoyance"), LevelLearned = 5 });

            List<LearnedSkill> learnedSkills = new List<LearnedSkill>();

            //Act
            learnedSkills = model.GetLearnedSkillsFromSkillList(skillList);

            //Assert
            Assert.AreEqual(actualSkills[0].Skill.Name, learnedSkills[0].Skill.Name);
            Assert.AreEqual(actualSkills[0].LevelLearned, learnedSkills[0].LevelLearned);
            Assert.AreEqual(actualSkills[1].Skill.Name, learnedSkills[1].Skill.Name);
            Assert.AreEqual(actualSkills[1].LevelLearned, learnedSkills[1].LevelLearned);
            Assert.AreEqual(actualSkills[2].Skill.Name, learnedSkills[2].Skill.Name);
            Assert.AreEqual(actualSkills[2].LevelLearned, learnedSkills[2].LevelLearned);
            Assert.AreEqual(actualSkills[3].Skill.Name, learnedSkills[3].Skill.Name);
            Assert.AreEqual(actualSkills[3].LevelLearned, learnedSkills[3].LevelLearned);
            Assert.AreEqual(actualSkills[4].Skill.Name, learnedSkills[4].Skill.Name);
            Assert.AreEqual(actualSkills[4].LevelLearned, learnedSkills[4].LevelLearned);
        }
    }
}
