using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jogo.Models;

namespace Jogo.Controllers
{
    public class SkinController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Skins = null;

            var julio = new UsersContext();

            var xxx = julio.UserProfiles;


            return View();
        }

        public JsonResult Cadastrar() {



            return null;
        
        }

    }
}
