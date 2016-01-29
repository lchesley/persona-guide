using CsvHelper;
using PersonaManager.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PersonaManager.Models
{
    public class SkillModel
    {
        List<Skill> skillList;

        public SkillModel(StreamReader reader)
        {
            skillList = BuildSkillList(reader);
        }

        private List<Skill> BuildSkillList(StreamReader reader)
        {
            List<Skill> list = new List<Skill>();

            using (TextReader textReader = reader)
            {
                var csv = new CsvReader(textReader);
                while (csv.Read())
                {
                    Skill skill = new Skill();
                    //skill.Cost = csv.GetField<string>("Cost");
                    //skill.Description = csv.GetField<string>("Description");
                    //skill.CanPassDown = (csv.GetField<string>("Heritable") == "X") ? true : false;
                    //skill.Name = csv.GetField<string>("Skill");
                    //skill.Type = csv.GetField<string>("Type");
                    //skill.SkillType = (InheritanceType)Enum.Parse(typeof(InheritanceType), csv.GetField<string>("InheritanceType"));
                    list.Add(skill);
                }
            }

            return list;
        }

        public List<Skill> GetSkillList()
        {
            return skillList;
        }

    }
}