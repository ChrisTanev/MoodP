using System.Collections.Generic;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
//using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace MP.Business.Implementation.FaceAPI.FaceRecognition
{
    public class ProcessImage
    {
        private List<ProcessedImage> _imgList = new List<ProcessedImage>();
        public List<ProcessedImage> ProcessFolder(string mainFolder, string processedImg)
        {
            foreach (var observerFile in System.IO.Directory.GetFiles(mainFolder))
                Processing(observerFile, processedImg);

            foreach (var dir in System.IO.Directory.GetDirectories(mainFolder))
                ProcessFolder(dir, processedImg);

            return _imgList;
        }

   
        private void Processing(string observerFile, string processedImg)
        {
            // if (observerFile == processedImg) return;

            try
            {
                long score;
                long matchTime;
                //Image<Gray, byte> sss = null;
                //Mat m = sss.Mat;

                using (Mat modelImage = CvInvoke.Imread(processedImg, ImreadModes.Grayscale))
                using (Mat observedImage = CvInvoke.Imread(observerFile, ImreadModes.Grayscale))
                {
                    Mat homography;
                    VectorOfKeyPoint modelKeyPoints;
                    VectorOfKeyPoint observedKeyPoints;

                    using (var matches = new VectorOfVectorOfDMatch())
                    {
                        Mat mask;
                        Recognizer.CalculateMatches(modelImage, observedImage, out matchTime, out modelKeyPoints, out observedKeyPoints, matches,
                           out mask, out homography, out score);
                    }
                    var mood = GetMoodFromFolder(observerFile);
                    ProcessedImage ni = new ProcessedImage
                    {
                        ImagePath = observerFile,
                        Score = score,
                        Mood = mood
                    };
                    _imgList.Add(ni);
                }
            }
            catch { }
        }
        public string GetMoodFromFolder(string imagePath)
        {


            if (imagePath.StartsWith(@"D:\MoodPlayerAPI\bin\Images\Happy"))
            {
                return Emotion.Happy.ToString();
            }
            else if (imagePath.StartsWith(@"D:\MoodPlayerAPI\bin\Images\Sad"))
            {
                return Emotion.Sad.ToString();

            }

            return Emotion.Suprise.ToString();


        }

        public List<ProcessedImage> GetResult(List<ProcessedImage> result)
        {
            var resultImageWithScores = new List<ProcessedImage>();
            foreach (var item in result)
            {
                if (item.Score > 0)
                {
                    resultImageWithScores.Add(item);
                }
            }
            var orderedRes = resultImageWithScores.OrderByDescending(o => o.Score).Take(5).ToList();
            long happy = 0;
            long sad = 0;
            long suprise = 0;
            foreach (var weightedImagese in orderedRes)
            {
                if (weightedImagese.Mood == Emotion.Sad.ToString())
                {
                    sad += weightedImagese.Score;
                }
                else if (weightedImagese.Mood == Emotion.Happy.ToString())
                {
                    happy += weightedImagese.Score;
                }
                if (weightedImagese.Mood == Emotion.Suprise.ToString())
                {
                    suprise += weightedImagese.Score;
                }

            }
            return orderedRes;
        }
    }
}
