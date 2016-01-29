using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonaManager.Entities
{
    public class LearnedSkill
    {
        public Skill Skill { get; set; }
        public int LevelLearned { get; set; }
    }
}