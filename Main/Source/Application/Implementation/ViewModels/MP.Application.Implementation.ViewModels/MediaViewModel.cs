using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using MP.Application.Facade;
using MP.Application.Implementation.Utility;
using MP.Business.Facade;
using MP.Data.Facade;

namespace MP.Application.Implementation.ViewModels
{
    public class MediaViewModel : BaseViewModel
    {

        private SongMeta SongMeta { get; set; }
        public MediaModels MediaModels { get; set; }
        List<MediaPlaybackItem> _mbp = new List<MediaPlaybackItem>();
        private readonly IViewModelService _iVmService;
        private readonly IGenreService _genreService;
        //   private readonly ILyricsAccess _iLyricsAccess;
        private readonly IUniversalSerializer _iUniversalSerializer;
        private readonly IMusicFileAccess _iMusicFileAccess;
        private readonly IPlaylistNaming _iPlaylistNaming;
        public ICommand OnPlayButton { get; set; }
        public ICommand OnShuffle { get; set; }
        public ICommand OnRepeat { get; set; }
        public ICommand OnPrevSong { get; set; }
        public ICommand OnNextSong { get; set; }
        public ICommand OnSelectedSongFromList { get; set; }
        public ICommand OnLoadMusic { get; set; }
        public ICommand OnCapture { get; set; }
        public ICommand GoToPage { get; set; }
        public ICommand MapChosenGenre { get; set; }
        public ICommand LikedSong { get; set; }
        public ICommand SavePlaylist { get; set; }
        public ICommand LoadSavedPlaylist { get; set; }

        public MediaViewModel(INavigationService navigationService, INavigationPage page, IViewModelService iService,
                IGenreService ign, IUniversalSerializer iuni, IMusicFileAccess imfa, IPlaylistNaming ipn)
            //, ILyricsAccess ila
        {

            PlaylistSorter.ListOfSongsAfterGenreSorting.CollectionChanged += CurrentPlaylistChanged;

            SongMeta = new SongMeta();
            GoToPage = new CommandBase(o =>
            {
                navigationService.Navigate(page.PageType);
            });
            //    _iLyricsAccess = ila;
            _iVmService = iService;
            _genreService = ign;
            _iUniversalSerializer = iuni;
            _iMusicFileAccess = imfa;
            _iPlaylistNaming = ipn;
            try
            {
                MediaModels = new MediaModels();
            }
            catch (Exception e)
            {
            
            }
          
            OnPlayButton = new CommandBase(Play);
            OnShuffle = new CommandBase(Shuffle);
            OnRepeat = new CommandBase(Repeat);
            OnPrevSong = new CommandBase(PreviousSong);
            OnNextSong = new CommandBase(NextSong);
            OnCapture = new CommandBase(o =>
            {
                navigationService.Navigate(page.PageType);

            });
            OnSelectedSongFromList = new CommandBase(SelectedSongFromList);
            OnLoadMusic = new CommandBase(LoadMusic);
            MapChosenGenre = new CommandBase(MapChosenGenreToCurrenGenre);
            LikedSong = new CommandBase(OnLikeSongClick);
            SavePlaylist = new CommandBase(OnSavePlaylistClick);
            LoadSavedPlaylist = new CommandBase(OnLoadPlaylistClick);
        }

