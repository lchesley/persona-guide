using CsvHelper;
using PersonaGuide.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PersonaGuide.Models
{
    public class SkillModel
    {
        List<Skill> skillList;

        public SkillModel(StreamReader reader)
        {
            skillList = BuildSkillList(reader);
        }

        public List<Skill> BuildSkillList(StreamReader reader)
        {
            List<Skill> skills = new List<Skill>();

            using (TextReader textReader = reader)
            {
                var csv = new CsvReader(textReader);
                while (csv.Read())
                {
                    Skill skill = new Skill();
                    skill.Cost = csv.GetField<string>("Cost");
                    skill.Description = csv.GetField<string>("Description");
                    skill.CanPassDown = (csv.GetField<string>("Heritable") == "X") ? true : false;
                    skill.Name = csv.GetField<string>("Skill");
                    skill.Type = csv.GetField<string>("Type");
                    skills.Add(skill);
                }
            }

            return skills;
        }

        public Skill GetSkillBySkillName(string skillName)
        {
            return skillList.Where(o => o.Name == skillName).FirstOrDefault();
        }

        public List<PersonaSkills> BuildSkillLevelsFromSkillList(string skillList)
        {
            List<PersonaSkills> list = new List<PersonaSkills>();

            char[] delimiterChars = { ',' };
            string[] skills = skillList.Split(delimiterChars);

            foreach (string s in skills)
            {
                PersonaSkills item = new PersonaSkills();
                int firstBracket = s.IndexOf("(");
                int secondBracket = s.IndexOf(")");
                if (firstBracket > 0)
                {
                    item.LevelLearned = Convert.ToInt32(s.Substring(firstBracket + 1, (secondBracket - (firstBracket + 1))));
                    item.Skill = GetSkillBySkillName(s.Remove(firstBracket).Trim());
                }
                else
                {
                    item.LevelLearned = 1;
                    item.Skill = GetSkillBySkillName(s.Trim());
                }

                list.Add(item);
            }

            return list;
        }

        public List<Skill> GetSkills()
        {
            return skillList;
        }
    }
}