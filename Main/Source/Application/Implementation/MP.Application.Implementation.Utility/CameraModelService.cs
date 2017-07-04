using System;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using MP.Application.Facade;
using MP.Business.Facade;
using MP.Data.Facade;


namespace MP.Application.Implementation.Utility
{
    public class CameraModelService : ICameraModelService
    {
        private IFaceApiAccess _iFaceApiAccess;
        private readonly IMoodService _iMoodService;
        public CameraModelService(IFaceApiAccess iFaceApiAccess,IMoodService imood)
        {
            _iFaceApiAccess = iFaceApiAccess;
            _iMoodService = imood;
        }
        public async Task StartCamera(CameraModels cameraModels)
        {
            try
            {
                await cameraModels.MediaCapture.InitializeAsync();
                cameraModels.CaptureElement.Source = cameraModels.MediaCapture;
                await cameraModels.MediaCapture.StartPreviewAsync();
                cameraModels.IsPreviewing = true;
                cameraModels.DisplayRequest.RequestActive();
                DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            }
            catch (UnauthorizedAccessException)
            {
            }

        }

        public async Task CleanCamera(CameraModels cameraModels)
        {
            if (cameraModels.MediaCapture != null)
            {
                if (cameraModels.IsPreviewing)
                    await cameraModels.MediaCapture.StopPreviewAsync();
                cameraModels.CaptureElement.Source = null;
                cameraModels.DisplayRequest?.RequestRelease();

                cameraModels.MediaCapture.Dispose();
                cameraModels.MediaCapture = null;
            }
        }

        public async Task CapturePhoto(CameraModels cameraModels)
        {
            var fold = FileService.GetPicturesFolder();
            var file = await FileService.CreatePhotoAsync(fold);
            var imgFormat = PictureProperties.CreatePImageProperties();
            await cameraModels.MediaCapture.CapturePhotoToStorageFileAsync(imgFormat, file);
            await CleanCamera(cameraModels);
            var photoToByteArray = PictureProperties.ConvertImageToByte(file);
            var qwe = await _iFaceApiAccess.Post(await photoToByteArray);
            var reslt = qwe[0];
            var mapImageProcessedMoodToMood = _iMoodService.GetMoodFromString(reslt.Mood);
         _iMoodService.SetCurrentMood(mapImageProcessedMoodToMood);

        }

    }
}