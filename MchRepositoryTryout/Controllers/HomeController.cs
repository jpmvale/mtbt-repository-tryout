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

        public ActionResult TableTU(string sede = "SLZ", string initialDate = default(string), string finalDate = default(string))
        {
            List<TU> tus = GetMTBT(sede, initialDate, finalDate);
            return View(tus);
        }

        public ActionResult GetDataFromTU(string sede = "SLZ", string initialDate = default(string), string finalDate = default(string))
        {
            List<TU> tus = GetMTBT(sede, initialDate, finalDate);
            return Json(tus);
        }

        private List<TU> GetMTBT(string sede, string initialDate, string finalDate)
        {
            int init = 0;
            int end = 892;
            DateTime lvInitialDate = initialDate == default(string) ? DateTime.Parse(string.Format("{0}/{1}/{2}", "01",
                                      (DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month : DateTime.Now.Month.ToString()), DateTime.Now.Year)).Date :
                                      DateTime.Parse(initialDate).Date;
            DateTime lvFinalDate = finalDate == default(string) ? DateTime.Now.Date : DateTime.Parse(finalDate).Date;

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

            List<TU> tus = TuService.GetTu(lvInitialDate, lvFinalDate, init, end);
            string[] items = { "TODOS", "SLZ", "SIS", "AAL", "NVA", "ACD", "SPAB", "MBA", "CJS" };
            ViewBag.InitialDate = lvInitialDate;
            ViewBag.FinalDate = lvFinalDate;
            ViewBag.Items = items;
            return tus;
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