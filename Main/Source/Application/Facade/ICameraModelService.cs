using System.Threading.Tasks;

namespace MP.Application.Facade
{
    public interface ICameraModelService
    {
        Task StartCamera(CameraModels cameraModels);
        Task CleanCamera(CameraModels cameraModels);
        Task CapturePhoto(CameraModels cameraModels);
    }
}