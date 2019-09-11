using FindAndCropImage;
using ISAI_APP.Models;
using ISAI_APP.OCR;
using LibraryScore;

using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.Linq;

using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace ISAI_APP.Controllers
{
    public class FindCropValidateController : Controller
    {
        // GET: FindCropValidate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FileVerification(HttpPostedFileBase file, string choose)
        {
            DataBaseJulio DataBase = new DataBaseJulio();
            string mensaje = string.Empty;
            if (file == null)
            {
                mensaje = "El archivo viene vacio";
                //return Json(mensaje);
            }

            Log objLog = new Log();
            objLog.Error = "Si llegue desde produccion" + "\n " + mensaje;
            DataBase.Logs.Add(objLog);
            DataBase.SaveChanges();

            /*modulo de pruebas*/
            objLog = new Log();
            var galleryDirectoryPath1 = Server.MapPath("~/Content/imagesUploads/");
            objLog.Error = galleryDirectoryPath1;
            DataBase.Logs.Add(objLog);
            DataBase.SaveChanges();

            System.Drawing.Bitmap bmpPostedImage = null;

            try
            {
                bmpPostedImage = new System.Drawing.Bitmap(file.InputStream);
                objLog.Error = "Creó la imagen bien";
                DataBase.Logs.Add(objLog);
                DataBase.SaveChanges();

                objLog = new Log();
                bmpPostedImage.Save(galleryDirectoryPath1 + file.FileName);
                objLog.Error = "Se guardó la imagen correctamente";
                DataBase.Logs.Add(objLog);
                DataBase.SaveChanges();
            }
            catch (Exception e)
            {
                objLog.Error = "Hubo error creando o guardando la imagen bien";
                DataBase.Logs.Add(objLog);
                DataBase.SaveChanges();
            }

            try
            {
                //FIND & CROP
                var imgToSave = FindAndCrop.FindCrop(bmpPostedImage, choose);
                objLog.Error = "La imagen se cortó chingón";
                DataBase.Logs.Add(objLog);
                DataBase.SaveChanges();

                if (imgToSave != null)
                {
                    var galleryDirectoryPath = Server.MapPath("~/Content/imagesCrop/");
                    string nombreImagen = "imagen" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    imgToSave.Save(galleryDirectoryPath + nombreImagen);

                    ViewBag.Url = nombreImagen;
                    ViewBag.Url2 = file.FileName;

                    try
                    {
                        //SCORE
                        var Lista = Score(nombreImagen);
                        objLog.Error = "Todo bien con el Score";
                        ViewBag.Lista = Lista;
                    }
                    catch (Exception e)
                    {
                        objLog.Error = "Error sacando Score";
                        DataBase.Logs.Add(objLog);
                        DataBase.SaveChanges();
                    }

                }
                else
                {
                    ViewBag.IsNull = "true";
                }
            }
            catch (Exception e)
            {
                objLog.Error = "Hubo error cortando la imagen o guardandola" + e.Message + "\nInner" + e.InnerException;
                DataBase.Logs.Add(objLog);
                DataBase.SaveChanges();
                if (e.Message.ToString() == "OpenCV: Bad input roi")
                {
                    ViewBag.IsNull = "true";
                    ViewBag.MensajeError = "Imagen demasiado pequeña para procesarse, favor de verificar";
                    
                }
                
            }

            return View();
        }

        public ActionResult Prueba()
        {

            return Json("null", JsonRequestBehavior.AllowGet);
        }

        public List<WeightedImages> Score(string nombreImagen)
        {
            ScoreImg score = new ScoreImg();
            var objeto = score.ProcessFolder(nombreImagen);

            return objeto;
        }
    }
}