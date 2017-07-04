using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using MP.Application.Facade;
using MP.Data.Facade;

namespace MP.Application.Implementation.Utility
{
    public class MediaViewModelService : IViewModelService
    {
        private MediaSource _mediaSource;
        private readonly IMusicFileAccess _msa;
        private readonly IFolderAccess _iFolderAccess;

        public MediaViewModelService(IMusicFileAccess msFileAccess, IFolderAccess ifa)
        {
            _msa = msFileAccess;
            _iFolderAccess = ifa;
        }

        public void Play(MediaElement media)
        {
            try
            {
                if (media.CurrentState == MediaElementState.Playing)
                    media.Pause();
                else
                    media.Play();
            }
            catch (FileLoadException)
            {

            }

        }

        public void NextSong(MediaModels mediaModels)
        {
            if (!mediaModels.IsNull())
                if (mediaModels.MediaElement.CurrentState == MediaElementState.Playing)
                {
                    mediaModels.MediaElement.Pause();
                }
            mediaModels.MediaPlaybackList.MoveNext();
            mediaModels.MediaElement.Play();
        }

        public void PreviousSong(MediaModels mediaModels)
        {
            if (!mediaModels.IsNull())
                if (mediaModels.MediaElement.CurrentState == MediaElementState.Playing)
                {
                    mediaModels.MediaElement.Pause();
                }
            mediaModels.MediaPlaybackList.MovePrevious();
            mediaModels.MediaElement.Play();
        }

        public void Shuffle(MediaModels mediaModels)
        {
            if (!mediaModels.IsNull())
                mediaModels.MediaPlaybackList.AutoRepeatEnabled = !mediaModels.MediaPlaybackList.AutoRepeatEnabled;
        }

        public void Repeat(MediaModels mediaModels)
        {
            if (!mediaModels.IsNull())
                mediaModels.MediaPlaybackList.ShuffleEnabled = !mediaModels.MediaPlaybackList.ShuffleEnabled;
        }

        public async Task LoadMusic(MediaElement media, ObservableCollection<MediaPlaybackItem> listOfMediaPlaybackItems,
            MediaPlaybackList mediaList, MediaModels mm, List<StorageFile> files)
        {
            try
            {

                if (files == null)
                {
                    files = await _msa.GetMusic(_iFolderAccess);
                }
                if (files == null)
                {
                    return;
                }
                if (files.Count > 0)
                    foreach (var item in files)
                    {
                        _mediaSource = MediaSource.CreateFromStorageFile(item);
                        var mediaPlaybackItem = new MediaPlaybackItem(_mediaSource);
                        listOfMediaPlaybackItems.Add(mediaPlaybackItem);
                        mediaList.Items.Add(mediaPlaybackItem);
                        await Mapper.MapSongMetaFromStorageFileToMediaPlaybackItem(item, mediaPlaybackItem);
                    }
                //adding all songs with meta to constant list 
                PlaylistSorter.ListOfAllLoadedSongs = new List<MediaPlaybackItem>(listOfMediaPlaybackItems);
                media.SetPlaybackSource(mediaList);
                mm.SongBinder.Artist = mediaList.Items[0].GetDisplayProperties().MusicProperties.Artist;
                mm.SongBinder.Title = mediaList.Items[0].GetDisplayProperties().MusicProperties.Title;
                const uint size = 200;
                var strgfil = await files[0].GetThumbnailAsync(ThumbnailMode.MusicView, size, ThumbnailOptions.UseCurrentScale);
                var bmi = new BitmapImage();
                await bmi.SetSourceAsync(strgfil);
                mm.SongBinder.ArtCover = bmi;
            }
            catch (Exception e)
            {
                // ignored
                return;
            }
        }

        public void SelectedSong(MediaModels mediaModels, object o)
        {
          //  if (mediaModels.IsNull()) return;
            var control = o as ListView;
            if (mediaModels.MediaPlaybackList.CurrentItemIndex < 0 || mediaModels.MediaPlaybackList.CurrentItemIndex >1000000)
            {
          
                return;
            }
            if (control != null) mediaModels.MediaPlaybackList.MoveTo((uint)control.SelectedIndex);
         
        }

    }
}