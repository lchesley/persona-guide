using PersonaGuide.Entities;
using PersonaGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonaGuide.Controllers
{
    public class HomeController : Controller
    {
        private PersonaRepository repository = new PersonaRepository();

        // GET : Persona Listing
        public ActionResult Index()
        {
            List<Persona> list = repository.GetPersonaList();            
            return View((IEnumerable<Persona>)list);
        }        

        // GET: Persona Listing By Skill
        [HttpPost]        
        public ActionResult Index(string skill)
        {
            List<Persona> list = repository.GetPersonaListBySkill(skill);                        
            return PartialView("_PersonaList", (IEnumerable<Persona>)list);
        }

        // GET: Persona Listing By Arcana
        [ActionName("IndexByArcana")]        
        public ActionResult IndexByArcana(string arcana)
        {
            List<Persona> list = repository.GetPersonaListByArcana((Arcana)Enum.Parse(typeof(Arcana), arcana));                        
            return PartialView("_PersonaList", (IEnumerable<Persona>)list);
        }

        // GET: Persona Listing By Inheritance Type.
        [ActionName("IndexByType")]
        public ActionResult IndexByType(string inheritanceType)
        {
            List<Persona> list = repository.GetPersonaListByType((PersonaType)Enum.Parse(typeof(PersonaType), inheritanceType));
            return PartialView("_PersonaList", (IEnumerable<Persona>)list);
        }

        // GET: Persona/Details/Ukobach
        public ActionResult Details(string id)
        {
            Persona item = repository.GetPersonaByPersonaName(id);
            return View(item);
        }

        // GET: How Do I Fuse?
        public ActionResult HowDoIFuse()
        {           
            FusionSearchResults results = new FusionSearchResults();
            GetPersonaNames();
            return View(results);
        }

        // GET: How Do I Fuse?
        [ActionName("FuseByName")]
        public ActionResult HowDoIFuse(string result)
        {
            GetPersonaNames();
            FusionSearchResults results = new FusionSearchResults();
            results = repository.GetFusionCombinations(result, null, null, 0);
            return View("HowDoIFuse", results);
        }

        // POST: How Do I Fuse?
        [HttpPost]
        [ActionName("FuseByParameters")]
        public ActionResult HowDoIFuse(int level, string first, string second, string result)
        {
            GetPersonaNames();
            FusionSearchResults results = new FusionSearchResults();
            results = repository.GetFusionCombinations(result, first, second, level);
            return View("HowDoIFuse", results);
        }

        // GET : Fusion
        public ActionResult Fusion()
        {
            GetPersonaNames();
            FusionResults results = new FusionResults();

            return View(results);
        }

        // GET : Fusion With First Persona Chosen.
        [ActionName("FusionWith")]
        public ActionResult Fusion(string first)
        {            
            FusionResults model = new FusionResults();
            GetPersonaNames();
            try
            {
                model = repository.GetFusionResults(first, null, null);
            }
            catch (Exception ex)
            {
                ViewData.ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View("Fusion", model);
        }

        // POST: Fusion
        [HttpPost]
        [ActionName("FullFusion")]
        public ActionResult Fusion(string first, string second, string third)
        {            
            FusionResults model = new FusionResults();
            GetPersonaNames();
            try
            {
                model = repository.GetFusionResults(first, second, third);
            }
            catch (Exception ex)
            {
                ViewData.ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View("Fusion", model);
        }

        // GET: Skills
        public ActionResult Skills()
        {
            List<Skill> list = repository.GetSkills();
            return View((IEnumerable<Skill>)list);
        }

        // GET: Skills By Type
        [ActionName("SkillsByType")]
        public ActionResult Skills(string type)
        {
            List<Skill> list = repository.GetSkillsByType(type);
            return View("Skills", (IEnumerable<Skill>)list);
        }

        public ActionResult ShowBySkill(string skill)
        {
            List<Persona> list = repository.GetPersonaListBySkill(skill);           

            return View("Index", (IEnumerable<Persona>)list);
        }

        public JsonResult GetPersonaNames(string term)
        {
            var result = repository.GetPersonaNames().Where(o => o.StartsWith(term, true, System.Globalization.CultureInfo.InvariantCulture));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPersonaSkills(string term)
        {
            var result = repository.GetSkillNames().Where(o => o.StartsWith(term, true, System.Globalization.CultureInfo.InvariantCulture));
            return Json(result, JsonRequestBehavior.AllowGet);
        }        

        public JsonResult GetPersonaArcana(string term)
        {
            var result = Enum.GetNames(typeof(Arcana)).Where(o => o.StartsWith(term, true, System.Globalization.CultureInfo.InvariantCulture));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPersonaTypes(string term)
        {
            var result = Enum.GetNames(typeof(PersonaType)).Where(o => o.StartsWith(term, true, System.Globalization.CultureInfo.InvariantCulture));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public void GetSkillsList()
        {
            SelectList Skills = new SelectList(repository.GetSkillNames());
            ViewData["Skills"] = Skills;
        }

        public void GetPersonaNames()
        {
            SelectList Names = new SelectList(repository.GetPersonaNames());
            ViewData["Names"] = Names;
        }
    }
}