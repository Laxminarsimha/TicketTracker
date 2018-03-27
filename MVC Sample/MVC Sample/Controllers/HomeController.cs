using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MVC_Sample.Models;

namespace MVC_Sample.Controllers
{
    public class HomeController : Controller
    {
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
        public ActionResult GetPriorityGroupedData()
        {
            if (StaticData.TicketData == null)
                StaticData.Loaddata(Server.MapPath("~/Source/Tickets.csv"));
            var resultdata = StaticData.TicketData.GroupBy(t => t.Priority).Select(k => new { name = k.Key, y = k.Count() });
            return this.Json(resultdata, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetStatusGroupedData()
        {
            if (StaticData.TicketData == null)
                StaticData.Loaddata(Server.MapPath("~/Source/Tickets.csv"));
            var resultdata = StaticData.TicketData.GroupBy(t => t.EscalationStatus).Select(k => new { name = k.Key, y = k.Count() });
            return this.Json(resultdata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSelectedPriorityData(string priority)
        {
            if (StaticData.TicketData == null)
                StaticData.Loaddata(Server.MapPath("~/Source/Tickets.csv"));
            ViewBag.FilteredData = StaticData.TicketData.Where(t => t.Priority == priority).ToList();
            return PartialView("~~/Views/Home/_FilteredView.cshtml")
        }
    }
}