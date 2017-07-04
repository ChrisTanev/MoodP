using System;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Unity;
using MP.Application.Implementation.Utility;
using MP.Application.Implementation.ViewModels;

namespace MP.Application.Implementation.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MediaView
    {
        public MediaView()
        {
            InitializeComponent();
            try
            {
                if (DataContext == null)
                    DataContext = IocContainer.StaticContainer.Resolve<MediaViewModel>();
                NavigationCacheMode = NavigationCacheMode.Enabled;
                NavigationCacheMode = NavigationCacheMode.Required;
            }
            catch (Exception r)
            {
                    
                
            }

        }

    }
}