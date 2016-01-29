using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonaManager.Entities
{
    public class SkillInheritance
    {
        public SkillInheritanceType Type { get; set; }
        public bool CanInherit { get; set; }
    }
}