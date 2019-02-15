using Cotacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cotacao.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetUsuarios()
        {
            var usuarios = db.Users.ToList();
            List<RegisterViewModel> resultado = new List<RegisterViewModel>();
            foreach (var item in usuarios)
            {
                resultado.Add(new RegisterViewModel
                {
                    Email = item.Email
                });
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}