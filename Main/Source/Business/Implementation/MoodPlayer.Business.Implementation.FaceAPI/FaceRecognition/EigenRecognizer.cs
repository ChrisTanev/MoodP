using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace MP.Business.Implementation.FaceAPI.FaceRecognition
{
    public class EigenRecognizer
    {
        private static CascadeClassifier _faceToRe;
        private static CascadeClassifier _smile;

        public EigenRecognizer()
        {
            _faceToRe = new CascadeClassifier("D:\\MoodPlayerAPI\\haarcascade_frontalface_default.xml");
            _smile = new CascadeClassifier("D:\\MoodPlayerAPI\\haarcascade_smile.xml");
        }
      

        public Image<Gray,byte> DetectFace(Image<Gray, byte> imageToBePredicted)
        {

            Image<Gray, byte> grayImageToBePredicted = imageToBePredicted.Convert<Gray, byte>();
            List<Rectangle> faces = new List<Rectangle>();
            Rectangle[] facesDetected = _faceToRe.DetectMultiScale(
                  grayImageToBePredicted,
                  1.1,
                  10,
                  new Size(64, 64),
                  Size.Empty);
            faces.AddRange(facesDetected);
            Image<Gray, byte> faceRectangleImage = null;

            foreach (Rectangle f in facesDetected)
            {
                faceRectangleImage = grayImageToBePredicted
                    .Copy(f)
                    .Convert<Gray, byte>()
                    .Resize(64, 64, Inter.Cubic);



                //faceRectangleImage.Save(string.Format("C:\\Users\\H.Tanev\\Desktop\\{0}.jpg", f.Height.ToString()));
            }

            return faceRectangleImage;
        }
        public Rectangle[] DetectFaceRectangle(Image<Gray, byte> imageToBePredicted)
        {

            Image<Gray, byte> grayImageToBePredicted = imageToBePredicted.Convert<Gray, byte>();
            List<Rectangle> faces = new List<Rectangle>();
            Rectangle[] facesDetected = _faceToRe.DetectMultiScale(
                  grayImageToBePredicted,
                  1.1,
                  10,
                  new Size(64, 64),
                  Size.Empty);
            faces.AddRange(facesDetected);
            Image<Gray, byte> faceRectangleImage = null;

            foreach (Rectangle f in facesDetected)
            {
                faceRectangleImage = grayImageToBePredicted
                    .Copy(f)
                    .Convert<Gray, byte>()
                    .Resize(64, 64, Inter.Cubic);



                //faceRectangleImage.Save(string.Format("C:\\Users\\H.Tanev\\Desktop\\{0}.jpg", f.Height.ToString()));
            }

            return facesDetected;
        }
        public Image<Gray,byte> DetectLips(Image<Gray, byte> grayImageToBePredicted, Rectangle[] facesDetected)
        {
            List<Rectangle> lipsDetected = new List<Rectangle>();
            Image<Gray, byte> mouthRectangle = null;
            Image<Gray, Byte> lowerfaceImage = null;
            try
            {
                lowerfaceImage = grayImageToBePredicted.Copy(new Rectangle(facesDetected[0].X, (int)(facesDetected[0].Y + facesDetected[0].Height * 0.6666), facesDetected[0].Width, (int)facesDetected[0].Height / 3));


            }
            catch (Exception e)
            {

            }
           // LowerfaceImage = GrayImageToBePredicted.Copy(new Rectangle(facesDetected[0].X, (int)(facesDetected[0].Y + facesDetected[0].Height * 0.6666), facesDetected[0].Width, (int)facesDetected[0].Height / 3));
            Rectangle[] mouthDetected = _smile.DetectMultiScale(
                        lowerfaceImage,
                        1.1,
                        10,
                          new Size(64, 64),
                        Size.Empty);
            lipsDetected.AddRange(facesDetected);
            int imgco = 0;
            foreach (Rectangle f in mouthDetected)
            {
                mouthRectangle = lowerfaceImage
                    .Copy(f)
                    .Convert<Gray, Byte>()
                    .Resize(64, 64, Inter.Cubic);



               // mouthRectangle.Save(string.Format("C:\\Users\\H.Tanev\\Desktop\\qwe\\{0}.jpg", imgco));
                imgco++;
            }

            return mouthRectangle;

        }
    }
}

//static Image<Bgr, byte> imageToBePredicted = new Image<Bgr, byte>("C:\\Users\\H.Tanev\\Desktop\\mesad.jpg");
/*  public EigenFaceRecognizer Train()
  {

      var faceImages = new Image<Gray, byte>[16];
      var faceLabels = new int[16];
      int label = 0; //happy 1-20.sad 120-140
                     var happyPicturesDir = Directory.GetFiles("C:\\Users\\H.Tanev\\Desktop\\myPics\\Happy");
      var happyPicturesDir = Directory.GetFiles("C:\\Users\\H.Tanev\\Desktop\\happyMouths");
      foreach (var picture in happyPicturesDir)
      {

          var faceImage = new Image<Gray, byte>(picture).Resize(64, 64, Inter.Cubic);
          faceImage._EqualizeHist();
          faceImages[label] = faceImage;
          faceLabels[label] = (int)Emotion.Happy + label;
          label++;
      }


  var sadPicturesDir = Directory.GetFiles("C:\\Users\\H.Tanev\\Desktop\\MyPics\\Sad");
      var sadPicturesDir = Directory.GetFiles("C: \\Users\\H.Tanev\\Desktop\\sadMouths");
      foreach (var picture in sadPicturesDir)
      {
          var faceImage = new Image<Gray, byte>(picture).Resize(64, 64, Inter.Cubic);
          ;
          faceImages[label] = faceImage;
          faceImage._EqualizeHist();
          faceLabels[label] = (int)Emotion.Sad + label;
          label++;
      }


      var faceRecognizer = new EigenFaceRecognizer(80, Double.NegativeInfinity);
      faceRecognizer.Train(faceImages, faceLabels);
      faceRecognizer.Save("C:\\Users\\H.Tanev\\Desktop\\training.yml");
      var filename =
Directory.GetFiles(@"D:\ICT ENGINEERING\5\DNP\ExamProjects\ConsoleApplication4\HaarCascade");
      var smileLoc = Directory.GetFiles("C:\\Users\\H.Tanev\\Desktop\\haar\\");
      faceToRe = new CascadeClassifier(filename[0]);
      smile = new CascadeClassifier(smileLoc[0]);
      return faceRecognizer;
  } */
