using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jogo_Main.Controllers
{
    public class GameOnController : Controller
    {
        
        public ActionResult Index(int nivelId)
        {

            if(nivelId == 0)
            {


            }


            return View();
        }

    }
}
