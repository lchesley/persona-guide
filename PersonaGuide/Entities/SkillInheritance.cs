using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonaGuide.Entities
{
    public class SkillInheritance
    {
        public InheritanceType Type { get; set; }
        public bool CanInherit { get; set; }
    }
}