using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Jogo_BackOffice.Filters;
using Jogo_BackOffice.Models;
using Newtonsoft.Json;

namespace Jogo_BackOffice.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
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
            try
            {
                Nivel nivelRetorno;

                using (var db = new NiveisContext())
                {

                    if(db.Niveis.Any())
                    {
                        var id = db.Niveis.OrderByDescending(x => x.Id).First().Id;

                        var item = new Nivel(vm) { Id = id + 1 };

                        db.Niveis.Add(item);

                        db.SaveChanges();

                        nivelRetorno = item;

                    }
                    else
                    {
                        var item = new Nivel(vm);

                        db.Niveis.Add(item);

                        db.SaveChanges();

                        nivelRetorno = item;

                    }
                    

                }

                return Json(new { Sucesso = true, Mensagem = "Sucesso", Dados = JsonConvert.SerializeObject(nivelRetorno) });
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Mensagem = "Erro" });
            }
         
        }

        [HttpPost]
        public JsonResult Editar(int id, NivelModel vm)
        {
            try
            {
                using (var db = new NiveisContext())
                {
                    var item = db.Niveis.FirstOrDefault(x => x.Id == id);

                    if (item != null)
                    {
                        item.AtualizarDominio(vm);

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

        public JsonResult Excluir(int id)
        {

            try
            {
                using (var db = new NiveisContext())
                {
                    foreach (var item in db.Niveis.Where(item => item.Id == id))
                        db.Niveis.Remove(item);

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