using Microsoft.Practices.Unity;
using MP.Application.Implementation.Utility;
using MP.Application.Implementation.ViewModels;

namespace MP.Application.Implementation.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CamView
    {
        public CamView()
        {
            InitializeComponent();
            DataContext = IocContainer.StaticContainer.Resolve<CameraViewModel>();
        }
    }
}