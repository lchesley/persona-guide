using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using PersonaGuide.Models;
using PersonaGuide.Entities;

namespace PersonaManager.Tests.Models
{
    [TestClass]
    public class FusionModelTest
    {
        SkillModel skillModel;
        PersonaModel personaModel;
        FusionModel fusionModel;        

        [TestInitialize]
        [DeploymentItem(@"App_Data\", @"App_Data\")]
        public void Initialize()
        {
            StreamReader fusionGuide = new StreamReader("App_Data\\FusionGuide.csv");
            StreamReader skillList = new StreamReader("App_Data\\SkillList.csv");

            FusionUtilities fusionUtilities = new FusionUtilities();

            skillModel = new SkillModel(skillList);
            personaModel = new PersonaModel(fusionGuide, skillModel);            
            fusionModel = new FusionModel(personaModel, fusionUtilities);
        }

        [TestMethod]        
        public void FuseTwoKnownPersona()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByName("Turdak");
            Persona second = personaModel.GetPersonaByName("Gozuki");

            //Act
            var result = fusionModel.FuseTwoKnownPersona(first, second);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void FuseTwoKnownPersona_LowLevel()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByName("Anzu");
            Persona second = personaModel.GetPersonaByName("Pixie");

            Persona expected = personaModel.GetPersonaByName("Sandman");

            //Act
            var result = fusionModel.FuseTwoKnownPersona(first, second);

            //Assert
            Assert.AreEqual<Persona>(result, expected);
        }

        [TestMethod]        
        [ExpectedException(typeof(ArgumentException), "Cannot fuse a persona to itself.")]
        public void FuseTwoKnownPersona_InvalidCombination()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByName("Turdak");
            Persona second = first;

            //Act
            var result = fusionModel.FuseTwoKnownPersona(first, second);           
        }
        
        [TestMethod]        
        public void FuseTwoKnownPersona_DifferentArcana()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByName("Neko Shogun");
            Persona second = personaModel.GetPersonaByName("Hua Po");

            Persona expected = personaModel.GetPersonaByName("Turdak");

            //Act
            var result = fusionModel.FuseTwoKnownPersona(first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<Persona>(expected, result);
        }

        [TestMethod]        
        public void FuseTwoKnownPersona_SameArcana()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByName("Matador");
            Persona second = personaModel.GetPersonaByName("Turdak");

            Persona expected = personaModel.GetPersonaByName("Mokoi");
            //Act
            var result = fusionModel.FuseTwoKnownPersona(first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<Persona>(expected, result);
        }

        [TestMethod]
        public void FuseTwoKnownPersona_SameArcana_Fool()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByName("Ose");
            Persona second = personaModel.GetPersonaByName("Black Frost");

            Persona expected = personaModel.GetPersonaByName("Orpheus Telos");
            //Act
            var result = fusionModel.FuseTwoKnownPersona(first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<Persona>(expected, result);
        }

        [TestMethod]
        public void FuseTwoKnownPersona_SpecialFusion()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByName("Nebiros");
            Persona second = personaModel.GetPersonaByName("Belial");

            Persona expected = personaModel.GetPersonaByName("Alice");
            //Act
            var result = fusionModel.FuseTwoKnownPersona(first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<Persona>(expected, result);
        }

        [TestMethod]        
        public void FuseThreeKnownPersona()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByName("Turdak");
            Persona second = personaModel.GetPersonaByName("Gozuki");
            Persona third = personaModel.GetPersonaByName("Eligor");

            //Act
            var result = fusionModel.FuseThreeKnownPersona(first, second, third);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Cannot fuse a persona to itself.")]
        public void FuseThreeKnownPersona_InvalidCombination()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByName("Turdak");
            Persona second = personaModel.GetPersonaByName("Turdak");
            Persona third = personaModel.GetPersonaByName("Eligor");

            //Act
            var result = fusionModel.FuseThreeKnownPersona(first, second, third);           
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Cannot fuse a persona to itself.")]
        public void FuseThreeKnownPersona_InvalidCombination_AfterInitialFusion()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByName("Neko Shogun");
            Persona second = personaModel.GetPersonaByName("Hua Po");
            Persona third = personaModel.GetPersonaByName("Turdak");

            //Act
            var result = fusionModel.FuseThreeKnownPersona(first, second, third);
        }

        [TestMethod]        
        public void FuseThreeKnownPersona_DifferentArcana()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByName("Nue");
            Persona second = personaModel.GetPersonaByName("Eligor");
            Persona third = personaModel.GetPersonaByName("Turdak");

            Persona expected = personaModel.GetPersonaByName("Mithra");

