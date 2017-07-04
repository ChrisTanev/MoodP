
using System.Threading.Tasks;

namespace MP.Data.Facade
{
   public interface IUniversalSerializer
   {
       Task<T> ReadObjectFromXmlFileAsync<T>(string filename);
       Task SaveObjectToXml<T>(T objectToSave, string filename);
   }
}
