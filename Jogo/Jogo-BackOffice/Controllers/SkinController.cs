using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jogo_BackOffice.Models;
using Newtonsoft.Json;

namespace Jogo_BackOffice.Controllers
{
    public class SkinController : Controller
    {
      
        public ActionResult Index()
        {
            using (var db = new SkinsContext())
            {
                ViewBag.Skins = JsonConvert.SerializeObject(db.Skins);
                return View();
            }
        }

        public JsonResult Cadastrar()
        {
            try
            {
                using (var db = new SkinsContext())
                {
                    db.Skins.Add(new Skin() { });

                    db.SaveChanges();
                }

                return Json(new { Sucesso = true });
        
            }
            catch (Exception)
            {
                return Json(new { Sucesso = false });
            }
        }


    }
}
