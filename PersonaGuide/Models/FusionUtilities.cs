using PersonaGuide.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonaGuide.Models
{
    public class FusionUtilities
    {
        string[,] doubleFusion;
        string[,] tripleFusion;
        Dictionary<string, string[]> specialFusion;

        public FusionUtilities()
        {
            #region Combination Arrays

            doubleFusion = new string[21, 21] {
                {ArcanaString.Fool, ArcanaString.Empress, ArcanaString.Devil, ArcanaString.Magician, ArcanaString.Temperance, ArcanaString.Justice, ArcanaString.Death, ArcanaString.Hierophant, ArcanaString.Strength, ArcanaString.Priestess, ArcanaString.Lovers, ArcanaString.Justice, ArcanaString.Magician, ArcanaString.Chariot, ArcanaString.Death, ArcanaString.Strength, ArcanaString.Hanged, ArcanaString.Lovers, ArcanaString.Emperor, ArcanaString.Hermit, ArcanaString.Star},
                {"", ArcanaString.Magician, ArcanaString.Chariot, ArcanaString.Justice, ArcanaString.Hanged, ArcanaString.Priestess, ArcanaString.Fortune, ArcanaString.Priestess, ArcanaString.Emperor, ArcanaString.Devil, ArcanaString.Justice, ArcanaString.Hierophant, ArcanaString.Hermit, ArcanaString.Hanged, ArcanaString.Star, ArcanaString.Chariot, ArcanaString.Temperance, ArcanaString.Death, ArcanaString.Emperor, ArcanaString.Star, ArcanaString.Empress},
                {"", "", ArcanaString.Priestess, ArcanaString.Temperance, ArcanaString.Empress, ArcanaString.Star, ArcanaString.Emperor, ArcanaString.Hierophant, ArcanaString.Death, ArcanaString.Strength, ArcanaString.Magician, ArcanaString.Justice, ArcanaString.Lovers, ArcanaString.Strength, ArcanaString.Star, ArcanaString.Lovers, ArcanaString.Fortune, ArcanaString.Magician, ArcanaString.Hanged, ArcanaString.Chariot, ArcanaString.Hermit},
                {"", "", "", ArcanaString.Empress, ArcanaString.Justice, ArcanaString.Magician, ArcanaString.Temperance, ArcanaString.Death, ArcanaString.Star, ArcanaString.Strength, ArcanaString.Hermit, ArcanaString.Chariot, ArcanaString.Devil, ArcanaString.Lovers, ArcanaString.Priestess, ArcanaString.Priestess, ArcanaString.Emperor, ArcanaString.Hierophant, ArcanaString.Priestess, ArcanaString.Chariot, ArcanaString.Fortune},
                {"", "", "", "", ArcanaString.Emperor,	ArcanaString.Hermit, ArcanaString.Fortune, ArcanaString.Strength, ArcanaString.Priestess, ArcanaString.Hierophant, ArcanaString.Star, ArcanaString.Star, ArcanaString.Strength, ArcanaString.Hierophant, ArcanaString.Devil, ArcanaString.Justice, ArcanaString.Lovers, ArcanaString.Hermit, ArcanaString.Temperance, ArcanaString.Magician, ArcanaString.Chariot},
                {"", "", "", "", "", ArcanaString.Hierophant, ArcanaString.Strength, ArcanaString.Star, ArcanaString.Hanged, ArcanaString.Lovers, ArcanaString.Strength, ArcanaString.Chariot, ArcanaString.Fortune, ArcanaString.Empress, ArcanaString.Chariot, ArcanaString.Emperor, ArcanaString.Devil, ArcanaString.Lovers, ArcanaString.Hermit, ArcanaString.Hanged, ArcanaString.Temperance},
                {"", "", "", "", "", "", ArcanaString.Lovers, ArcanaString.Devil, ArcanaString.Empress, ArcanaString.Chariot, ArcanaString.Justice, ArcanaString.Magician, ArcanaString.Death, ArcanaString.Emperor, ArcanaString.Hanged, ArcanaString.Empress, ArcanaString.Chariot, ArcanaString.Hierophant, ArcanaString.Magician, ArcanaString.Star, ArcanaString.Hanged},
                {"", "", "", "", "", "", "", ArcanaString.Chariot, ArcanaString.Magician, ArcanaString.Star, ArcanaString.Priestess, ArcanaString.Lovers, ArcanaString.Fortune, ArcanaString.Temperance, ArcanaString.Strength, ArcanaString.Magician, ArcanaString.Empress, ArcanaString.Emperor, ArcanaString.Justice, ArcanaString.Lovers, "-"},
                {"", "", "", "", "", "", "", "", ArcanaString.Justice, ArcanaString.Fortune, ArcanaString.Priestess, ArcanaString.Emperor, ArcanaString.Lovers, ArcanaString.Hermit, ArcanaString.Lovers, ArcanaString.Hierophant, ArcanaString.Fortune, ArcanaString.Temperance, ArcanaString.Empress, ArcanaString.Devil, "-"},
                {"", "", "", "", "", "", "", "", "", ArcanaString.Hermit, ArcanaString.Chariot, ArcanaString.Priestess, ArcanaString.Death, ArcanaString.Fortune, ArcanaString.Priestess, ArcanaString.Hanged, ArcanaString.Emperor, ArcanaString.Justice, ArcanaString.Hierophant, ArcanaString.Death, ArcanaString.Magician},
                {"", "", "", "", "", "", "", "", "", "", ArcanaString.Fortune, ArcanaString.Temperance, ArcanaString.Emperor, ArcanaString.Star, ArcanaString.Empress, ArcanaString.Hanged, ArcanaString.Temperance, ArcanaString.Devil, ArcanaString.Lovers, ArcanaString.Hanged, ArcanaString.Devil},
                {"", "", "", "", "", "", "", "", "", "", "", ArcanaString.Strength, ArcanaString.Star, ArcanaString.Devil, ArcanaString.Fortune, ArcanaString.Priestess, ArcanaString.Hermit, ArcanaString.Empress, ArcanaString.Hierophant, ArcanaString.Empress, "-"},
                {"", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Hanged, ArcanaString.Justice, ArcanaString.Death, ArcanaString.Temperance, ArcanaString.Hierophant, ArcanaString.Fortune, ArcanaString.Strength, ArcanaString.Hierophant, ArcanaString.Priestess},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Death, ArcanaString.Emperor, ArcanaString.Hermit, ArcanaString.Priestess, ArcanaString.Chariot, ArcanaString.Devil, ArcanaString.Justice, "-"},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Temperance, ArcanaString.Emperor, ArcanaString.Strength, ArcanaString.Hierophant, ArcanaString.Chariot, ArcanaString.Magician, ArcanaString.Death},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Devil, ArcanaString.Death, ArcanaString.Strength, ArcanaString.Star, ArcanaString.Fortune, ArcanaString.Lovers},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Tower, ArcanaString.Hanged, ArcanaString.Magician, ArcanaString.Hermit, ArcanaString.Hierophant},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Star, ArcanaString.Fortune, ArcanaString.Empress, ArcanaString.Strength},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Moon, ArcanaString.Empress, ArcanaString.Justice},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Sun, ArcanaString.Emperor},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Judgement}
            };

            tripleFusion = new string[21, 21]{
                {ArcanaString.Fool,ArcanaString.Chariot,ArcanaString.Sun,ArcanaString.Magician,ArcanaString.Star,ArcanaString.Judgement,ArcanaString.Emperor,ArcanaString.Sun,ArcanaString.Hermit,ArcanaString.Lovers,ArcanaString.Emperor,ArcanaString.Justice,ArcanaString.Devil,ArcanaString.Temperance,ArcanaString.Hanged,ArcanaString.Moon,ArcanaString.Judgement,ArcanaString.Strength,ArcanaString.Fortune,ArcanaString.Tower,ArcanaString.Moon},
                {"", ArcanaString.Magician,ArcanaString.Justice,ArcanaString.Hermit,ArcanaString.Moon,ArcanaString.Fortune,ArcanaString.Chariot,ArcanaString.Judgement,ArcanaString.Fool,ArcanaString.Strength,ArcanaString.Hanged,ArcanaString.Sun,ArcanaString.Hermit,ArcanaString.Emperor,ArcanaString.Tower,ArcanaString.Empress,ArcanaString.Temperance,ArcanaString.Fool,ArcanaString.Lovers,ArcanaString.Death,ArcanaString.Emperor},
                {"", "", ArcanaString.Priestess,ArcanaString.Justice,ArcanaString.Empress,ArcanaString.Death,ArcanaString.Fool,ArcanaString.Sun,ArcanaString.Magician,ArcanaString.Tower,ArcanaString.Hierophant,ArcanaString.Fortune,ArcanaString.Death,ArcanaString.Lovers,ArcanaString.Star,ArcanaString.Emperor,ArcanaString.Hanged,ArcanaString.Moon,ArcanaString.Devil,ArcanaString.Fool,ArcanaString.Strength},
                {"", "", "", ArcanaString.Empress,ArcanaString.Hanged,ArcanaString.Tower,ArcanaString.Temperance,ArcanaString.Priestess,ArcanaString.Moon,ArcanaString.Judgement,ArcanaString.Strength,ArcanaString.Hierophant,ArcanaString.Star,ArcanaString.Fortune,ArcanaString.Devil,ArcanaString.Tower,ArcanaString.Chariot,ArcanaString.Sun,ArcanaString.Priestess,ArcanaString.Justice,ArcanaString.Fool},
                {"", "", "", "", ArcanaString.Emperor,ArcanaString.Lovers,ArcanaString.Devil,ArcanaString.Hermit,ArcanaString.Empress,ArcanaString.Chariot,ArcanaString.Sun,ArcanaString.Chariot,ArcanaString.Fortune,ArcanaString.Hierophant,ArcanaString.Justice,ArcanaString.Fool,ArcanaString.Hermit,ArcanaString.Temperance,ArcanaString.Tower,	ArcanaString.Moon,ArcanaString.Sun},
                {"", "", "", "", "", ArcanaString.Hierophant,ArcanaString.Moon,ArcanaString.Hanged,ArcanaString.Sun,ArcanaString.Star,ArcanaString.Strength,ArcanaString.Magician,ArcanaString.Tower,ArcanaString.Chariot,ArcanaString.Priestess,ArcanaString.Star,ArcanaString.Devil,ArcanaString.Fortune,ArcanaString.Strength,ArcanaString.Hermit,ArcanaString.Temperance},
                {"", "", "", "", "", "", ArcanaString.Lovers,ArcanaString.Hierophant,ArcanaString.Judgement,ArcanaString.Hanged,ArcanaString.Tower,ArcanaString.Hermit,ArcanaString.Sun,ArcanaString.Priestess,ArcanaString.Strength,ArcanaString.Sun,ArcanaString.Magician,ArcanaString.Hermit,ArcanaString.Death,ArcanaString.Star,ArcanaString.Devil},
                {"", "", "", "", "", "", "", ArcanaString.Chariot,ArcanaString.Fool,ArcanaString.Emperor,ArcanaString.Lovers,ArcanaString.Death,ArcanaString.Devil,ArcanaString.Magician,ArcanaString.Moon,ArcanaString.Temperance,ArcanaString.Emperor,ArcanaString.Empress,ArcanaString.Justice,ArcanaString.Strength,"-"},
                {"", "", "", "", "", "", "", "", ArcanaString.Justice,ArcanaString.Strength,ArcanaString.Priestess,ArcanaString.Tower,ArcanaString.Lovers,ArcanaString.Temperance,ArcanaString.Emperor,ArcanaString.Fortune,ArcanaString.Moon,ArcanaString.Hierophant,ArcanaString.Hanged,ArcanaString.Hierophant,"-"},
                {"", "", "", "", "", "", "", "", "", ArcanaString.Hermit,ArcanaString.Hanged,ArcanaString.Devil,ArcanaString.Fool,ArcanaString.Moon,ArcanaString.Sun,ArcanaString.Priestess,ArcanaString.Death,ArcanaString.Justice,ArcanaString.Empress,ArcanaString.Fortune,ArcanaString.Magician},
                {"", "", "", "", "", "", "", "", "", "", ArcanaString.Fortune,ArcanaString.Moon,ArcanaString.Justice,ArcanaString.Fool,ArcanaString.Death,ArcanaString.Fool,ArcanaString.Sun,ArcanaString.Devil,ArcanaString.Fool,ArcanaString.Magician,ArcanaString.Empress},
                {"", "", "", "", "", "", "", "", "", "", "", ArcanaString.Strength,ArcanaString.Empress,ArcanaString.Lovers,ArcanaString.Tower,ArcanaString.Hanged,ArcanaString.Fool,ArcanaString.Judgement,ArcanaString.Star,ArcanaString.Emperor,"-"},
                {"", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Hanged,ArcanaString.Tower,ArcanaString.Hierophant,ArcanaString.Chariot,ArcanaString.Priestess,ArcanaString.Moon,ArcanaString.Temperance,ArcanaString.Temperance,ArcanaString.Hermit},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Death,ArcanaString.Moon,ArcanaString.Strength,ArcanaString.Star,ArcanaString.Temperance,ArcanaString.Sun,ArcanaString.Empress,"-"},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Temperance,ArcanaString.Magician,ArcanaString.Lovers,ArcanaString.Chariot,ArcanaString.Death,ArcanaString.Empress,ArcanaString.Hierophant},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Devil,ArcanaString.Fool,ArcanaString.Death,ArcanaString.Hierophant,ArcanaString.Fool,ArcanaString.Tower},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Tower,ArcanaString.Priestess,ArcanaString.Judgement,ArcanaString.Devil,ArcanaString.Fortune},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Star,ArcanaString.Magician,ArcanaString.Tower,ArcanaString.Hanged},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Moon,ArcanaString.Judgement,ArcanaString.Priestess},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Sun,ArcanaString.Lovers},
                {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ArcanaString.Judgement},               
            };

            #endregion

            #region Special Fusions

            specialFusion = new Dictionary<string, string[]>();
            specialFusion.Add("Black Frost", new string[] { "Jack Frost", "Pyro Jack", "King Frost" });
            specialFusion.Add("Alice", new string[] { "Nebiros", "Belial" });
            specialFusion.Add("Pale Rider", new string[] { "White Rider", "Black Rider", "Red Rider" });
            specialFusion.Add("Norn", new string[] { "Clotho", "Lachesis", "Atropos" });
            specialFusion.Add("Michael", new string[] { "Uriel", "Rapael", "Gabriel" });
            specialFusion.Add("Shiva", new string[] { "Rangda", "Barong" });
            specialFusion.Add("Beelzebub", new string[] { "Astaroth", "Baal Zebul" });
            specialFusion.Add("Ardha", new string[] { "Shiva", "Parvati" });
            specialFusion.Add("Zeus", new string[] { "Warrior Zeus", "Seth" });
            specialFusion.Add("Lucifer", new string[] { "Metatron", "Ardha", "Zeus" });

            #endregion
        }

        #region Utilities

        public string GetDoubleFusionArcana(int row, int col)
        {
            return doubleFusion[row, col];
        }

        public string GetTripleFusionArcana(int row, int col)
        {
            return tripleFusion[row, col];
        }

        public List<string[]> DoubleFusionsByArcana(Arcana arcana)
        {
            List<string[]> combinations = new List<string[]>();

            var rowLowerLimit = doubleFusion.GetLowerBound(0);
            var rowUpperLimit = doubleFusion.GetUpperBound(0);

            var colLowerLimit = doubleFusion.GetLowerBound(1);
            var colUpperLimit = doubleFusion.GetUpperBound(1);

            for (int row = rowLowerLimit; row <= rowUpperLimit; row++)
            {
                for (int col = colLowerLimit; col <= colUpperLimit; col++)
                {
                    if (doubleFusion[row, col] == arcana.ToString())
                    {
                        string[] item = new string[] { ((Arcana)row).ToString(), ((Arcana)col).ToString() };

                        if (!item.Contains(arcana.ToString()))
                        {
                            combinations.Add(item);
                        }
                    }
                }
            }

            return combinations;
        }

        public List<string[]> TripleFusionByArcana(Arcana arcana)
        {
            List<string[]> combinations = new List<string[]>();

            var rowLowerLimit = tripleFusion.GetLowerBound(0);
            var rowUpperLimit = tripleFusion.GetUpperBound(0);

            var colLowerLimit = tripleFusion.GetLowerBound(1);
            var colUpperLimit = tripleFusion.GetUpperBound(1);

            for (int row = rowLowerLimit; row <= rowUpperLimit; row++)
            {
                for (int col = colLowerLimit; col <= colUpperLimit; col++)
                {
                    if (tripleFusion[row, col] == arcana.ToString())
                    {
                        string[] item = new string[] { ((Arcana)row).ToString(), ((Arcana)col).ToString() };

                        if (!item.Contains(arcana.ToString()))
                        {
                            combinations.Add(item);
                        }
                    }
                }
            }

            return combinations;
        }

        #region Fusion Checking Utilities

        public bool ListContainsFusionCombination(List<string[]> combinations, string firstName, string secondName)
        {
            bool result = false;

            foreach (string[] combination in combinations)
            {
                if (combination.Count() == 2)
                {
                    if (combination.Contains(firstName) && combination.Contains(secondName))
                    {
                        result = true;
                        return result;
                    }
                }
            }

            return result;
        }

        public bool ListContainsFusionCombination(List<string[]> combinations, string firstName, string secondName, string thirdName)
        {
            bool result = false;

            foreach (string[] combination in combinations)
            {
                if (combination.Count() == 3)
                {
                    if (combination.Contains(firstName) && combination.Contains(secondName) && combination.Contains(thirdName))
                    {
                        result = true;
                        return result;
                    }
                }
            }

            return result;
        }

        #endregion

        #region Special Fusion Utilities

        public bool SpecialFusionOnly(string personaName)
        {
            bool result = false;

            result = specialFusion.ContainsKey(personaName);

            return result;
        }

        public bool SpecialFusionOnly(string[] combination)
        {
            bool result = false;

            List<string> searchTerms = new List<string>(combination);
            int matchesFound;

            foreach (var item in specialFusion)
            {
                List<string> fusionComponents = new List<string>(item.Value);

                matchesFound = 0;

                foreach (string term in searchTerms)
                {
                    if (fusionComponents.Contains(term))
                    {
                        matchesFound++;
                    }
                }

                if (matchesFound == searchTerms.Count && matchesFound == fusionComponents.Count)
                {
                    result = true;
                    return result;
                }
            }

            return result;
        }

        public List<string[]> SpecialFusionCombinationByPersona(string personaName)
        {
            List<string[]> result = new List<string[]>();

            try
            {
                result.Add(specialFusion[personaName]);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("No fusion result possible.");
            }

            return result;
        }

        public string SpecialFusionResultantPersonaName(string[] combination)
        {
            string result = String.Empty;

            List<string> fusionCombination = new List<string>(combination);
            int matchesFound;
            bool fusionSuccess = false;

            foreach (var item in specialFusion)
            {
                List<string> fusionComponents = new List<string>(item.Value);

                matchesFound = 0;

                foreach (string component in fusionCombination)
                {
                    if (fusionComponents.Contains(component))
                    {
                        matchesFound++;
                    }
                }

                if (matchesFound == fusionCombination.Count && matchesFound == fusionComponents.Count)
                {
                    result = item.Key;
                    fusionSuccess = true;
                    return result;
                }
            }

            if (!fusionSuccess)
            {
                throw new ArgumentException("No fusion result possible.");
            }

            return result;
        }

        #endregion

        #endregion
    }
}