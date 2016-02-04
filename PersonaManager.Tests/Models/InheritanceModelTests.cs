using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonaManager.Models;
using PersonaManager.Entities;
using System.Collections.Generic;

namespace PersonaManager.Tests.Models
{
    [TestClass]
    public class InheritanceModelTests
    {
        InheritanceModel model;

        [TestInitialize]        
        public void Initialize()
        {
            model = new InheritanceModel();
        }

        [TestMethod]
        public void GetSkillInheritanceByPersonaInheritanceType()
        {
            //Arrange  
            PersonaInheritanceType type = PersonaInheritanceType.Dark_A;

            //Act
            var result = model.GetSkillInheritanceByPersonaInheritanceType(type);

            //Assert
            Assert.IsNotNull(result); 
        }

        [TestMethod]
        public void GetSkillInheritanceByPersonaInheritanceType_CountGreaterThanZero()
        {
            //Arrange  
            int actualCount = 0;
            PersonaInheritanceType type = PersonaInheritanceType.Dark_A;

            //Act
            var result = model.GetSkillInheritanceByPersonaInheritanceType(type);

            //Assert
            Assert.AreNotEqual(result.Count, actualCount);
        }

        [TestMethod]
        public void GetSkillInheritanceByPersonaInheritanceType_CheckReturnedValuesAreCorrect()
        {
            //Arrange              
            PersonaInheritanceType type = PersonaInheritanceType.Dark_A;

            List<SkillInheritance> actualSkillInheritance = new List<SkillInheritance>();
            actualSkillInheritance.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Physical });
            actualSkillInheritance.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Fire });
            actualSkillInheritance.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Ice });
            actualSkillInheritance.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Electricity });
            actualSkillInheritance.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Wind });
            actualSkillInheritance.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Dark });
            actualSkillInheritance.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Almighty });
            actualSkillInheritance.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Status });
            actualSkillInheritance.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Recovery });
            actualSkillInheritance.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Support });
            actualSkillInheritance.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Passive });
            actualSkillInheritance.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Navigator });                                                                       

            List<SkillInheritance> resultSkillInheritance = new List<SkillInheritance>();

            //Act
            resultSkillInheritance = model.GetSkillInheritanceByPersonaInheritanceType(type);

            //Assert
            Assert.AreEqual(resultSkillInheritance[0].Type, actualSkillInheritance[0].Type);
            Assert.AreEqual(resultSkillInheritance[1].Type, actualSkillInheritance[1].Type);
            Assert.AreEqual(resultSkillInheritance[2].Type, actualSkillInheritance[2].Type);
            Assert.AreEqual(resultSkillInheritance[3].Type, actualSkillInheritance[3].Type);
            Assert.AreEqual(resultSkillInheritance[4].Type, actualSkillInheritance[4].Type);
            Assert.AreEqual(resultSkillInheritance[5].Type, actualSkillInheritance[5].Type);
            Assert.AreEqual(resultSkillInheritance[6].Type, actualSkillInheritance[6].Type);
            Assert.AreEqual(resultSkillInheritance[7].Type, actualSkillInheritance[7].Type);
            Assert.AreEqual(resultSkillInheritance[8].Type, actualSkillInheritance[8].Type);
            Assert.AreEqual(resultSkillInheritance[9].Type, actualSkillInheritance[9].Type);
            Assert.AreEqual(resultSkillInheritance[10].Type, actualSkillInheritance[10].Type);
            Assert.AreEqual(resultSkillInheritance[11].Type, actualSkillInheritance[11].Type);            
        }
    }
}
