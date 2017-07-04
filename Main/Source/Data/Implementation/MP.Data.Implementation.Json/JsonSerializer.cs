using System.Threading.Tasks;
using MP.Data.Facade;
using Newtonsoft.Json;

namespace MP.Data.Implementation.Json
{
    public class JsonSerializer : ISerializer
    {
        string ISerializer.Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        T ISerializer.Deserialize<T>(string data)
        {
            if (string.IsNullOrEmpty(data))
                return default(T);

            return JsonConvert.DeserializeObject<T>(data);
        }

        Task<string> ISerializer.SerializeAsync(object value)
        {
            return Task.Factory.StartNew(() => JsonConvert.SerializeObject(value));
        }

        Task<T> ISerializer.DeserializeAsync<T>(string data)
        {
            if (string.IsNullOrEmpty(data))
                return new Task<T>(() => default(T));

            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(data));
        }
    }
}