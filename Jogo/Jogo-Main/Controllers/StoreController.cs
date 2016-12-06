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

        [HttpGet]
        public JsonResult Comprar(int Id)
        {

            UserProfile user = null;

            bool retorno = true;

            using (var db = new UsersContext())
            {
                var id = WebSecurity.GetUserId(User.Identity.Name);

                user = db.UserProfiles.FirstOrDefault(x => x.UserId == id);

                if (user != null && user.Saldo > 100)
                {
                    user.Saldo -= 100;
                }
                else
                {
                    retorno = false;
                }

                db.SaveChanges();
            }

            if(retorno)
            using (var db = new UserSkisContext())
            {
                var itemId = db.UserSkins.Any() ? db.UserSkins.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1 : 1;

                db.UserSkins.Add(new UserSkin(user.UserId, Id) {Id = itemId});

                db.SaveChanges();
            }


            return Json(new { Sucesso = retorno }, JsonRequestBehavior.AllowGet);
        }

    }
}
