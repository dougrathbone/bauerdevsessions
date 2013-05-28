using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NServiceBus;
using _2013.April.NServiceBus.Messages;

namespace _2013.April.NServiceBus.ClientWeb.Controllers
{
    public class HomeController : Controller
    {
        public IBus Bus { get; set; }
        
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult OrderProduct(string name, string product)
        {
            var command = new PlaceOrderCommand() {OrderMadeBy = name, Product = product};

            Bus.Send(command);

            return View("OrderPlaced");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
