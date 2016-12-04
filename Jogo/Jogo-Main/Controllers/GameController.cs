using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jogo_Main.Models;
using Newtonsoft.Json;

namespace Jogo_Main.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/

        public ActionResult Index()
        {
            UserProfile user = null;

            Tabuleiro tabuleiro = null;

            Skin skin = null;

            Nivel nivel = null;

            using (var db = new UsersContext())
            {
                user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == User.Identity.Name.ToLower());
            }

            using (var db = new TabuleirosContext())
            {
                tabuleiro = db.Tabuleiros.Where(x => x.UserId == user.UserId).OrderByDescending(x => x.Id).FirstOrDefault();
            }

            using (var db = new SkinsContext())
            {
                skin = db.Skins.FirstOrDefault(x => x.Id == tabuleiro.SkinId);

            }
            using (var db = new NiveisContext())
            {
                nivel = db.Niveis.FirstOrDefault(x => x.Id == tabuleiro.NivelId);
            }


            ViewBag.Dados = tabuleiro.Dados;

            ViewBag.Skin = JsonConvert.SerializeObject(skin);

            ViewBag.Nivel = JsonConvert.SerializeObject(nivel);

            ViewBag.Tabuleiro = tabuleiro.Dados;


            return View();
        }



    }
}
