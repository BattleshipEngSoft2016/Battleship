using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jogo_Main.Models;
using Microsoft.Web.WebPages.OAuth;
using Newtonsoft.Json;
using WebMatrix.WebData;

namespace Jogo_Main.Controllers
{
    public class GameOnController : Controller
    {

        public ActionResult Index(int nivelId)
        {

            UserProfile user = null;

            List<int> skinsUser;

            using (var db = new UsersContext())
            {
                var id = WebSecurity.GetUserId(User.Identity.Name);

                user = db.UserProfiles.FirstOrDefault(x => x.UserId == id);

                if (user != null)
                {
                    ViewBag.Pontos = user.Pontos;

                    ViewBag.Saldo = user.Saldo;
                }
            }



            using (var db = new UserSkisContext())
            {
                skinsUser = db.UserSkins.Where(u => u.UserId == user.UserId).Select(x => x.SkinId).ToList();
            }

            using (var db = new NiveisContext())
            {
                ViewBag.Nivel = db.Niveis.FirstOrDefault(x => x.Id == nivelId);
            }

            using (var db = new SkinsContext())
            {
                var itens = new List<Skin>();

                var skPadrao = db.Skins.FirstOrDefault(x => x.Id == 2);
               
                var skCompradas = skinsUser.Select(i => db.Skins.FirstOrDefault(x => x.Id == i)).ToList();

                itens.Add(skPadrao);

                if (skCompradas.Any())
                    itens.AddRange(skCompradas);

                ViewBag.Skins = JsonConvert.SerializeObject(itens.Select(x => new { x.Id, x.Nome }));
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

                    if (db.Tabuleiros.Any())
                    {
                        var id = db.Tabuleiros.OrderByDescending(x => x.Id).First().Id;

                        var item = new Tabuleiro(user.UserId, nivelId, skinId, dados) {Id = id + 1};

                        db.Tabuleiros.Add(item);

                    }
                    else
                    {
                        var item = new Tabuleiro(user.UserId, nivelId, skinId, dados) { Id = 1 };

                        db.Tabuleiros.Add(item);
                    }

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
