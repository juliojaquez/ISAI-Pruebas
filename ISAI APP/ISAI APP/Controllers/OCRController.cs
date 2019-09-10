using ISAI_APP.OCR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ISAI_APP.Controllers
{
    public class OCRController : Controller
    {
        // GET: OCR
        public ActionResult OCR()
        {
            return View();
        }

        public async Task<ActionResult> OCRVerification(HttpPostedFileBase file)
        {
            List<string> palabras = await OCRUtil.GetText(file);

            return Json(palabras, JsonRequestBehavior.AllowGet);

        }
    }
}