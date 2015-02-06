using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonaGuide.Entities
{
    public class FusionResults
    {
        public Persona FirstPersona { get; set; }
        public Persona SecondPersona { get; set; }
        public Persona ThirdPersona { get; set; }
        public Persona ResultPersona { get; set; }
    }
}