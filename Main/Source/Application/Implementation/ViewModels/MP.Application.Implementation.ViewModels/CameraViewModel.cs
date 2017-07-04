using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using MP.Application.Facade;
using MP.Application.Implementation.Utility;
using MP.Business.Facade;
namespace MP.Application.Implementation.ViewModels
{
    public class CameraViewModel : BaseViewModel
    {
        private readonly ICameraModelService _cameraModelService;
        private readonly ILyricsProcessingFacade _iLyricsProcessingFacade;
        public ICommand OnCapture { get; set; }
        public ICommand OnLoad { get; set; }
        public CameraModels CameraModels { get; set; }

        public CameraViewModel(INavigationService navigationService,

            ICameraModelService iCameraModelService, ILyricsProcessingFacade ilpf)
        {
            _cameraModelService = iCameraModelService;
            _iLyricsProcessingFacade = ilpf;
            OnCapture = new CommandBase(async o =>
            {
                navigationService.NavigateBack();
                await _cameraModelService.CapturePhoto(CameraModels);
                try
                {

                    var qwer =
                        _iLyricsProcessingFacade.ProcessLyricsAndMood(PlaylistSorter.ListOfSongsAfterGenreSorting);
                    while (PlaylistSorter.ListOfSongsAfterGenreSorting.Count > 0)
                    {
                        PlaylistSorter.ListOfSongsAfterGenreSorting.RemoveAt(
                            PlaylistSorter.ListOfSongsAfterGenreSorting.Count - 1);
                    }

                    PlaylistSorter.ListOfSongsAfterGenreSorting.Clear();

                    foreach (var mediaPlaybackItem in qwer)
                        PlaylistSorter.ListOfSongsAfterGenreSorting.Add(mediaPlaybackItem);
                }
                catch (Exception e)
                {

                }

            });
            OnLoad = new CommandBase(ExecuteOnLoad);
            try
            {
                CameraModels = new CameraModels();
            }
            catch (Exception e)
            {

            }

        }

        private void ExecuteOnLoad(object obj)
        {
            CameraModels.CaptureElement = obj as CaptureElement;
            _cameraModelService.StartCamera(CameraModels);


        }


    }
}