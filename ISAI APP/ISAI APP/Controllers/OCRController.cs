using ISAI_APP.Models.DTOS;
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
            string fromPrincipal = Request["isPrincipal"];

            if (fromPrincipal != null)
            {
                ObjOcr objOcr = FindCampos(palabras);
                return Json(objOcr, JsonRequestBehavior.AllowGet);
            }


            return Json(palabras, JsonRequestBehavior.AllowGet);
        }

        private static ObjOcr FindCampos(List<string> palabras)
        {
            ObjOcr ocr = new ObjOcr();

            var result0 = palabras.Where(x => x.Contains("NOMBRE")).ToArray();
            var result1 = palabras.Where(x => x.Contains("CURP")).ToArray();

            int index = palabras.IndexOf(result0[0]);
            int indexCurp = palabras.IndexOf(result1[0]);

            ocr.TextoCompleto = palabras.ToArray();
            ocr.ApellidoPat = palabras[index + 1];
            ocr.ApellidoMat = palabras[index + 2];
            ocr.Nombre = palabras[index + 3];
            ocr.Curp = palabras[indexCurp + 1];


            return ocr;
        }
    }
}