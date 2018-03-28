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
            List<TicketData> ticketdata = LoadData(Server.MapPath("~/Source/Tickets.csv"));
            var resultdata = ticketdata.GroupBy(t => t.Priority).Select(k => new { name = k.Key, y = k.Count() });
            return this.Json(resultdata, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetStatusGroupedData()
        {
            List<TicketData> ticketdata = LoadData(Server.MapPath("~/Source/Tickets.csv"));
            var resultdata = ticketdata.GroupBy(t => t.EscalationStatus).Select(k => new { name = k.Key, y = k.Count() });
            return this.Json(resultdata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSelectedPriorityData(string priority)
        {
            List<TicketData> ticketdata = LoadData(Server.MapPath("~/Source/Tickets.csv"));
            ViewBag.FilteredData = ticketdata.Where(t => t.Priority == priority).ToList();
            return PartialView("~/Views/Home/_FilteredView.cshtml");
        }
        public List<TicketData> LoadData(string Filepath)
        {
            List<TicketData> ticketdata = new List<TicketData>();
            if (System.IO.File.Exists(Filepath))
            {
                var data = System.IO.File.ReadAllLines(Filepath);
                for (int i = 1; i < data.Length; i++)
                {
                    var ticket = data[i];
                    TicketData currentData = new TicketData();
                    currentData.EscalationId = ticket.Split(',')[0];
                    currentData.Summary = ticket.Split(',')[1];
                    currentData.Priority = ticket.Split(',')[2];
                    currentData.EscalationStatus = ticket.Split(',')[3];
                    ticketdata.Add(currentData);
                }
            }
            return ticketdata;
        }

        public ActionResult Search()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult SearchDefects(string search)
        {
            List<TicketData> ticketdata = LoadData(Server.MapPath("~/Source/Tickets.csv"));
            ViewBag.FilteredData = ticketdata.Where(t => t.Summary.ToLower().Contains(search.ToLower())).ToList();
            return PartialView("~/Views/Home/_FilteredView.cshtml");
        }
    }
}