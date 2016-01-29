using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonaManager.Entities
{
    public class Persona
    {
        public string Name { get; set; }
        public Arcana Arcana { get; set; }
        public int InitialLevel { get; set; }
        public int HPIncrease { get; set; }
        public int SPIncrease { get; set; }
        public string SkillsList { get; set; }
        public List<LearnedSkill> LearnedSkills { get; set; }
        public Skill ExtractedSkill { get; set; }     
    }
}