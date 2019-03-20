using MchRepositoryTryout.DAL;
using MchRepositoryTryout.Models;
using MchRepositoryTryout.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MchRepositoryTryout.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TableMch()
        {
            return View();
        }

        public ActionResult TableTU(string sede = "SLZ")
        {
            int init = 0;
            int end = 892;

            if (!string.IsNullOrEmpty(sede))
            {
                if (!Equals(sede, "TODOS"))
                {
                    var slzBounds = ConfigurationManager.AppSettings[sede].ToString();
                    var initEnd = slzBounds.Split('|');
                    init = int.Parse(initEnd[0]);
                    end = int.Parse(initEnd[1]);
                }
                ViewBag.Sede = sede;
            }

            List<TU> tus = TuService.GetTu(init, end);
            string[] items = { "TODOS", "SLZ", "SIS", "AAL", "NVA", "ACD", "SPAB", "MBA", "CJS" };
            ViewBag.Items = items;
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