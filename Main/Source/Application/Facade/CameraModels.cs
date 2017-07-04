using Windows.Media.Capture;
using Windows.System.Display;
using Windows.UI.Xaml.Controls;

namespace MP.Application.Facade
{
    public class CameraModels : ModelBase
    {
        private CaptureElement _captureElement;

        public CameraModels()
        {
            MediaCapture = new MediaCapture();
            DisplayRequest = new DisplayRequest();
            CaptureElement = new CaptureElement();
        }

        public bool IsPreviewing { get; set; }
        public DisplayRequest DisplayRequest { get; set; }

        public MediaCapture MediaCapture { get; set; }

        public CaptureElement CaptureElement
        {
            get { return _captureElement; }
            set
            {
                _captureElement = value;
                OnPropertyChanged();
            }
        }
    }
}