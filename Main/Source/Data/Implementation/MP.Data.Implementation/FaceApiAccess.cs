using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MP.Data.Facade;

namespace MP.Data.Implementation
{
    public class FaceApiAccess :IFaceApiAccess 
    {
        private readonly ISerializer _isz;
        private List<MoodResult> _moodResult;

        public FaceApiAccess(ISerializer isz)
        {
            _isz = isz;
        }

        public async Task<List<MoodResult>> Post(byte[] pic)
        {
            string res;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost/MP.Business.Implementation.FaceAPI/");
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                using (
                    var request = new HttpRequestMessage(HttpMethod.Post,
                        client.BaseAddress + "api/Recognition/Recognize/"))

                {
                    request.Content = new ByteArrayContent(pic);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    var response = await client.PostAsync(request.RequestUri, request.Content);

                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();

                        res = result.Result;
                        try
                            {
                            _moodResult = _isz.Deserialize<List<MoodResult>>(res);
                        }
                        catch (Exception e)
                        {


                        }
                        
                    }


                }
            }

            return _moodResult;
        }

      
        }
    }

