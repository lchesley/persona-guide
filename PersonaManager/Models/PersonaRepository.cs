using PersonaManager.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PersonaManager.Models
{
    public class PersonaRepository
    {
        StreamReader fusionReader;
        StreamReader skillsReader;

        SkillModel skillModel;
        PersonaModel personaModel;
        InheritanceModel inheritanceModel;

        public PersonaRepository()
        {
            fusionReader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/FusionGuide.csv"));
            skillsReader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/SkillList.csv"));

            skillModel = new SkillModel(skillsReader);
            inheritanceModel = new InheritanceModel();
            personaModel = new PersonaModel(fusionReader, skillModel, inheritanceModel);
        }

        public List<Persona> GetPersonaList()
        {
            return personaModel.GetPersonaList();
        }
    }
}