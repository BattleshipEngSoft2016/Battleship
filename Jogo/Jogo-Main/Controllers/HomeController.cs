using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jogo_Main.Filters;
using Jogo_Main.Models;
using Newtonsoft.Json;
using WebMatrix.WebData;

namespace Jogo_Main.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            using (var db = new UsersContext())
            {

                ViewBag.Ranking = JsonConvert.SerializeObject(db.UserProfiles.Select(x => new {x.UserName , x.Pontos}  ).OrderBy(x => x.Pontos).Take(5));

             var id = WebSecurity.GetUserId(User.Identity.Name);

             var user = db.UserProfiles.FirstOrDefault(x => x.UserId == id);

                if (user != null)
                {
                    ViewBag.Pontos = user.Pontos;

                    ViewBag.Saldo = user.Saldo;
                }
            }

            using (var db = new NiveisContext())
            {
                ViewBag.Niveis = JsonConvert.SerializeObject(db.Niveis.Select(x => new { x.Id, x.Nome }));
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
