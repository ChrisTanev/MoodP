using System.Collections.Generic;
using MP.Application.Facade;

namespace MP.Application.Implementation.Utility
{
    public class GenreService : IGenreService
    {
        public static Genres CurrentGenre { get; set; }
        private readonly Dictionary<string, Genres> _dict;

        public GenreService()
        {
            _dict = new Dictionary<string, Genres>
            {
                 { "Random", Genres.Random},
                 { "Dance", Genres.Dance},
                 { "Disco", Genres.Disco},
                 { "Opera", Genres.Opera },
                 { "Pop", Genres.Pop},
                 {"Techno",Genres.Techno },
                 {"Rock",Genres.Rock }
            };
        }
        public Genres MapToString(string genre)
        {
         
            return _dict[genre];


        }

        public Genres GetCurrentGenre()
        {
            return CurrentGenre;
            
        }

        public void SetCurrentGenre(Genres g)
        {
            CurrentGenre = g;
        }
    }
}