using System.Threading.Tasks;

namespace MP.Data.Facade
{
    public interface ISerializer
    {
        string Serialize(object value);
        T Deserialize<T>(string data);
        Task<string> SerializeAsync(object value);
        Task<T> DeserializeAsync<T>(string data);
    }
}