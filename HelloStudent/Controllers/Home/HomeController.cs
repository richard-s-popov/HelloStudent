using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloStudent.Controllers.Home
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CallOrder(string name, string phone)
        {
            // todo: реализовать логику отправки email с заявкой на звонок

            return new EmptyResult();
        }
    }
}