        public void CurrentPlaylistChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                MediaModels.AfterPlaylistChange();
            }
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                MediaModels.ListOfMediaPlaybackItems = PlaylistSorter.ListOfSongsAfterGenreSorting;
            }

        }


        private async void LoadMusic(object obj)
        {

            try
            {
                MediaModels.FactoryCreate(obj);
                await
                    _iVmService.LoadMusic(obj as MediaElement, MediaModels.ListOfMediaPlaybackItems,
                        MediaModels.MediaPlaybackList,
                        MediaModels, null);
                SongMeta.FolderPath = _iMusicFileAccess.GetStorageFolderPath(); 
            }
            catch (Exception e)
            {
                
            }

        
        }

        private async void OnLoadPlaylistClick(object obj)
        {
            MediaModels.FactoryCreate(obj);
          
            SongMeta =
                await _iUniversalSerializer.ReadObjectFromXmlFileAsync<SongMeta>(_iPlaylistNaming.GetFavouritePlaylistName());



            var strgFolder = await StorageFolder.GetFolderFromPathAsync(SongMeta.FolderPath);
            List<StorageFile> list = new List<StorageFile>();
            foreach (var song in SongMeta.Songs)
            {
                list.Add(await strgFolder.GetFileAsync(song.FileName));

            }
            await
                _iVmService.LoadMusic(obj as MediaElement, MediaModels.ListOfMediaPlaybackItems,
                    MediaModels.MediaPlaybackList,
                    MediaModels, list);
          
        }

        private async void OnSavePlaylistClick(object obj)
        {
           
            await _iUniversalSerializer.SaveObjectToXml(SongMeta, _iPlaylistNaming.GetFavouritePlaylistName());

        }

        private void OnLikeSongClick(object obj)
        {

            try
            {
                var currentSong = MediaModels.MediaPlaybackList.CurrentItem;
                var artist = currentSong.GetDisplayProperties().MusicProperties.Artist;
                var title = currentSong.GetDisplayProperties().MusicProperties.Title;
                if (_mbp.Contains(currentSong))
                {
                    _mbp.Remove(currentSong);
                    SongMeta.Songs.RemoveAll(id => id.Artist == artist && id.Title == title);
                }
                else
                {
                    string fileName;
                    Mapper.FileNames.TryGetValue(title + "" + artist, out fileName);

                    SongMeta.Songs.Add(new SongsId { Artist = artist, Title = title, FileName = fileName });
                    _mbp.Add(currentSong);
                }
            }
            catch (Exception)
            {
                //ignore
            }

        }

        private void MapChosenGenreToCurrenGenre(object obj)
        {
            try
            {
                GenreService.CurrentGenre = _genreService.MapToString(obj as string);
                MediaModels.MediaElement.Stop();
                MediaModels.MediaPlaybackList = new MediaPlaybackList();
                MediaModels.ListOfMediaPlaybackItems =
                    new ObservableCollection<MediaPlaybackItem>(
                        PlaylistSorter.SortBasedOnGenre(GenreService.CurrentGenre));
            
            }
            catch (Exception e)
            {


            }
            PlaylistSorter.ListOfSongsAfterGenreSorting.Clear();
            PlaylistSorter.ListOfSongsAfterGenreSorting = MediaModels.ListOfMediaPlaybackItems;

            MediaModels.MediaElement.AutoPlay = true;
            MediaModels.MediaElement.Play();
            OnMediaItemChanged(MediaModels.MediaPlaybackList);

        }




        private async void OnMediaItemChanged(MediaPlaybackList sender)
        {
            try
            {

                var artist = sender.CurrentItem.GetDisplayProperties().MusicProperties.Artist;
                var title = sender.CurrentItem.GetDisplayProperties().MusicProperties.Title;
                MediaModels.SongBinder.Artist = string.IsNullOrEmpty(artist) ? "Unknown Artist" : artist;
                MediaModels.SongBinder.Title = string.IsNullOrEmpty(title) ? "Unknown Title" : title;
                MediaModels.SongBinder.ArtCover = null;
                var bmi = new BitmapImage();
                var qwe = await sender.CurrentItem.GetDisplayProperties().Thumbnail.OpenReadAsync();
                await bmi.SetSourceAsync(qwe);
                MediaModels.SongBinder.ArtCover = bmi;
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public void SelectedSongFromList(object obj)
        {
            _iVmService.SelectedSong(MediaModels, obj);
            if (MediaModels.MediaElement.CurrentState != MediaElementState.Playing)
            {
                MediaModels.MediaElement.Play();
            }
            OnMediaItemChanged(MediaModels.MediaPlaybackList);
         }

        public void Play(object o)
        {
            _iVmService.Play(MediaModels.MediaElement);
        }

        public void NextSong(object o)
        {

            _iVmService.NextSong(MediaModels);
            OnMediaItemChanged(MediaModels.MediaPlaybackList);
            try
            {
                var cont = o as ListView;
                if (cont != null) cont.SelectedIndex = (int)MediaModels.MediaPlaybackList.CurrentItemIndex;
            }
            catch (Exception e)
            {


            }

        }

        public void PreviousSong(object o)
        {
            _iVmService.PreviousSong(MediaModels);
            OnMediaItemChanged(MediaModels.MediaPlaybackList);
            try
            {
                var cont = o as ListView;
                if (cont != null) cont.SelectedIndex = (int)MediaModels.MediaPlaybackList.CurrentItemIndex;
            }
            catch (Exception e)
            {


            }

        }

        public void Repeat(object o)
        {
            _iVmService.Repeat(MediaModels);
        }


        public void Shuffle(object o)
        {
            _iVmService.Shuffle(MediaModels);
        }
    }
}