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
                    //persona.AddedHP = Convert.ToInt32(csv.GetField<string>("HP"));
                    //persona.AddedSP = Convert.ToInt32(csv.GetField<string>("SP"));
                    //persona.Arcana = (Arcana)Enum.Parse(typeof(Arcana), csv.GetField<string>("Arcana"));
                    //persona.CardExtracted = csv.GetField<string>("Card");
                    //persona.InitialLevel = Convert.ToInt32(csv.GetField<string>("Lv"));
                    //persona.IsDownloadedContent = (csv.GetField<string>("DLC") == "X") ? true : false;
                    //persona.Name = csv.GetField<string>("Persona");
                    //persona.Skills = skillModel.BuildSkillLevelsFromSkillList(csv.GetField<string>("Skills"));
                    //persona.Type = (csv.GetField<string>("Type") == "") ? PersonaType.Any : (PersonaType)Enum.Parse(typeof(PersonaType), csv.GetField<string>("Type"));
                    //persona.InheritanceMatrix = inheritanceUtilities.GetSkillInheritance(persona.Type);
                    list.Add(persona);
                }
            }

            return list;
        }
    }
}