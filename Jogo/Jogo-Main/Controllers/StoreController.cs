using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jogo_Main.Models;
using WebMatrix.WebData;

namespace Jogo_Main.Controllers
{
    public class StoreController : Controller
    {
        //
        // GET: /Store/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Comprar()
        {

            using (var db = new UsersContext())
            {
                var id = WebSecurity.GetUserId(User.Identity.Name);

                var user = db.UserProfiles.FirstOrDefault(x => x.UserId == id);

                if (user != null && user.Saldo > 100)
                {
                    user.Saldo -= 100;
                }
                else
                {
                     return Json(new {Sucesso = false});
             }

                db.SaveChanges();
            }

            return Json(new { Sucesso = true });
        }

    }
}
