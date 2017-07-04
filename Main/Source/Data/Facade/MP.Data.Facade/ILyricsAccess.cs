
using MP.Application.Facade;

namespace MP.Data.Facade
{
    public interface ILyricsAccess
    {
        string GetTrackLyrics(SongBinder songBinder);
    }
}