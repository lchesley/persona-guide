using PersonaGuide.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonaGuide.Models
{
    public class InheritanceUtilities
    {
        bool[,] skillInheritance;

        public InheritanceUtilities()
        {
            #region Combination Arrays

            skillInheritance = new bool[31, 13] {
                {true, true, true, true, true, true, true, true, true, true, true, true, true},
                {true, false, false, false, false, false, false, true, true, true, true, true, true},
                {true, false, false, false, false, false, false, false, true, false, true, false, true},
                {true, true, true, true, true, true, true, true, true, true, true, true, true},
                {true, true, true, true, true, true, true, false, true, false, true, false, true},
                {false, true, true, true, true, true, true, true, true, true, true, true, true},
                {false, true, true, true, true, true, true, false, true, false, true, false, true},
                {true, true, false, true, true, true, true, true, true, true, true, true, true},
                {true, true, false, true, true, true, true, false, true, false, true, false, true},
                {false, true, false, true, true, true, true, true, true, true, true, true, true},
                {false, true, false, true, true, true, true, false, true, false, true, false, true},
                {true, false, true, true, true, true, true, true, true, true, true, true, true},
                {true, false, true, true, true, true, true, false, true, false, true, false, true},
                {false, false, true, true, true, true, true, true, true, true, true, true, true},
                {false, false, true, true, true, true, true, false, true, false, true, false, true},
                {true, true, true, true, false, true, true, true, true, true, true, true, true},
                {true, true, true, true, false, true, true, false, true, false, true, false, true},
                {false, true, true, true, false, true, true, true, true, true, true, true, true},
                {false, true, true, true, false, true, true, false, true, false, true, false, true},
                {true, true, true, false, true, true, true, true, true, true, true, true, true},
                {true, true, true, false, true, true, true, false, true, false, true, false, true},
                {false, true, true, false, true, true, true, true, true, true, true, true, true},
                {false, true, true, false, true, true, true, false, true, false, true, false, true},
                {true, true, true, true, true, true, false, true, true, true, true, true, true},
                {true, true, true, true, true, true, false, false, true, false, true, false, true},
                {false, true, true, true, true, true, false, true, true, true, true, true, true},
                {false, true, true, true, true, true, false, false, true, false, true, false, true},
                {true, true, true, true, true, false, true, true, true, true, true, true, true},
                {true, true, true, true, true, false, true, false, true, false, true, false, true},
                {false, true, true, true, true, false, true, true, true, true, true, true, true},
                {false, true, true, true, true, false, true, false, true, false, true, false, true},
            };
            
            #endregion
        }

        public List<SkillInheritance> GetSkillInheritance(PersonaType type)
        {
            List<SkillInheritance> matrix = new List<SkillInheritance>();

            for(int i=0; i < 13; i++)
            {
                SkillInheritance item = new SkillInheritance();
                item.Type = (InheritanceType)i;
                item.CanInherit = skillInheritance[(int) type, i];
                if (item.CanInherit)
                {
                    matrix.Add(item);
                }
            }
            
            return matrix;
        }
    }
}