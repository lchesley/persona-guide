using CsvHelper;
using PersonaGuide.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PersonaGuide.Models
{
    public class PersonaModel
    {
        List<Persona> personaList;
        SkillModel skillModel;

        public PersonaModel(StreamReader reader, SkillModel skillModel)
        {
            this.skillModel = skillModel;
            personaList = BuildPersonaList(reader);
        }

        public List<Persona> BuildPersonaList(StreamReader reader)
        {
            List<Persona> list = new List<Persona>();

            using (TextReader textReader = reader)
            {
                var csv = new CsvReader(textReader);
                while (csv.Read())
                {
                    Persona persona = new Persona();
                    persona.AddedHP = Convert.ToInt32(csv.GetField<string>("HP"));
                    persona.AddedSP = Convert.ToInt32(csv.GetField<string>("SP"));
                    persona.Arcana = (Arcana)Enum.Parse(typeof(Arcana), csv.GetField<string>("Arcana"));
                    persona.CardExtracted = csv.GetField<string>("Card");
                    persona.InitialLevel = Convert.ToInt32(csv.GetField<string>("Lv"));
                    persona.IsDownloadedContent = (csv.GetField<string>("DLC") == "X") ? true : false;
                    persona.Name = csv.GetField<string>("Persona");
                    persona.Skills = skillModel.BuildSkillLevelsFromSkillList(csv.GetField<string>("Skills"));
                    list.Add(persona);
                }
            }

            return list;
        }

        public List<Persona> GetPersonaList()
        {
            return personaList;
        }

        public List<Persona> GetPersonaList(Arcana arcana)
        {
            return personaList.Where(o => o.Arcana == arcana).OrderBy(o => o.InitialLevel).ToList<Persona>();
        }

        public List<Persona> GetPersonaList(Skill skill)
        {
            List<Persona> list = new List<Persona>();
            List<Persona> temp = new List<Persona>();

            temp = GetPersonaList();

            foreach (Persona item in temp)
            {
                List<PersonaSkills> skills = item.Skills.Where(o => o.Skill.Equals(skill)).ToList<PersonaSkills>();

                if (skills != null && skills.Count > 0)
                {
                    list.Add(item);
                }
            }

            return list;
        }

        public Persona GetPersonaByName(string name)
        {
            Persona result = new Persona();

            result = personaList.Where(o => o.Name == name).FirstOrDefault();

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}