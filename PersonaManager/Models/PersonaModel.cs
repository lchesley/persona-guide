﻿using CsvHelper;
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
        SkillModel skillModel;
        InheritanceModel inheritanceModel;

        public PersonaModel(StreamReader reader, SkillModel skillModel, InheritanceModel inheritanceModel)
        {
            this.skillModel = skillModel;
            this.inheritanceModel = inheritanceModel;
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
                    persona.ExtractedSkill = skillModel.GetSkillBySkillName(csv.GetField<string>("Card"));
                    persona.InitialLevel = Convert.ToInt32(csv.GetField<string>("Lv"));
                    persona.IsDownloadedContent = (csv.GetField<string>("DLC") == "X") ? true : false;
                    persona.Name = csv.GetField<string>("Persona");
                    persona.LearnedSkills = skillModel.GetLearnedSkillsFromSkillList(csv.GetField<string>("Skills"));
                    persona.InheritanceType = (csv.GetField<string>("Type") == "") ? PersonaInheritanceType.Any : (PersonaInheritanceType)Enum.Parse(typeof(PersonaInheritanceType), csv.GetField<string>("Type"));
                    persona.InheritableSkillTypes = inheritanceModel.GetSkillInheritanceByPersonaInheritanceType(persona.InheritanceType);
                    list.Add(persona);
                }
            }

            return list;
        }
    }
}