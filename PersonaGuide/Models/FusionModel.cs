using PersonaGuide.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonaGuide.Models
{
    public class FusionModel
    {
        FusionUtilities fusionUtilities;
        PersonaModel personaModel;

        public FusionModel(PersonaModel personaModel, FusionUtilities fusionUtilities)
        {
            this.personaModel = personaModel;
            this.fusionUtilities = fusionUtilities;
        }

        #region Known Persona Fusion

        public Persona FuseTwoKnownPersona(Persona firstPersona, Persona secondPersona)
        {
            //Can't fuse a person to itself.        
            if (firstPersona.Equals(secondPersona))
            {
                throw new ArgumentException("A persona cannot be fused to itself.");
            }

            Persona result = new Persona();

            //Is it a special fusion?
            if (fusionUtilities.SpecialFusionOnly(new string[] { firstPersona.Name, secondPersona.Name }))
            {
                result = personaModel.GetPersonaByName(fusionUtilities.SpecialFusionResultantPersonaName(new string[] { firstPersona.Name, secondPersona.Name }));
                return result;
            }

            int averageBaseLevel = Convert.ToInt32(Math.Round(((double)firstPersona.InitialLevel + (double)secondPersona.InitialLevel) / 2, MidpointRounding.AwayFromZero)) + 1;

            //Same arcana means different fusion rules.
            if (firstPersona.Arcana == secondPersona.Arcana)
            {
                result = personaModel.GetPersonaList(firstPersona.Arcana).Where(o => o.InitialLevel < averageBaseLevel && o.Name != firstPersona.Name && o.Name != secondPersona.Name).OrderByDescending(o => o.InitialLevel).FirstOrDefault();
            }
            else
            {
                object firstArcanaAsNumber = Convert.ChangeType(firstPersona.Arcana, firstPersona.Arcana.GetTypeCode());
                object secondArcanaAsNumber = Convert.ChangeType(secondPersona.Arcana, secondPersona.Arcana.GetTypeCode());

                Arcana resultingArcana;

                if (String.IsNullOrEmpty(fusionUtilities.GetDoubleFusionArcana((int)firstArcanaAsNumber, (int)secondArcanaAsNumber)))
                {
                    resultingArcana = (Arcana)Enum.Parse(typeof(Arcana), fusionUtilities.GetDoubleFusionArcana((int)secondArcanaAsNumber, (int)firstArcanaAsNumber));
                }
                else
                {
                    resultingArcana = (Arcana)Enum.Parse(typeof(Arcana), fusionUtilities.GetDoubleFusionArcana((int)firstArcanaAsNumber, (int)secondArcanaAsNumber));
                }

                //Get the persona on either side of the base level.  If one doesn't exist, get the highest or lowest of the arcana respectively.
                Persona lower = personaModel.GetPersonaList(resultingArcana).Where(o => o.InitialLevel <= averageBaseLevel).OrderByDescending(o => o.InitialLevel).FirstOrDefault();
                if(lower == null)
                {
                    lower = personaModel.GetPersonaList(resultingArcana).OrderBy(o => o.InitialLevel).FirstOrDefault();
                }

                Persona higher = personaModel.GetPersonaList(resultingArcana).Where(o => o.InitialLevel >= averageBaseLevel).OrderBy(o => o.InitialLevel).FirstOrDefault();
                if(higher == null)
                {
                    higher = personaModel.GetPersonaList(resultingArcana).OrderByDescending(o => o.InitialLevel).FirstOrDefault();
                }
                
                //Take higher or lower, depending on which one has the smallest difference.
                if(Math.Abs(lower.InitialLevel - averageBaseLevel) > Math.Abs(higher.InitialLevel - averageBaseLevel))
                {
                    result = higher;
                }
                else
                {
                    result = lower;
                }                
            }

            return result;
        }

        public Persona FuseThreeKnownPersona(Persona firstPersona, Persona secondPersona, Persona thirdPersona)
        {
            //If any two persona or all three persona are the same, error!
            if (firstPersona.Equals(secondPersona) || secondPersona.Equals(thirdPersona) || firstPersona.Equals(thirdPersona))
            {
                throw new ArgumentException("A persona cannot be fused to itself.");
            }

            Persona result = new Persona();

            //Is it a special fusion?
            if (fusionUtilities.SpecialFusionOnly(new string[] { firstPersona.Name, secondPersona.Name, thirdPersona.Name }))
            {
                result = personaModel.GetPersonaByName(fusionUtilities.SpecialFusionResultantPersonaName(new string[] { firstPersona.Name, secondPersona.Name, thirdPersona.Name }));
                return result;
            }

            int averageBaseLevel = Convert.ToInt32(Math.Round(((double)firstPersona.InitialLevel + (double)secondPersona.InitialLevel + (double)thirdPersona.InitialLevel) / 3, MidpointRounding.AwayFromZero)) + 5;

            //Same Arcana means different fusion rules.
            if (firstPersona.Arcana == secondPersona.Arcana && firstPersona.Arcana == thirdPersona.Arcana)
            {
                //Result is the first persona in the arcana list that is greater than average base level, and not one of the components.
                result = personaModel.GetPersonaList(firstPersona.Arcana).Where(o => o.InitialLevel > averageBaseLevel && o.Name != firstPersona.Name && o.Name != secondPersona.Name && o.Name != thirdPersona.Name).OrderBy(o => o.InitialLevel).FirstOrDefault();
            }
            else
            {
                //Make a list of the persona, order by level.
                List<Persona> components = new List<Persona>();
                components.Add(firstPersona);
                components.Add(secondPersona);
                components.Add(thirdPersona);
                components.OrderBy(o => o.InitialLevel);

                //Fuse the lowest two persona.
                Persona temp = FuseTwoKnownPersona(components[0], components[1]);

                //If the temporary persona equals the third, not valid.
                if (temp.Equals(components[2]))
                {
                    throw new ArgumentException("A persona cannot be fused to itself.");
                }

                object firstArcanaAsNumber = Convert.ChangeType(temp.Arcana, temp.Arcana.GetTypeCode());
                object secondArcanaAsNumber = Convert.ChangeType(components[2].Arcana, components[2].Arcana.GetTypeCode());

                Arcana resultingArcana;

                if (String.IsNullOrEmpty(fusionUtilities.GetTripleFusionArcana((int)firstArcanaAsNumber, (int)secondArcanaAsNumber)))
                {
                    resultingArcana = (Arcana)Enum.Parse(typeof(Arcana), fusionUtilities.GetTripleFusionArcana((int)secondArcanaAsNumber, (int)firstArcanaAsNumber));
                }
                else
                {
                    resultingArcana = (Arcana)Enum.Parse(typeof(Arcana), fusionUtilities.GetTripleFusionArcana((int)firstArcanaAsNumber, (int)secondArcanaAsNumber));
                }

                //Get the persona on either side of the base level.  If one doesn't exist, get the highest or lowest of the arcana respectively.
                Persona lower = personaModel.GetPersonaList(resultingArcana).Where(o => o.InitialLevel <= averageBaseLevel).OrderByDescending(o => o.InitialLevel).FirstOrDefault();
                if (lower == null)
                {
                    lower = personaModel.GetPersonaList(resultingArcana).OrderBy(o => o.InitialLevel).FirstOrDefault();
                }

                Persona higher = personaModel.GetPersonaList(resultingArcana).Where(o => o.InitialLevel >= averageBaseLevel).OrderBy(o => o.InitialLevel).FirstOrDefault();
                if (higher == null)
                {
                    higher = personaModel.GetPersonaList(resultingArcana).OrderByDescending(o => o.InitialLevel).FirstOrDefault();
                }

                //Take higher or lower, depending on which one has the smallest difference.
                if (Math.Abs(lower.InitialLevel - averageBaseLevel) > Math.Abs(higher.InitialLevel - averageBaseLevel))
                {
                    result = higher;
                }
                else
                {
                    result = lower;
                }                          
            }

            return result;
        }

        #endregion

        #region How Do I Fuse

        public List<string[]> HowDoIFuse(Arcana arcanaToFuse)
        {
            List<string[]> combinations = new List<string[]>();

            combinations.AddRange(fusionUtilities.DoubleFusionsByArcana(arcanaToFuse));

            combinations.AddRange(fusionUtilities.TripleFusionByArcana(arcanaToFuse));

            return combinations;
        }

        public List<string[]> HowDoIFuse(Persona personaToFuse)
        {
            List<string[]> combinations = new List<string[]>();

            if (fusionUtilities.SpecialFusionOnly(personaToFuse.Name))
            {
                combinations.AddRange(fusionUtilities.SpecialFusionCombinationByPersona(personaToFuse.Name));
                return combinations;
            }

            List<string[]> doubleCombinations = fusionUtilities.DoubleFusionsByArcana(personaToFuse.Arcana);
            List<string[]> tripleCombinations = fusionUtilities.TripleFusionByArcana(personaToFuse.Arcana);

            combinations.AddRange(GetDoubleFusionMatches(doubleCombinations, personaToFuse));

            combinations.AddRange(GetTripleFusionMatches(tripleCombinations, personaToFuse));

            return combinations;
        }

        public List<string[]> HowDoIFuse(Persona personaToFuse, Persona firstPersonaToInclude)
        {
            List<string[]> combinations = new List<string[]>();
            List<string[]> temp = new List<string[]>();

            temp = HowDoIFuse(personaToFuse);

            //Check for the included persona.
            foreach (string[] item in temp)
            {
                if (item.Contains(firstPersonaToInclude.Name))
                {
                    combinations.Add(item);
                }
            }

            return combinations;
        }

        public List<string[]> HowDoIFuse(Persona personaToFuse, Persona firstPersonaToInclude, Persona secondPersonaToInclude)
        {
            List<string[]> combinations = new List<string[]>();
            List<string[]> temp = new List<string[]>();

            temp = HowDoIFuse(personaToFuse, firstPersonaToInclude);

            //Check for the included persona.
            foreach (string[] item in temp)
            {
                if (item.Contains(secondPersonaToInclude.Name))
                {
                    combinations.Add(item);
                }
            }

            return combinations;
        }

        public List<string[]> HowDoIFuse(Persona personaToFuse, int cappedComponentLevel)
        {
            List<string[]> combinations = new List<string[]>();

            if (fusionUtilities.SpecialFusionOnly(personaToFuse.Name))
            {
                combinations.AddRange(fusionUtilities.SpecialFusionCombinationByPersona(personaToFuse.Name));
                return combinations;
            }

            List<string[]> doubleCombinations = fusionUtilities.DoubleFusionsByArcana(personaToFuse.Arcana);
            List<string[]> tripleCombinations = fusionUtilities.TripleFusionByArcana(personaToFuse.Arcana);

            combinations.AddRange(GetDoubleFusionMatches(doubleCombinations, personaToFuse, cappedComponentLevel));

            combinations.AddRange(GetTripleFusionMatches(tripleCombinations, personaToFuse, cappedComponentLevel));

            return combinations;
        }

        public List<string[]> HowDoIFuse(Persona personaToFuse, Persona firstPersonaToInclude, int cappedComponentLevel)
        {
            List<string[]> combinations = new List<string[]>();
            List<string[]> temp = new List<string[]>();

            temp = HowDoIFuse(personaToFuse, cappedComponentLevel);

            //Check for the included persona.
            foreach (string[] item in temp)
            {
                if (item.Contains(firstPersonaToInclude.Name))
                {
                    combinations.Add(item);
                }
            }

            return combinations;
        }

        public List<string[]> HowDoIFuse(Persona personaToFuse, Persona firstPersonaToInclude, Persona secondPersonaToInclude, int cappedComponentLevel)
        {
            List<string[]> combinations = new List<string[]>();
            List<string[]> temp = new List<string[]>();

            temp = HowDoIFuse(personaToFuse, firstPersonaToInclude, cappedComponentLevel);

            //Check for the included persona.
            foreach (string[] item in temp)
            {
                if (item.Contains(secondPersonaToInclude.Name))
                {
                    combinations.Add(item);
                }
            }

            return combinations;
        }

        #endregion

        #region Utilities

        public List<string[]> GetDoubleFusionMatches(List<string[]> doubleCombinations, Persona personaToFuse, int cappedComponentLevel = 99)
        {
            List<string[]> combinations = new List<string[]>();
            int targetLevel = personaToFuse.InitialLevel;

            int nextLowestLevel = int.MinValue;
            int nextHighestLevel = int.MaxValue;

            double lowLevelRange = double.MinValue;
            double highLevelRange = double.MaxValue;

            try
            {
                nextLowestLevel = personaModel.GetPersonaList(personaToFuse.Arcana).Where(o => o.InitialLevel < personaToFuse.InitialLevel).OrderByDescending(o => o.InitialLevel).FirstOrDefault().InitialLevel;
            }
            catch
            {                
                nextLowestLevel = 0;
            }

            lowLevelRange = ((targetLevel - nextLowestLevel) / 2) + nextLowestLevel;

            try
            {
                nextHighestLevel = personaModel.GetPersonaList(personaToFuse.Arcana).Where(o => o.InitialLevel > personaToFuse.InitialLevel).OrderBy(o => o.InitialLevel).FirstOrDefault().InitialLevel;
            }
            catch
            {
                nextHighestLevel = targetLevel;
            }

            highLevelRange = ((nextHighestLevel - targetLevel) / 2) + targetLevel;

            //Get the next two highest for a valid same arcana fusion.
            List<Persona> doubleCombo = personaModel.GetPersonaList(personaToFuse.Arcana).Where(o => o.InitialLevel > targetLevel && o.InitialLevel <= cappedComponentLevel).OrderBy(o => o.InitialLevel).Take(2).ToList<Persona>();
            if (doubleCombo != null && doubleCombo.Count == 2)
            {
                if (!fusionUtilities.ListContainsFusionCombination(combinations, doubleCombo[0].Name, doubleCombo[1].Name))
                {
                    combinations.Add(new string[] { doubleCombo[0].Name, doubleCombo[1].Name });
                }
            }

            foreach (string[] arcanaList in doubleCombinations)
            {
                List<Persona> firstPrecursors = new List<Persona>();
                List<Persona> secondPrecursors = new List<Persona>();

                firstPrecursors = personaModel.GetPersonaList((Arcana)Enum.Parse(typeof(Arcana), arcanaList[0])).Where(o => o.InitialLevel <= cappedComponentLevel).OrderBy(o => o.InitialLevel).ToList<Persona>();
                secondPrecursors = personaModel.GetPersonaList((Arcana)Enum.Parse(typeof(Arcana), arcanaList[1])).Where(o => o.InitialLevel <= cappedComponentLevel).OrderBy(o => o.InitialLevel).ToList<Persona>();

                foreach (Persona first in firstPrecursors)
                {
                    if (!first.Equals(personaToFuse))
                    {
                        foreach (Persona second in secondPrecursors)
                        {
                            if (!second.Equals(personaToFuse) && !first.Equals(second))
                            {
                                int resultingFusionLevel = Convert.ToInt32(Math.Round(((double)first.InitialLevel + (double)second.InitialLevel) / 2, MidpointRounding.AwayFromZero)) + 1;                                

                                if (resultingFusionLevel >= lowLevelRange && resultingFusionLevel < highLevelRange)
                                {
                                    if (!fusionUtilities.ListContainsFusionCombination(combinations, first.Name, second.Name))
                                    {
                                        combinations.Add(new string[] { first.Name, second.Name });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return combinations;
        }

        public List<string[]> GetTripleFusionMatches(List<string[]> tripleCombinations, Persona personaToFuse, int cappedComponentLevel = 99)
        {
            List<string[]> combinations = new List<string[]>();
            int targetLevel = personaToFuse.InitialLevel;

            int nextLowestLevel = int.MinValue;
            int nextHighestLevel = int.MaxValue;

            double lowLevelRange = double.MinValue;
            double highLevelRange = double.MaxValue;

            try
            {
                nextLowestLevel = personaModel.GetPersonaList(personaToFuse.Arcana).Where(o => o.InitialLevel < personaToFuse.InitialLevel).OrderByDescending(o => o.InitialLevel).FirstOrDefault().InitialLevel;
            }
            catch (Exception ex)
            {
                nextLowestLevel = 0;
            }

            lowLevelRange = ((targetLevel - nextLowestLevel) / 2) + nextLowestLevel;

            try
            {
                nextHighestLevel = personaModel.GetPersonaList(personaToFuse.Arcana).Where(o => o.InitialLevel > personaToFuse.InitialLevel).OrderBy(o => o.InitialLevel).FirstOrDefault().InitialLevel;
            }
            catch
            {
                nextHighestLevel = targetLevel;
            }

            highLevelRange = ((nextHighestLevel - targetLevel) / 2) + targetLevel;

            //Check for a valid triple combination - three lower of the same arcana.
            List<Persona> tripleCombo = personaModel.GetPersonaList(personaToFuse.Arcana).Where(o => o.InitialLevel < personaToFuse.InitialLevel && o.InitialLevel <= cappedComponentLevel).OrderBy(o => o.InitialLevel).Take(3).ToList<Persona>();

            if (tripleCombo != null && tripleCombo.Count == 3)
            {
                if (!fusionUtilities.ListContainsFusionCombination(combinations, tripleCombo[0].Name, tripleCombo[1].Name, tripleCombo[2].Name))
                {
                    combinations.Add(new string[] { tripleCombo[0].Name, tripleCombo[1].Name, tripleCombo[2].Name });
                }
            }

            //Triples now
            foreach (string[] arcanaTripleList in tripleCombinations)
            {
                //For each arcana in the list, get all the double combinations that make it, then test them to make sure they match conditions.
                List<string[]> firstHalfDoubleCombinations = fusionUtilities.DoubleFusionsByArcana((Arcana)Enum.Parse(typeof(Arcana), arcanaTripleList[0]));
                List<string[]> secondHalfDoubleCombinations = fusionUtilities.DoubleFusionsByArcana((Arcana)Enum.Parse(typeof(Arcana), arcanaTripleList[1]));

                //Do the first half.
                List<Persona> firstHalfThirdPersona = personaModel.GetPersonaList((Arcana)Enum.Parse(typeof(Arcana), arcanaTripleList[1])).Where(o => o.Name != personaToFuse.Name && o.InitialLevel <= cappedComponentLevel).OrderBy(o => o.InitialLevel).ToList<Persona>();

                foreach (Persona third in firstHalfThirdPersona)
                {
                    foreach (string[] arcanaList in firstHalfDoubleCombinations)
                    {
                        List<Persona> firstPrecursors = new List<Persona>();
                        List<Persona> secondPrecursors = new List<Persona>();

                        firstPrecursors = personaModel.GetPersonaList((Arcana)Enum.Parse(typeof(Arcana), arcanaList[0])).Where(o => o.Name != personaToFuse.Name && o.InitialLevel <= cappedComponentLevel).OrderBy(o => o.InitialLevel).ToList<Persona>();
                        secondPrecursors = personaModel.GetPersonaList((Arcana)Enum.Parse(typeof(Arcana), arcanaList[1])).Where(o => o.Name != personaToFuse.Name && o.InitialLevel <= cappedComponentLevel).OrderBy(o => o.InitialLevel).ToList<Persona>();

                        foreach (Persona first in firstPrecursors)
                        {
                            if (!first.Equals(third))
                            {
                                foreach (Persona second in secondPrecursors)
                                {
                                    if (!second.Equals(third) && !second.Equals(first) && second.Arcana != first.Arcana)
                                    {
                                        int resultingFusionLevel = Convert.ToInt32(Math.Round(((double)first.InitialLevel + (double)second.InitialLevel + (double)third.InitialLevel) / 3, MidpointRounding.AwayFromZero)) + 5;
                                        
                                        if (resultingFusionLevel >= lowLevelRange && resultingFusionLevel < highLevelRange && third.InitialLevel > first.InitialLevel && third.InitialLevel > second.InitialLevel)
                                        {
                                            if (!fusionUtilities.ListContainsFusionCombination(combinations, first.Name, second.Name, third.Name))
                                            {
                                                combinations.Add(new string[] { first.Name, second.Name, third.Name });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //Do the second half.
                List<Persona> secondHalfThirdPersona = personaModel.GetPersonaList((Arcana)Enum.Parse(typeof(Arcana), arcanaTripleList[0])).Where(o => o.Name != personaToFuse.Name && o.InitialLevel <= cappedComponentLevel && o.InitialLevel < (targetLevel * 3)).ToList<Persona>();
                foreach (Persona third in secondHalfThirdPersona)
                {
                    foreach (string[] arcanaList in secondHalfDoubleCombinations)
                    {
                        List<Persona> firstPrecursors = new List<Persona>();
                        List<Persona> secondPrecursors = new List<Persona>();

                        firstPrecursors = personaModel.GetPersonaList((Arcana)Enum.Parse(typeof(Arcana), arcanaList[0])).Where(o => o.Name != personaToFuse.Name && o.InitialLevel <= cappedComponentLevel).OrderBy(o => o.InitialLevel).ToList<Persona>();
                        secondPrecursors = personaModel.GetPersonaList((Arcana)Enum.Parse(typeof(Arcana), arcanaList[1])).Where(o => o.Name != personaToFuse.Name && o.InitialLevel <= cappedComponentLevel).OrderBy(o => o.InitialLevel).ToList<Persona>();

                        foreach (Persona first in firstPrecursors)
                        {
                            if (!first.Equals(third))
                            {
                                foreach (Persona second in secondPrecursors)
                                {
                                    if (!second.Equals(third) && !second.Equals(first) && second.Arcana != first.Arcana)
                                    {
                                        int resultingFusionLevel = Convert.ToInt32(Math.Round(((double)first.InitialLevel + (double)second.InitialLevel + (double)third.InitialLevel) / 3, MidpointRounding.AwayFromZero)) + 5;

                                        //if (resultingFusionLevel > nextLowestLevel && resultingFusionLevel < targetLevel && third.InitialLevel > first.InitialLevel && third.InitialLevel > second.InitialLevel)
                                        //{
                                        //    if (!fusionUtilities.ListContainsFusionCombination(combinations, first.Name, second.Name, third.Name))
                                        //    {
                                        //        combinations.Add(new string[] { first.Name, second.Name, third.Name });
                                        //    }
                                        //}

                                        if (resultingFusionLevel > lowLevelRange && resultingFusionLevel < highLevelRange && third.InitialLevel > first.InitialLevel && third.InitialLevel > second.InitialLevel)
                                        {
                                            if (!fusionUtilities.ListContainsFusionCombination(combinations, first.Name, second.Name, third.Name))
                                            {
                                                combinations.Add(new string[] { first.Name, second.Name, third.Name });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return combinations;
        }

        #endregion
    }
}