using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAndCropImage
{
    public static class FindAndCrop
    {
        public static Bitmap FindCrop(Bitmap img, string choose)
        {
            string imagenTemplate = string.Empty;
            double rangoEfectividad = 0.0;
            if (choose == "INE")
            {
                imagenTemplate = "area_firma3.jpg";
                rangoEfectividad = 0.2;
            }
            else if (choose == "DOCUMENTO")
            {
                imagenTemplate = "escudo.jpg";
                rangoEfectividad = 0.1;
            }
            string imagesDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Image<Bgr, byte> source = new Image<Bgr, byte>(img); // Image B
            //Image OriginalImage = Image.FromFile(@"C:\Users\Julio.Jaquez\Desktop\imagenes\ife-1.jpg");

            string rutaTemplate = imagesDirectory + "Content\\templates\\" + imagenTemplate;

            Image<Bgr, byte> template = new Image<Bgr, byte>(rutaTemplate); // Image A
            //Image<Bgr, byte> wat = new Image<Bgr, byte>(@"C:\Users\Julio.Jaquez\Desktop\detailimage\periodico.jpg");
            Image<Bgr, byte> imageToShow = source.Copy();
            Image<Bgr, byte> imagenFirma = null;
            Bitmap a = null;
            string RutaUrl = string.Empty;

            using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] >= rangoEfectividad)
                {
                    // This is a match. Do something with it, for example draw a rectangle around it.
                    Rectangle match = new Rectangle(maxLocations[0], template.Size);
                    imagenFirma = CropImage(img, match, ref RutaUrl);
                    imageToShow.Draw(match, new Bgr(Color.Red), 3);
                    a = imagenFirma.ToBitmap();

                    //Image b = imageToShow.ToJpegData();
                }
            }


            return a;
            // Show imageToShow in an ImageBox (here assumed to be called imageBox1)
            //imageBox1.Image = imageToShow;
            //imageBox2.Image = imagenFirma;
        }

        public static Image<Bgr, byte> CropImage(Image source, Rectangle crop, ref string _rutaUrl)
        {
            Image<Bgr, Byte> myImage = null;
            try
            {
                var bmp = new Bitmap(crop.Width, crop.Height);
                using (var gr = Graphics.FromImage(bmp))
                {
                    gr.DrawImage(source, new Rectangle(0, 0, bmp.Width, bmp.Height), crop, GraphicsUnit.Pixel);
                }

                //string nombreImagen = "imagen" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                //_rutaUrl = @"../ISAI APP/Content/images/" + nombreImagen;


                //bmp.Save(_rutaUrl);
                myImage = new Image<Bgr, Byte>(bmp);
            }
            catch (Exception e)
            {
                _rutaUrl = "Error";

            }
            return myImage;
        }
    }
}
