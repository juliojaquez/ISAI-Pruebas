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

        private static INEData EscenarioUno(List<string> palabras)
        {
            try
            {
                INEData ocr = new INEData();
                string textoPlano = String.Empty;
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
                //Seccion TextoArea
                ocr.textCompleto = palabras.ToArray();
                foreach (var texto in palabras.ToArray())
                {
                    textoPlano = textoPlano + " " + texto;
                }
                ocr.textComplete = textoPlano;


                if (ocr.firstName != null && ocr.lastName != null && ocr.name != null && ocr.textComplete != null)
                {
                    ocr.resultEnvironment = 1;
                    ocr.typeEnvironment = "Escenario 1";
                }
                else
                {
                    ocr.resultEnvironment = 0;
                    ocr.typeEnvironment = "No Funciono";
                }
                return ocr;
            }
            catch(Exception e)
            {
                e.Message.ToString();
                return null;
            }
        }

        private static INEData EscenarioDos(List<string> palabras)
        {
            try
            {
                INEData ocr = new INEData();
                string textoPlano = String.Empty;
                var result0 = palabras.Where(x => x.Contains("Ca")).ToArray();
                var result1 = palabras.Where(x => x.Contains("III")).ToArray();
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
                //Seccion TextoArea
                ocr.textCompleto = palabras.ToArray();
                foreach (var texto in palabras.ToArray())
                {
                    textoPlano = textoPlano + " " + texto;
                }
                ocr.textComplete = textoPlano;


                if (ocr.firstName != null && ocr.lastName != null && ocr.name != null && ocr.textComplete != null)
                {
                    ocr.resultEnvironment = 1;
                    ocr.typeEnvironment = "Escenario 2";
                }
                else
                {
                    ocr.resultEnvironment = 0;
                    ocr.typeEnvironment = "No funciono";
                }
                return ocr;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
        }

        private static INEData FindCampos(List<string> palabras)
        {
            DataBaseJulio DataBase = new DataBaseJulio();
            INEData objOcr = new INEData();
            objOcr = EscenarioUno(palabras);
            try
            {
                if (objOcr.resultEnvironment == 1)
                {
                    objOcr.registerDate = DateTime.Now;
                    DataBase.INELogs.Add(objOcr);
                    DataBase.SaveChanges();
                    return objOcr;
            }
                else if (EscenarioDos(palabras).resultEnvironment == 1)
            {
                objOcr = EscenarioDos(palabras);
                objOcr.registerDate = DateTime.Now;
                DataBase.INELogs.Add(objOcr);
                DataBase.SaveChanges();
                return objOcr;
            }
            else
            {
                return null;
            }
        }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
        }
    }
}