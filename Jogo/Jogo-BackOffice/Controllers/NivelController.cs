using System.Web.Mvc;
using Jogo_BackOffice.Models;
using Newtonsoft.Json;

namespace Jogo_BackOffice.Controllers
{
    public class NivelController : Controller
    {
        //
        // GET: /Nivel/

        public ActionResult Index()
        {

            using (var db = new NiveisContext())
            {
                ViewBag.Niveis = JsonConvert.SerializeObject(db.Niveis);  
                return View();
            }

        }

        [HttpPost]
        public JsonResult Cadastrar(NivelModel vm)
        {
            
            using (var db = new NiveisContext())
            {
                db.Niveis.Add(new Nivel() { });

            }

            return Json(new { Sucesso = true });
        }
    }
}