using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompressionDemo.Controllers
{
    public class CompressionController : Controller
    {
        //
        // GET: /Compression/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RunLengthEncoding()
        {
            return View();
        }

        public ActionResult StaticHuffmanCode()
        {
            return View();
        }

        public ActionResult ArithmeticCoding()
        {
            return View();
        }

        public ActionResult Lzw()
        {
            return View();
        }
    }
}
