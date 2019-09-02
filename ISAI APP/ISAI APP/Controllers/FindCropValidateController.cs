using FindAndCropImage;
using LibraryScore;
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
            return View();
        }

        [HttpPost]
        public ActionResult FileVerification(HttpPostedFileBase file)
        {
            System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(file.InputStream);
            var galleryDirectoryPath1 = Server.MapPath("~/Content/imagesUploads/");
            bmpPostedImage.Save(galleryDirectoryPath1 + file.FileName);
            
            var imgToSave = FindAndCrop.FindCrop(bmpPostedImage);

            if (imgToSave != null)
            {
                var galleryDirectoryPath = Server.MapPath("~/Content/imagesCrop/");
                string nombreImagen = "imagen" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                imgToSave.Save(galleryDirectoryPath + nombreImagen);

                ViewBag.Url = nombreImagen;
                ViewBag.Url2 = file.FileName;

                var Lista = Score(nombreImagen);

                ViewBag.Lista = Lista;
            }
            else
            {
                ViewBag.IsNull = "true";
            }
            return View();
        }

        public List<WeightedImages> Score(string nombreImagen)
        {
            ScoreImg score = new ScoreImg();
            var objeto = score.ProcessFolder(nombreImagen);

            return objeto;
        }
    }
}