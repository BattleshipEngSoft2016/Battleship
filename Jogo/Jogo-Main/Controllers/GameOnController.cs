using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jogo_Main.Models;
using Microsoft.Web.WebPages.OAuth;
using Newtonsoft.Json;

namespace Jogo_Main.Controllers
{
    public class GameOnController : Controller
    {

        public ActionResult Index(int nivelId)
        {
            using (var db = new SkinsContext())
            {
                ViewBag.Skins = JsonConvert.SerializeObject(db.Skins.Select(x => new { x.Id, x.Nome }));
            }


            return View();
        }

        [HttpPost]
        public JsonResult Cadastrar(int nivelId, int skinId, string dados)
        {
            try
            {
                UserProfile user = null;

                using (var db = new UsersContext())
                {
                    user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == User.Identity.Name.ToLower());
                }


                using (var db = new TabuleirosContext())
                {
                    db.Tabuleiros.Add(new Tabuleiro(user.UserId, nivelId, skinId, dados));

                    db.SaveChanges();

                }

            }
            catch (Exception)
            {

                throw;
            }

            return null;

        }

    }
}
