using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Unity;
using MP.Application.Facade;
using MP.Application.Implementation.Utility;
using MP.Application.Implementation.ViewModels;
using MP.Application.Implementation.Views.Navigation;
using MP.Business.Facade;
using MP.Business.Implementation;
using MP.Data.Facade;
using MP.Data.Implementation;
using MP.Data.Implementation.Json;

namespace MP.Application.Implementation.Views
{
    /// <summary>
    ///     Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App
    {
        /// <summary>
        ///     Initializes the singleton application object.  This is the first line of authored code
        ///     executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;

            IocContainer.StaticContainer = new UnityContainer();
            IocContainer.StaticContainer.RegisterType<INavigationPage, NavigateToMediaView>();
            IocContainer.StaticContainer.RegisterType<INavigationService, NavigationService>();
            IocContainer.StaticContainer.RegisterType<IViewModelService, MediaViewModelService>();
            IocContainer.StaticContainer.RegisterType<IMusicFileAccess, MusicFileAccess>();
            IocContainer.StaticContainer.RegisterType<MediaViewModel>();
            IocContainer.StaticContainer.RegisterType<INavigationPage, NavigateToCameraView>();
            IocContainer.StaticContainer.RegisterType<INavigationService, NavigationService>();
            IocContainer.StaticContainer.RegisterType<ICameraModelService, CameraModelService>();
            IocContainer.StaticContainer.RegisterType<IGenreService, GenreService>();
            IocContainer.StaticContainer.RegisterType<IFolderAccess, FolderAccess>();
            IocContainer.StaticContainer.RegisterType<IFaceApiAccess, FaceApiAccess>();
            IocContainer.StaticContainer.RegisterType<ILyricsAccess, LyricsApiAccess>();
            IocContainer.StaticContainer.RegisterType<ISerializer, JsonSerializer>();
            IocContainer.StaticContainer.RegisterType<IUniversalSerializer, UniversalSerializer>();
            IocContainer.StaticContainer.RegisterType<IPlaylistNaming, PlaylistNaming > ();
            IocContainer.StaticContainer.RegisterType<IGenreService, GenreService>();
            IocContainer.StaticContainer.RegisterType<IMoodService, MoodService > ();
            IocContainer.StaticContainer.RegisterType<ILyricsProcessingFacade, LyricsProcessingFacade>();
            IocContainer.StaticContainer.RegisterType<CameraViewModel>();
        }

        /// <summary>
        ///     Invoked when the application is launched normally by the end user.  Other entry points
        ///     will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (Debugger.IsAttached)
                DebugSettings.EnableFrameRateCounter = true;
#endif
            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                    rootFrame.Navigate(typeof(MediaView), e.Arguments);
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        ///     Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        ///     Invoked when application execution is being suspended.  Application state is saved
        ///     without knowing whether the application will be terminated or resumed with the contents
        ///     of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}