using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using MP.Application.Facade;

namespace MP.Application.Implementation.Utility
{
    public class Mapper
    {

        public static Dictionary<string, string> FileNames = new Dictionary<string, string>();

        public static async Task MapSongMetaFromStorageFileToMediaPlaybackItem(StorageFile item,
            MediaPlaybackItem mpbItem)
        {

            var mediaMeta = mpbItem.GetDisplayProperties();
            var mp = await item.Properties.GetMusicPropertiesAsync();
            try
            {
                const uint size = 200;
                var strgfil = await item.GetThumbnailAsync(ThumbnailMode.MusicView, size, ThumbnailOptions.UseCurrentScale);
                var bmi = new BitmapImage();
                await bmi.SetSourceAsync(strgfil);
                mediaMeta.MusicProperties.Artist = mp.Artist;
                mediaMeta.MusicProperties.Title = mp.Title;
                foreach (var genre in mp.Genre)
                {
                    mediaMeta.MusicProperties.Genres.Add(genre);
                }
                mediaMeta.Thumbnail = RandomAccessStreamReference.CreateFromStream(strgfil);
                FileNames.Add(mediaMeta.MusicProperties.Title + "" + mediaMeta.MusicProperties.Artist, item.Name);
            }
            catch (Exception)
            {
                //Ignore
            }

            mpbItem.ApplyDisplayProperties(mediaMeta);
        }

        public List<SongBinder> MapMediaPlaybackItemToSongMeta(List<MediaPlaybackItem> listOfPlaybackItems)
        {

            List<SongBinder> songs = new List<SongBinder>();
            foreach (var item in listOfPlaybackItems)
            {
                songs.Add(new SongBinder
                {
                    Artist = item.GetDisplayProperties().MusicProperties.Artist,
                    Title = item.GetDisplayProperties().MusicProperties.Title,

                });
            }
            return songs;
        }


    }
}
