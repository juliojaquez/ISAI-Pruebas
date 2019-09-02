using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryScore
{
    public class ScoreImg
    {
        // A List<> which contains processed images scores
        List<WeightedImages> imgList = new List<WeightedImages>();

        public List<WeightedImages> ProcessFolder(string detailImage)
        {
            string imagesDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string mainFolder = imagesDirectory + "Content\\documents\\";
            string rutaDetailImage = imagesDirectory + "Content\\imagesCrop\\" + detailImage;

            foreach (var file in System.IO.Directory.GetFiles(mainFolder))
                ProcessImage(file, rutaDetailImage);

            //foreach (var dir in System.IO.Directory.GetDirectories(mainFolder))
            //    ProcessFolder(dir, detailImage);

            return imgList;
        }

        public void ProcessImage(string completeImage, string detailImage)
        {
            if (completeImage == detailImage) return;

            try
            {
                long score;
                long matchTime;

                using (Mat modelImage = CvInvoke.Imread(detailImage, ImreadModes.Color))
                using (Mat observedImage = CvInvoke.Imread(completeImage, ImreadModes.Color))
                {
                    Mat homography;
                    VectorOfKeyPoint modelKeyPoints;
                    VectorOfKeyPoint observedKeyPoints;

                    using (var matches = new VectorOfVectorOfDMatch())
                    {
                        Mat mask;
                        DrawMatches.FindMatch(modelImage, observedImage, out matchTime, out modelKeyPoints, out observedKeyPoints, matches,
                           out mask, out homography, out score);
                    }

                    imgList.Add(new WeightedImages() { ImagePath = completeImage, Score = score });
                }
            }
            catch (Exception e)
            {
            }

        }
    }

    // Custom class to store image scores
    public class WeightedImages
    {
        public string ImagePath { get; set; } = "";
        public long Score { get; set; } = 0;
    }




}
