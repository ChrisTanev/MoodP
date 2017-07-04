
using MP.Application.Facade;
using MP.Business.Facade;

namespace MP.Business.Implementation
{
    public class PlaylistNaming : IPlaylistNaming
    {

        private readonly IMoodService _iMoodService;
        private readonly IGenreService _iGenreService;
        public PlaylistNaming(IMoodService iMood , IGenreService iGenreService)
        {
            _iMoodService = iMood;
            _iGenreService = iGenreService;
 }
        public string GetFavouritePlaylistName()
        {

            return string.Format("My {0} {1} Favourite Playlist.wpl", _iMoodService.GetCurrentMood().ToString(),
                _iGenreService.GetCurrentGenre().ToString()); 
        }

    }

}