using ISAI_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISAI_APP.Controllers
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

        public string Parametros(string name, string firstName, string lastName, string curp, string fechaNacimiento)
        {
            string Resultado = name + ' ' + firstName + ' ' + lastName + ' ' + ' ' + curp + ' ' + fechaNacimiento;
            ViewBag.Result = Resultado;
            return Resultado;
        }


        
    }
}