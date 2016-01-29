﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonaManager.Entities
{
    public class Persona
    {
        public string Name { get; set; }
        public Arcana Arcana { get; set; }
        public int InitialLevel { get; set; }
        public int HPIncrease { get; set; }
        public int SPIncrease { get; set; }
        public bool IsDownloadedContent { get; set; }
        public List<LearnedSkill> LearnedSkills { get; set; }
        public Skill ExtractedSkill { get; set; }
        public PersonaInheritanceType InheritanceType { get; set; }
        public List<SkillInheritance> InheritableSkillTypes { get; set; }
        public string SkillsList
        {
            get
            {
                string temp = String.Empty;

                if (LearnedSkills != null && LearnedSkills.Count > 0)
                {
                    foreach (LearnedSkill skill in LearnedSkills)
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
    }
}