            //Act
            var result = fusionModel.FuseThreeKnownPersona(first, second, third);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<Persona>(expected, result);
        }
        
        [TestMethod]        
        public void FuseThreeKnownPersona_SameArcana()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByName("Nata Taishi");
            Persona second = personaModel.GetPersonaByName("Chimera");
            Persona third = personaModel.GetPersonaByName("Eligor");

            Persona expected = personaModel.GetPersonaByName("Ares");

            //Act
            var result = fusionModel.FuseThreeKnownPersona(first, second, third);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<Persona>(expected, result);
        }

        [TestMethod]
        public void FuseThreeKnownPersona_SpecialFusion()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByName("White Rider");
            Persona second = personaModel.GetPersonaByName("Red Rider");
            Persona third = personaModel.GetPersonaByName("Black Rider");

            Persona expected = personaModel.GetPersonaByName("Pale Rider");

            //Act
            var result = fusionModel.FuseThreeKnownPersona(first, second, third);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<Persona>(expected, result);
        } 

        [TestMethod]        
        public void HowDoIFuse_Arcana()
        {
            //Arrange            

            //Act
            var result = fusionModel.HowDoIFuse(Arcana.Fool);
            
            //Assert            
            Assert.IsNotNull(result);
        }
 
        [TestMethod]
        public void HowDoIFuse_Persona()
        {
            //Arrange       
            Persona persona = personaModel.GetPersonaByName("Legion");
            bool result = false;

            //Act
            var list = fusionModel.HowDoIFuse(persona);

            foreach (string[] item in list)
            {
                if(Array.Exists(item, o => o == "Orpheus Telos") && Array.Exists(item, o => o == "Ose"))
                {
                    result = true;
                }
            }
            
            //Assert            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HowDoIFuse_Persona_SpecialFusion()
        {
            //Arrange       
            Persona persona = personaModel.GetPersonaByName("Alice");
            bool result = false;

            //Act
            var list = fusionModel.HowDoIFuse(persona);

            foreach (string[] item in list)
            {
                if (Array.Exists(item, o => o == "Nebiros") && Array.Exists(item, o => o == "Belial"))
                {
                    result = true;
                }
            }

            //Assert            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HowDoIFuse_Persona_BackwardsTest()
        {
            //Arrange
            Persona persona = personaModel.GetPersonaByName("Ares");
            bool result = true;

            //Act
            var list = fusionModel.HowDoIFuse(persona);

            foreach(string[] item in list)
            {
                Persona check = new Persona();

                if (item.Count() == 2)
                {
                    check = fusionModel.FuseTwoKnownPersona(personaModel.GetPersonaByName(item[0]), personaModel.GetPersonaByName(item[1]));
                    if(!check.Equals(persona))
                    {
                        result = false;
                    }                    
                }
                else
                {
                    check = fusionModel.FuseThreeKnownPersona(personaModel.GetPersonaByName(item[0]), personaModel.GetPersonaByName(item[1]), personaModel.GetPersonaByName(item[2]));
                    if (!check.Equals(persona))
                    {
                        result = false;
                    }                    
                }
            }

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HowDoIFuse_Persona_OneIncludedPersona_BackwardsTest()
        {
            //Arrange
            Persona persona = personaModel.GetPersonaByName("Turdak");
            Persona includedPersona = personaModel.GetPersonaByName("Eligor");

            bool result = true;

            //Act
            var list = fusionModel.HowDoIFuse(persona, includedPersona);

            foreach (string[] item in list)
            {
                if(!item.Contains(includedPersona.Name))
                {
                    result = false;
                }

                Persona check = new Persona();

                if (item.Count() == 2)
                {
                    check = fusionModel.FuseTwoKnownPersona(personaModel.GetPersonaByName(item[0]), personaModel.GetPersonaByName(item[1]));
                    if (!check.Equals(persona))
                    {
                        result = false;
                    }
                }
                else
                {
                    check = fusionModel.FuseThreeKnownPersona(personaModel.GetPersonaByName(item[0]), personaModel.GetPersonaByName(item[1]), personaModel.GetPersonaByName(item[2]));
                    if (!check.Equals(persona))
                    {
                        result = false;
                    }
                }
            }

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HowDoIFuse_Persona_TwoIncludedPersona_BackwardsTest()
        {
            //Arrange
            Persona persona = personaModel.GetPersonaByName("Turdak");
            Persona includedPersona = personaModel.GetPersonaByName("Eligor");
            Persona secondIncludedPersona = personaModel.GetPersonaByName("Oni");

            bool result = true;

            //Act
            var list = fusionModel.HowDoIFuse(persona, includedPersona, secondIncludedPersona);

            foreach (string[] item in list)
            {
                if (!item.Contains(includedPersona.Name) || !item.Contains(secondIncludedPersona.Name))
                {
                    result = false;
                }

                Persona check = new Persona();

                if (item.Count() == 2)
                {
                    check = fusionModel.FuseTwoKnownPersona(personaModel.GetPersonaByName(item[0]), personaModel.GetPersonaByName(item[1]));
                    if (!check.Equals(persona))
                    {
                        result = false;
                    }
                }
                else
                {
                    check = fusionModel.FuseThreeKnownPersona(personaModel.GetPersonaByName(item[0]), personaModel.GetPersonaByName(item[1]), personaModel.GetPersonaByName(item[2]));
                    if (!check.Equals(persona))
                    {
                        result = false;
                    }
                }
            }

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HowDoIFuse_Persona_CappedComponentLevel_BackwardsTest()
        {
            //Arrange
            Persona persona = personaModel.GetPersonaByName("Raijuu");
            bool result = true;
            int cappedComponentLevel = 12;

            //Act
            var list = fusionModel.HowDoIFuse(persona, cappedComponentLevel);

            foreach (string[] item in list)
            {
                Persona check = new Persona();

                foreach(string temp in item)
                {
                    if(personaModel.GetPersonaByName(temp).InitialLevel > cappedComponentLevel)
                    {
                        result = false;
                    }
                }

                if (item.Count() == 2)
                {

                    check = fusionModel.FuseTwoKnownPersona(personaModel.GetPersonaByName(item[0]), personaModel.GetPersonaByName(item[1]));
                    if (!check.Equals(persona))
                    {
                        result = false;
                    }
                }
                else
                {
                    check = fusionModel.FuseThreeKnownPersona(personaModel.GetPersonaByName(item[0]), personaModel.GetPersonaByName(item[1]), personaModel.GetPersonaByName(item[2]));
                    if (!check.Equals(persona))
                    {
                        result = false;
                    }
                }
            }

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HowDoIFuse_Persona_OneIncludedPersona_CappedComponentLevel_BackwardsTest()
        {
            //Arrange
            Persona persona = personaModel.GetPersonaByName("Turdak");
            Persona includedPersona = personaModel.GetPersonaByName("Eligor");
            int cappedComponentLevel = 27;

            bool result = true;

            //Act
            var list = fusionModel.HowDoIFuse(persona, includedPersona, cappedComponentLevel);

            foreach (string[] item in list)
            {
                foreach (string temp in item)
                {
                    if (personaModel.GetPersonaByName(temp).InitialLevel > cappedComponentLevel)
                    {
                        result = false;
                    }
                }

                if (!item.Contains(includedPersona.Name))
                {
                    result = false;
                }

                Persona check = new Persona();

                if (item.Count() == 2)
                {
                    check = fusionModel.FuseTwoKnownPersona(personaModel.GetPersonaByName(item[0]), personaModel.GetPersonaByName(item[1]));
                    if (!check.Equals(persona))
                    {
                        result = false;
                    }
                }
                else
                {
                    check = fusionModel.FuseThreeKnownPersona(personaModel.GetPersonaByName(item[0]), personaModel.GetPersonaByName(item[1]), personaModel.GetPersonaByName(item[2]));
                    if (!check.Equals(persona))
                    {
                        result = false;
                    }
                }
            }

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HowDoIFuse_Persona_TwoIncludedPersona_CappedComponentLevel_BackwardsTest()
        {
            //Arrange
            Persona persona = personaModel.GetPersonaByName("Turdak");
            Persona includedPersona = personaModel.GetPersonaByName("Eligor");
            Persona secondIncludedPersona = personaModel.GetPersonaByName("Oni");
            int cappedComponentLevel = 27;

            bool result = true;

            //Act
            var list = fusionModel.HowDoIFuse(persona, includedPersona, secondIncludedPersona, cappedComponentLevel);

            foreach (string[] item in list)
            {
                foreach (string temp in item)
                {
                    if (personaModel.GetPersonaByName(temp).InitialLevel > cappedComponentLevel)
                    {
                        result = false;
                    }
                }

                if (!item.Contains(includedPersona.Name) || !item.Contains(secondIncludedPersona.Name))
                {
                    result = false;
                }

                Persona check = new Persona();

                if (item.Count() == 2)
                {
                    check = fusionModel.FuseTwoKnownPersona(personaModel.GetPersonaByName(item[0]), personaModel.GetPersonaByName(item[1]));
                    if (!check.Equals(persona))
                    {
                        result = false;
                    }
                }
                else
                {
                    check = fusionModel.FuseThreeKnownPersona(personaModel.GetPersonaByName(item[0]), personaModel.GetPersonaByName(item[1]), personaModel.GetPersonaByName(item[2]));
                    if (!check.Equals(persona))
                    {
                        result = false;
                    }
                }
            }

            //Assert
            Assert.IsTrue(result);
        }
    }
}
