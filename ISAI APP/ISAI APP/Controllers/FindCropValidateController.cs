using FindAndCropImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISAI_APP.Controllers
{
    public class FindCropValidateController : Controller
    {
        // GET: FindCropValidate
        public ActionResult Index()
        {
            FindAndCrop.FindCrop();
            return View();
        }


        public ActionResult FileVerification()
        {
            return View();
        }
    }
}