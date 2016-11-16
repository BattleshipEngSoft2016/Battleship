using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jogo_BackOffice.Filters;
using Jogo_BackOffice.Models;
using Newtonsoft.Json;

namespace Jogo_BackOffice.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
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

        [HttpPost]
        public JsonResult Cadastrar(SkinModel vm)
        {
            try
            {


                Skin skinRetorno;


                using (var db = new SkinsContext())
                

                    if (db.Skins.Any())
                    {
                        var id = db.Skins.OrderByDescending(x => x.Id).First().Id;

                        var item = new Skin(vm) { Id = id + 1 };

                        db.Skins.Add(item);

                        db.SaveChanges();

                        skinRetorno = item;

                    }
                    else
                    {
                        var item = new Skin(vm);

                        db.Skins.Add(item);

                        db.SaveChanges();

                        skinRetorno = item;

                    }

                return Json(new { Sucesso = true, Mensagem= "Sucesso", Dados = skinRetorno });
        
            }
            catch (Exception)
            {
                return Json(new { Sucesso = false, Mensagem = "Erro" });
            }
        }

       
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

        [HttpPost]
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
