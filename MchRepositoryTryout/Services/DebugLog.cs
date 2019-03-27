using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace MchRepositoryTryout.Services
{
    public class DebugLog
    {
        public static Boolean Logar(String strInfo)
        {
            bool result = false;
            string fileName = "";
            StreamWriter tw = null;

            fileName = ConfigurationManager.AppSettings["LOG_FILE_NAME"] + "_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;
            tw = new StreamWriter(HttpContext.Current.Server.MapPath("~/") + "/Logs/" + fileName + ".txt", true,
                                    System.Text.Encoding.Default);
            tw.WriteLine(strInfo);
            result = true;
            tw.Close();

            return result;
        }

        public static Boolean LogChange(String strInfo)
        {
            bool result = false;
            string fileName = "";
            StreamWriter tw = null;

            fileName = "userAlteration" + "_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "_" + DateTime.Now.Hour;
            tw = new StreamWriter(HttpContext.Current.Server.MapPath("~/") + "/Logs/" + fileName + ".txt", true,
                                    System.Text.Encoding.Default);
            strInfo = DateTime.Now + " => " + strInfo;
            tw.WriteLine(strInfo);
            result = true;
            tw.Close();

            return result;
        }

        public static Boolean RemoteLog(String strInfo)
        {
            bool result = false;
            string fileName = "";
            StreamWriter tw = null;

            fileName = "RemoteLog" + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day;
            tw = new StreamWriter(HttpContext.Current.Server.MapPath("~/") + "/Logs/" + fileName + ".txt", true,
                                    System.Text.Encoding.Default);
            strInfo = DateTime.Now + " => " + strInfo;
            tw.WriteLine(strInfo);
            result = true;
            tw.Close();

            return result;
        }
    }
}