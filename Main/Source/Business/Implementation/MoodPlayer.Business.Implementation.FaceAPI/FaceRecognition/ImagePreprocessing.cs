using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace MP.Business.Implementation.FaceAPI.FaceRecognition
{
    public class ImagePreprocessing
    {
        /// <summary>
        /// Turn image to gray and equalize histogram
        /// </summary>
        /// <param name="img"></param>
        public static Image<Gray, byte> PrepareImage(Image img)
        {
            Image<Gray, byte> grayImage = new Image<Gray, byte>(img as Bitmap);
            EigenRecognizer er = new EigenRecognizer();
            try
            {
               
                var faceRec = er.DetectFaceRectangle(grayImage);
                var lips = er.DetectLips(grayImage, faceRec);
              //  
                if (lips != null)
                {
                    lips._EqualizeHist();
                    return lips;
                }
               
            }
            catch (Exception e)
            {


            }
            var face = er.DetectFace(grayImage);
            face._EqualizeHist();
            return face;

        }
    }
}