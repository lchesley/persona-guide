using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonaGuide.Entities
{
    public class FusionSearchResults
    {
        public Persona ResultPersona { get; set; }
        public Persona FirstPersona { get; set; }
        public Persona SecondPersona { get; set; }
        public int CappedLevel { get; set; }
        public List<string[]> Matches { get; set; }

        public string ConvertToString(string[] combination)
        {
            string temp = String.Empty;

            foreach(string item in combination)
            {
                temp += item + " + ";
            }

            temp = temp.Remove(temp.Length - 3);

            return temp;
        }
    }    
}