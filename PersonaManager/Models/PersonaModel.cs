using CsvHelper;
using PersonaManager.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PersonaManager.Models
{
    public class PersonaModel
    {
        List<Persona> personaList;

        public PersonaModel(StreamReader reader)
        {            
            personaList = BuildPersonaList(reader);
        }

        public List<Persona> GetPersonaList()
        {
            return personaList;
        }

        protected List<Persona> BuildPersonaList(StreamReader reader)
        {
            List<Persona> list = new List<Persona>();
            
            using (TextReader textReader = reader)
            {
                var csv = new CsvReader(textReader);
                while (csv.Read())
                {
                    Persona persona = new Persona();
                    persona.HPIncrease = Convert.ToInt32(csv.GetField<string>("HP"));
                    persona.SPIncrease = Convert.ToInt32(csv.GetField<string>("SP"));
                    persona.Arcana = (Arcana)Enum.Parse(typeof(Arcana), csv.GetField<string>("Arcana"));
                    //persona.ExtractedSkill = csv.GetField<string>("Card");
                    persona.InitialLevel = Convert.ToInt32(csv.GetField<string>("Lv"));
                    persona.IsDownloadedContent = (csv.GetField<string>("DLC") == "X") ? true : false;
                    persona.Name = csv.GetField<string>("Persona");
                    //persona.LearnedSkills = skillModel.BuildSkillLevelsFromSkillList(csv.GetField<string>("Skills"));
                    persona.InheritanceType = (csv.GetField<string>("Type") == "") ? PersonaInheritanceType.Any : (PersonaInheritanceType)Enum.Parse(typeof(PersonaInheritanceType), csv.GetField<string>("Type"));
                    //persona.InheritableSkillTypes = inheritanceUtilities.GetSkillInheritance(persona.InheritanceType);
                    list.Add(persona);
                }
            }

            return list;
        }
    }
}