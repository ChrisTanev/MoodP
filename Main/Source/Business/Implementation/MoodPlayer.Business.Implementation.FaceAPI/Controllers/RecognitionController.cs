using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Emgu.CV;
using Emgu.CV.CvEnum;
//using Emgu.CV;
//using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using MP.Business.Implementation.FaceAPI.FaceRecognition;
using Newtonsoft.Json;

namespace MP.Business.Implementation.FaceAPI.Controllers
{
    public class RecognitionController : ApiController
    {
        private ProcessedImage _imageFinalResult;
        private List<ProcessedImage> _listResult = new List<ProcessedImage>();
        [System.Web.Http.HttpPost]
        [AcceptVerbs("GET", "POST")]
        public List<ProcessedImage> Recognize()
        {
            try
            {
                var parms = Request.Content.ReadAsByteArrayAsync().Result;
                using (var im = Image.FromStream(new MemoryStream(parms)))
                {
                    var preparedImg = ImagePreprocessing.PrepareImage(im);
                    var pathToPreparedImg =
                        @"D:\MoodPlayerAPI\bin\preparedImg.png";
                    preparedImg.Save(pathToPreparedImg);
                    ProcessImage ip = new ProcessImage();
                    var result = ip.ProcessFolder(ConfigurationManager.AppSettings["FolderWithDatabasePictures"], pathToPreparedImg);
                    _listResult = ip.GetResult(result);
                    _imageFinalResult = _listResult[0];   

                }

                return _listResult;
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return _listResult;
        }
    }

}
