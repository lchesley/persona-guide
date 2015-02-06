using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PersonaGuide.Models;
using PersonaGuide.Entities;

namespace PersonaManager.Tests.Models
{
    [TestClass]
    public class FusionUtilitiesTest
    {
        FusionUtilities model;

        [TestInitialize]
        public void Initialize()
        {
            model = new FusionUtilities();
        }

        [TestMethod]        
        public void DoubleFusionsByArcana()
        {
            //Arrange                             

            //Act
            var result = model.DoubleFusionsByArcana(Arcana.Fool);

            //Assert            
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DoubleFusionsByArcana_CheckResult()
        {
            //Arrange                                    
            int expected = 0; 

            //Act
            var result = model.DoubleFusionsByArcana(Arcana.Fool);            

            //Assert            
            Assert.AreEqual(result.Count, expected);            
        }

        [TestMethod]        
        public void TripleFusionsByArcana()
        {
            //Arrange                                   

            //Act
            var result = model.TripleFusionByArcana(Arcana.Fool);

            //Assert            
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TripleFusionsByArcana_CheckResult()
        {
            //Arrange                        
            int expected = 13;            

            //Act
            var result = model.TripleFusionByArcana(Arcana.Sun);            

            //Assert            
            Assert.AreEqual(result.Count, expected);            
        }

        [TestMethod]
        public void GetDoubleFusionArcana()
        {
            //Arrange
            string expected = "Fool";

            //Act
            var result = model.GetDoubleFusionArcana(0, 0);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetTripleFusionArcana()
        {
            //Arrange
            string expected = "Fool";

            //Act
            var result = model.GetTripleFusionArcana(0, 0);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ListContainsFusionCombination_Found()
        {
            //Arrange
            List<string[]> combinations = new List<string[]>();
            combinations.Add(new string[] { "a", "b" });
            combinations.Add(new string[] { "b", "d" });
            combinations.Add(new string[] { "c", "d" });
            combinations.Add(new string[] { "b", "b" });

            string first = "a";
            string second = "b";

            //Act
            bool result = model.ListContainsFusionCombination(combinations, first, second);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ListContainsFusionCombination_NotFound()
        {
            //Arrange
            List<string[]> combinations = new List<string[]>();
            combinations.Add(new string[] { "a", "b" });
            combinations.Add(new string[] { "b", "d" });
            combinations.Add(new string[] { "c", "d" });
            combinations.Add(new string[] { "b", "b" });            

            string first = "a";
            string second = "c";

            //Act
            bool result = model.ListContainsFusionCombination(combinations, first, second);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ListContainsFusionCombination_ChecksOnlyPairs()
        {
            //Arrange
            List<string[]> combinations = new List<string[]>();
            combinations.Add(new string[] { "a", "b" });
            combinations.Add(new string[] { "b", "d" });
            combinations.Add(new string[] { "c", "d" });
            combinations.Add(new string[] { "b", "b" });
            combinations.Add(new string[] { "a", "c", "d" });

            string first = "a";
            string second = "c";

            //Act
            bool result = model.ListContainsFusionCombination(combinations, first, second);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ListContainsFusionCombinationTriple_Found()
        {
            //Arrange
            List<string[]> combinations = new List<string[]>();
            combinations.Add(new string[] { "a", "b", "c" });
            combinations.Add(new string[] { "b", "d", "a" });
            combinations.Add(new string[] { "c", "d", "b" });
            combinations.Add(new string[] { "b", "b", "a" });

            string first = "a";
            string second = "b";
            string third = "c";

            //Act
            bool result = model.ListContainsFusionCombination(combinations, first, second, third);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ListContainsFusionCombinationTriple_NotFound()
        {
            //Arrange
            List<string[]> combinations = new List<string[]>();
            combinations.Add(new string[] { "a", "b", "c" });
            combinations.Add(new string[] { "b", "d", "a" });
            combinations.Add(new string[] { "c", "d", "b" });
            combinations.Add(new string[] { "b", "b", "a" });

            string first = "a";
            string second = "c";
            string third = "d";

            //Act
            bool result = model.ListContainsFusionCombination(combinations, first, second, third);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ListContainsFusionCombination_ChecksOnlyTriples()
        {
            //Arrange
            List<string[]> combinations = new List<string[]>();
            combinations.Add(new string[] { "a", "c" });
            combinations.Add(new string[] { "b", "d" });
            combinations.Add(new string[] { "c", "d" });
            combinations.Add(new string[] { "b", "b" });
            combinations.Add(new string[] { "a", "c", "d" });

            string first = "a";
            string second = "c";
            string third = "b";

            //Act
            bool result = model.ListContainsFusionCombination(combinations, first, second, third);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SpecialFusionOnlyPersona_False()
        {
            //Arrange
            string personaName = "Rangda";

            //Act
            bool result = model.SpecialFusionOnly(personaName);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SpecialFusionOnlyPersona_True()
        {
            //Arrange
            string personaName = "Alice";

            //Act
            bool result = model.SpecialFusionOnly(personaName);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SpecialFusionOnlyCombination_True()
        {
            //Arrange
            string[] combination = new string[] { "Nebiros", "Belial" };

            //Act
            bool result = model.SpecialFusionOnly(combination);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SpecialFusionOnlyCombination_False()
        {
            //Arrange
            string[] combination = new string[] { "Anzu", "Berith" };

            //Act
            bool result = model.SpecialFusionOnly(combination);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SpecialFusionOnlyCombination_Partial()
        {
            //Arrange
            string[] combination = new string[] { "White Rider", "Black Rider" };

            //Act
            bool result = model.SpecialFusionOnly(combination);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "No fusion result possible.")]
        public void SpecialFusionResultantPersonaName_NoResult()
        {
            //Arrange
            string[] combination = new string[] { "White Rider", "Black Rider" };

            //Act
            string result = model.SpecialFusionResultantPersonaName(combination);
        }

        [TestMethod]
        public void SpecialFusionResultantPersonaName_Result()
        {
            //Arrange
            string[] combination = new string[] { "White Rider", "Black Rider", "Red Rider" };
            string expected = "Pale Rider";

            //Act
            string result = model.SpecialFusionResultantPersonaName(combination);

            //Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "No fusion result possible.")]
        public void SpecialFusionCombinationByPersona_NoResult()
        {
            //Arrange
            string personaName = "Berith";

            //Act
            List<string[]> result = model.SpecialFusionCombinationByPersona(personaName);
        }

        [TestMethod]        
        public void SpecialFusionCombinationByPersona_Result()
        {
            //Arrange
            string personaName = "Alice";
            bool matchFound = false;

            //Act
            List<string[]> result = model.SpecialFusionCombinationByPersona(personaName);

            foreach (string[] item in result)
            {
                if (item.Contains("Nebiros") && item.Contains("Belial"))
                {
                    matchFound = true;
                }
            }

            //Assert
            Assert.IsTrue(matchFound);
        }
    }
}
