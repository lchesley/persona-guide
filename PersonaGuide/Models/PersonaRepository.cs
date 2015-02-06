using PersonaGuide.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PersonaGuide.Models
{
    public class PersonaRepository
    {
        StreamReader personaReader;
        StreamReader skillReader;

        SkillModel skillModel;
        PersonaModel personaModel;
        FusionModel fusionModel;
        FusionUtilities fusionUtilities;

        public PersonaRepository()
        {
            personaReader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/FusionGuide.csv"));
            skillReader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/SkillList.csv"));

            fusionUtilities = new FusionUtilities();
            skillModel = new SkillModel(skillReader);
            personaModel = new PersonaModel(personaReader, skillModel);
            fusionModel = new FusionModel(personaModel, fusionUtilities);
        }

        public List<Persona> GetPersonaList()
        {            
            return personaModel.GetPersonaList();
        }

        public List<Persona> GetPersonaListByArcana(Arcana arcana)
        {
            return personaModel.GetPersonaList(arcana);
        }

        public List<Persona> GetPersonaListBySkill(string skillName)
        {
            Skill skill = skillModel.GetSkillBySkillName(skillName);
            return personaModel.GetPersonaList(skill);
        }

        public Persona GetPersonaByPersonaName(string name)
        {
            return personaModel.GetPersonaByName(name);
        }        

        public List<Skill> GetSkills()
        {
            return skillModel.GetSkills();
        }

        public List<Skill> GetSkillsByType(string type)
        {
            return skillModel.GetSkills().Where(o => o.Type == type).ToList<Skill>();
        }

        public List<string> GetSkillNames()
        {
            List<string> skillNames = new List<string>();

            skillNames.Add("---Select a Skill---");

            skillNames.AddRange(skillModel.GetSkills().Select(o => o.Name).OrderBy(o => o).ToList<string>());

            return skillNames;
        }

        public List<string> GetPersonaNames()
        {
            List<string> personaNames = new List<string>();

            personaNames.Add("---Select a Persona---");

            personaNames.AddRange(personaModel.GetPersonaList().Select(o => o.Name).OrderBy(o => o).ToList<string>());

            return personaNames;
        }

        public FusionResults GetFusionResults(string first, string second, string third)
        {
            FusionResults model = new FusionResults();

            //Get the chosen persona values.
            if (!String.IsNullOrEmpty(first) && first.IndexOf("Select") == -1)
            {
                model.FirstPersona = personaModel.GetPersonaByName(first);
            }

            if (!String.IsNullOrEmpty(second) && second.IndexOf("Select") == -1)
            {
                model.SecondPersona = personaModel.GetPersonaByName(second);
            }

            if (!String.IsNullOrEmpty(third) && third.IndexOf("Select") == -1)
            {
                model.ThirdPersona = personaModel.GetPersonaByName(third);
            }

            //Do an appropriate fusion, based on the number of persona selected.
            if (model.FirstPersona != null && model.SecondPersona != null && model.ThirdPersona != null)
            {
                model.ResultPersona = fusionModel.FuseThreeKnownPersona(model.FirstPersona, model.SecondPersona, model.ThirdPersona);
            }
            else if(model.FirstPersona != null && model.SecondPersona != null)
            {
                model.ResultPersona = fusionModel.FuseTwoKnownPersona(model.FirstPersona, model.SecondPersona);
            }

            return model;
        }

        public FusionSearchResults GetFusionCombinations(string result, string first, string second, int level)
        {
            FusionSearchResults model = new FusionSearchResults();
            if (level > 0)
            {
                model.CappedLevel = level;
            }
            else
            {
                model.CappedLevel = personaModel.GetPersonaByName(result).InitialLevel;
            }

            if(!String.IsNullOrEmpty(result) && result.IndexOf("Select") == -1)
            {
                model.ResultPersona = personaModel.GetPersonaByName(result);

                if(!String.IsNullOrEmpty(first) && first.IndexOf("Select") == -1)
                {
                    model.FirstPersona = personaModel.GetPersonaByName(first);

                    if(!String.IsNullOrEmpty(second) && second.IndexOf("Select") == -1)
                    {
                        model.SecondPersona = personaModel.GetPersonaByName(second);

                        if(model.CappedLevel > 0)
                        {
                            model.Matches = fusionModel.HowDoIFuse(model.ResultPersona, model.FirstPersona, model.SecondPersona, model.CappedLevel);
                        }
                        else
                        {
                            model.Matches = fusionModel.HowDoIFuse(model.ResultPersona, model.FirstPersona, model.SecondPersona);
                        }
                    }
                    else
                    {
                        if (model.CappedLevel > 0)
                        {
                            model.Matches = fusionModel.HowDoIFuse(model.ResultPersona, model.FirstPersona, model.CappedLevel);
                        }
                        else
                        {
                            model.Matches = fusionModel.HowDoIFuse(model.ResultPersona, model.FirstPersona);
                        }
                    }
                }
                else
                {
                    if(model.CappedLevel > 0)
                    {                        
                        model.Matches = fusionModel.HowDoIFuse(model.ResultPersona, model.CappedLevel);
                    }
                    else
                    {
                        model.Matches = fusionModel.HowDoIFuse(model.ResultPersona);
                    }
                }
            }

            return model;
        } 
    }
}