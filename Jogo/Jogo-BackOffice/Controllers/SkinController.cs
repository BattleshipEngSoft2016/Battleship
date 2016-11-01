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

        public JsonResult Cadastrar(SkinModel vm)
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

        [HttpPost]
        public JsonResult Excluir(int id)
        {

            using (var db = new SkinsContext())
            {
                var item = db.Skins.FirstOrDefault(x => x.Id == id);

                db.Skins.Remove(item);

                db.SaveChanges();
            }

            return Json(new { Sucesso = true });


        }

        public JsonResult Editar(int id, SkinModel vm)
        {

            try
            {
                using (var db = new SkinsContext())
                {
                    var item = db.Skins.FirstOrDefault(x => x.Id == id);

                    if (item != null)
                    {
                        item.AtulizarDominio(vm);

                        db.SaveChanges();
                    }
                    else
                    {
                        return Json(new { Sucesso = false });
                    }

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
