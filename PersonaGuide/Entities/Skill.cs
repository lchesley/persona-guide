using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PersonaGuide.Entities
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("Sub-Type")]
        public string Type { get; set; }
        public string Cost { get; set; }
        [DisplayName("Inheritable")]
        public bool CanPassDown { get; set; }
        [DisplayName("Type")]
        public InheritanceType SkillType { get; set; }

        public override bool Equals(object obj)
        {
            Skill skill = obj as Skill;
            if (skill == null)
            {
                return false;
            }
            else
            {
                return Name.Equals(skill.Name);
            }
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}