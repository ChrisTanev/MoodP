using System;
using System.Net.Http;
using MP.Application.Facade;
using MP.Data.Facade;

namespace MP.Data.Implementation
{
    public class LyricsApiAccess : ILyricsAccess
    {
        private static readonly string ApiKey = "3748ee24b88168a0067b521f0abb79bc";
        private static readonly string BaseUrl = "http://api.musixmatch.com/ws/1.1/";
        private  ISerializer _isz ;
        private string _lyricsBody;

        public LyricsApiAccess(ISerializer isSerializer)
        {
            _isz = isSerializer;
        }

        public string GetTrackLyrics(SongBinder songBinder)
        {
     
            using (var client = new HttpClient())
            {
                //getting the lyrics , only 30% tho
                //http://api.musixmatch.com/ws/1.1/matcher.lyrics.get?q_track=sexy%20and%20i%20know%20it&q_artist=lmfao&apikey=3748ee24b88168a0067b521f0abb79bc
                client.DefaultRequestHeaders.Add("apikey", ApiKey);

                client.BaseAddress = new Uri(BaseUrl);

                using (var request = new HttpRequestMessage(HttpMethod.Get, string.Format(client.BaseAddress +
                                                                                          "matcher.lyrics.get?q_artist={0}&q_track={1}&apikey={2}",
                    songBinder.Artist, songBinder.Title, ApiKey)))
                {

                    var post = client.GetStringAsync(request.RequestUri);

                    var deserializedObjs = _isz.Deserialize<RootObject>(post.Result);
                    _lyricsBody = deserializedObjs.message.body.lyrics.lyrics_body;

                }
            }
            return _lyricsBody;
        }

    }
}