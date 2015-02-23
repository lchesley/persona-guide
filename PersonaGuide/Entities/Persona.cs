using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PersonaGuide.Entities
{
    public class Persona
    {
        [DisplayName("Persona")]
        public string Name { get; set; }
        [DisplayName("Arcana")]
        public Arcana Arcana { get; set; }
        [DisplayName("Level")]
        public int InitialLevel { get; set; }
        [DisplayName("HP")]
        public int AddedHP { get; set; }
        [DisplayName("SP")]
        public int AddedSP { get; set; }
        [DisplayName("DLC")]
        public bool IsDownloadedContent { get; set; }
        [DisplayName("Card")]
        public string CardExtracted { get; set; }
        [DisplayName("Inheritance Type")]
        public PersonaType Type { get; set; }
        public List<PersonaSkills> Skills { get; set; }
        public List<SkillInheritance> InheritanceMatrix { get; set; }
        [DisplayName("Skills")]
        public string SkillsList
        {
            get
            {
                string temp = String.Empty;

                if (Skills != null && Skills.Count > 0)
                {
                    foreach (PersonaSkills skill in Skills)
                    {
                        if (skill.LevelLearned == 1)
                        {
                            temp += String.Format("{0}, ", skill.Skill.Name);
                        }
                        else
                        {
                            temp += String.Format("{0}({1}), ", skill.Skill.Name, skill.LevelLearned);
                        }
                    }
                }

                return temp.Remove(temp.Length - 2);
            }
        }
        [DisplayName("Inheritable Skill Types")]
        public string InheritableSkillTypes
        {
            get
            {
                string temp = String.Empty;

                if(InheritanceMatrix != null && InheritanceMatrix.Count > 0)
                {
                    foreach(SkillInheritance item in InheritanceMatrix)
                    {
                        temp += String.Format("{0}, ", item.Type);
                    }
                }

                return temp.Remove(temp.Length - 2);
            }
        }


        public override bool Equals(object obj)
        {
            Persona persona = obj as Persona;
            if (persona == null)
            {
                return false;
            }
            else
            {
                return Name.Equals(persona.Name);
            }
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}