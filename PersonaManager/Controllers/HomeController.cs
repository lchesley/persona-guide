using PersonaManager.Entities;
using PersonaManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonaManager.Controllers
{
    public class HomeController : Controller
    {
        private PersonaRepository repository = new PersonaRepository();

        public ActionResult Index()
        {
            List<Persona> list = repository.GetPersonaList();
            return View((IEnumerable<Persona>)list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}