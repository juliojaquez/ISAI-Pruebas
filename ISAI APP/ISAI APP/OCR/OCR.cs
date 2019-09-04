using IronOcr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISAI_APP.OCR
{
    public static class OCRUtil
    {
        public static string GetText()
        {
            string imagesDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string fileSource = imagesDirectory + "Content\\documents\\img1.jpg";

            var Ocr = new AutoOcr();
            var Result = Ocr.Read(fileSource);
            Console.WriteLine(Result.Text);

            //var Ocr = new AdvancedOcr()
            //{
            //    //CleanBackgroundNoise = true,
            //    //EnhanceContrast = true,
            //    //EnhanceResolution = true,
            //    ////Language = IronOcr.Languages.English.OcrLanguagePack,
            //    //Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
            //    //ColorSpace = AdvancedOcr.OcrColorSpace.Color,
            //    ////DetectWhiteTextOnDarkBackgrounds = true,
            //    ////InputImageType = AdvancedOcr.InputTypes.AutoDetect,
            //    //RotateAndStraighten = true,
            //    ////ReadBarCodes = true,
            //    //ColorDepth = 4
            //};

            Console.WriteLine(Result.Text);
            return Result.Text;
        }

    }
}