using System.Collections.Generic;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP.Business.Implementation.FaceAPI.FaceRecognition;

namespace MP.Business.UnitTests.FaceAPI
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestRecogn()
        {

            // var folderToBeProcessed = @"C:\Users\H.Tanev\Desktop\data";
            var folderToBeProcessed = @"..\..\..\..\Implementation\MoodPlayer.Business.Implementation.FaceAPI\Images";
            var imageToBeProcessed = @"C:\Users\H.Tanev\Desktop\processedImage\0.jpg";
            var resultImageWithScores = new List<ProcessedImage>();
            CvInvoke.Imread(@"C:\Users\H.Tanev\Desktop/mesad.jpg", ImreadModes.Grayscale);
            ProcessImage pi = new ProcessImage();
            //  Mat result = Recognizer.Draw(modelImage, observedImage, out matchTime);
            var result = pi.ProcessFolder(folderToBeProcessed, imageToBeProcessed);
            foreach (var item in result)
            {
                if (item.Score > 0)
                {
                    resultImageWithScores.Add(item);
                }
            }
            var orderedRes = resultImageWithScores.OrderByDescending(o => o.Score).Take(5).ToList();
            // var moodRes = Map(orderedRes);
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
        }


    }
}
