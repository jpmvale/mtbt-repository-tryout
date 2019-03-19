using MchRepositoryTryout.DAL;
using MchRepositoryTryout.Models;
using MchRepositoryTryout.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MchRepositoryTryout.Controllers
{
    public class HomeController : Controller
    {
        readonly List<Mch> mchs = MchService.GetMchs();
        readonly List<TU> tus = TuService.GetTu();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TableMch()
        {
            return View(mchs);
        }

        public ActionResult TableTU()
        {
            return View(tus);
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
    }
}