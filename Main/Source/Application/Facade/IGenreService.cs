namespace MP.Application.Facade
{
    public interface IGenreService
    {
        Genres MapToString(string genre);
        Genres GetCurrentGenre();
        void SetCurrentGenre(Genres g);
    }
}