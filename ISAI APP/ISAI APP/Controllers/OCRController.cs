using ISAI_APP.Models;
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
                INEData objOcr = FindCampos(palabras);
                return Json(objOcr, JsonRequestBehavior.AllowGet);
            }


            return Json(palabras, JsonRequestBehavior.AllowGet);
        }

        private static INEData FindCampos(List<string> palabras)
        {
            DataBaseJulio DataBase = new DataBaseJulio();
            string textoPlano = String.Empty;
            try
            {
                INEData ocr = new INEData();

                var result0 = palabras.Where(x => x.Contains("NOMBRE")).ToArray();
                var result1 = palabras.Where(x => x.Contains("CURP")).ToArray();

                //Seccion nombre
                if (result0.Length > 0)
                {
                    int index = palabras.IndexOf(result0[0]);
                    ocr.firstName = palabras[index + 1];
                    ocr.lastName = palabras[index + 2];
                    ocr.name = palabras[index + 3];
                }
                //Seccion curp
                if (result1.Length > 0)
                {
                    int indexCurp = palabras.IndexOf(result1[0]);
                    ocr.CURP = palabras[indexCurp + 1];
                }

                ocr.textCompleto = palabras.ToArray();

                foreach (var texto in palabras.ToArray())
                {
                    textoPlano = textoPlano + " " + texto;
                }



                INEData objLog = new INEData();
                objLog.name = ocr.name;
                objLog.firstName = ocr.firstName;
                objLog.lastName = ocr.lastName;
                //objLog.home = "Av. de la Finca";
                //objLog.age = 24;
                //objLog.sex = "H";
                //objLog.code = 80050;
                //objLog.stateRepublic = "Puebla";
                //objLog.stateNumber = 1;
                //objLog.folio = 222223;
                //objLog.registerYear = 2012;
                objLog.CURP = ocr.CURP;
                //objLog.stateNumber = 12;
                //objLog.municipalityNumber = 3;
                //objLog.locationNumber = 23;
                //objLog.sectionNumber = 4;
                //objLog.validUntil = 2023;
                //objLog.codeElector = "GOJDGDGGGS2";
                objLog.registerDate = DateTime.Now;
                objLog.textComplete = textoPlano;

                DataBase.INELogs.Add(objLog);
                DataBase.SaveChanges();
                return ocr;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
        }
    }
}