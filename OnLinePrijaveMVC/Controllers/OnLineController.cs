using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnLinePrijaveMVC.Controllers
{
    public class OnLinePrijaveController : Controller
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(OnLinePrijaveController));

        public ActionResult OnLinePrijave()
        {
            log.Info($"Browser: {Request.Browser.Browser}, Version: {Request.Browser.Version}, UserAgent: {Request.UserAgent}");

            return View();
        }
    }
}