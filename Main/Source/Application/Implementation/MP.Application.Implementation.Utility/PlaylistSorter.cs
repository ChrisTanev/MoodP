using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Media.Playback;
using MP.Application.Facade;

namespace MP.Application.Implementation.Utility
{
    public class PlaylistSorter
    {
        public static List<MediaPlaybackItem> ListOfAllLoadedSongs;
        public static ObservableCollection<MediaPlaybackItem> ListOfSongsAfterGenreSorting = new ObservableCollection<MediaPlaybackItem>();

        public static ObservableCollection<MediaPlaybackItem> SortBasedOnGenre(Genres genre)
        {
            var sortedPlaylist = new ObservableCollection<MediaPlaybackItem>();
            if (genre == Genres.Random)
            {
                return new ObservableCollection<MediaPlaybackItem>(ListOfAllLoadedSongs);
            }

            foreach (var song in ListOfAllLoadedSongs)
            {

                if (song.GetDisplayProperties().MusicProperties.Genres.Contains(genre.ToString()))
                {
                    sortedPlaylist.Add(song);
                }
            }
            if (sortedPlaylist.Count < 1)
            {
                return new ObservableCollection<MediaPlaybackItem>(ListOfAllLoadedSongs);
            }
            return sortedPlaylist;
        }

        public static ObservableCollection<MediaPlaybackItem> SortBasedOnMood(Genres genre)
        {
            var sortedPlaylist = new ObservableCollection<MediaPlaybackItem>();
            foreach (var song in ListOfAllLoadedSongs)
            {

                if (song.GetDisplayProperties().MusicProperties.Genres.Contains(genre.ToString()))
                {
                    sortedPlaylist.Add(song);
                }
            }
            return sortedPlaylist;
        }
    }
}
