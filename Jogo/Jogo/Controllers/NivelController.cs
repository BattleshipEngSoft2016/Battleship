using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jogo.Models;

namespace Jogo.Controllers
{
    public class NivelController : Controller
    {
        //
        // GET: /NIvel/

        public ActionResult Index()
        {

            ViewBag.Niveis = new NiveisContext().Niveis;

            return View();
        }


        public JsonResult Cadastrar()
        {
            try
            {
                using (var ctx = new NiveisContext())
                {
                    Nivel stud = new Nivel() { };
                    ctx.Niveis.Add(stud);
                    ctx.SaveChanges();
                }

                return Json(new { Sucesso = true });

            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false });
            }

        }

    }
}
