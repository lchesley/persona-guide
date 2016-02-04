using PersonaManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonaManager.Models
{
    public class InheritanceModel
    {
        bool[,] skillInheritance;

        public InheritanceModel()
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

        public List<SkillInheritance> GetSkillInheritanceByPersonaInheritanceType(PersonaInheritanceType type)
        {
            List<SkillInheritance> list = new List<SkillInheritance>();

            for (int i = 0; i < 13; i++)
            {
                SkillInheritance item = new SkillInheritance();
                item.Type = (SkillInheritanceType)i;
                item.CanInherit = skillInheritance[(int)type, i];
                if (item.CanInherit)
                {
                    list.Add(item);
                }
            }

            return list;
        }
    }
}