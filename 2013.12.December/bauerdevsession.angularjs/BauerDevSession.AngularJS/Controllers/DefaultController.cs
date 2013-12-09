using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BauerDevSession.AngularJS.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HelloWorld()
        {
            return View();
        }

        public ActionResult GiftRequest()
        {
            return View();
        }

        public JsonResult GetListOfGifts()
        {
            var gifts = new Gift[] {new Gift {Code = "PS4", Name = "Play Station 4"}, new Gift {Code = "X1", Name = "XBox One"}};
            
            return Json(gifts, JsonRequestBehavior.AllowGet);
        }
	}

    class Gift
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}