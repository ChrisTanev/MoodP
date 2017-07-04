using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using MP.Data.Facade;

namespace MP.Data.Implementation.Json
{
    public class UniversalSerializer : IUniversalSerializer
    {
        public  async Task<T> ReadObjectFromXmlFileAsync<T>(string filename)  
        {
            T objectFromXml = default(T);
            var serializer = new XmlSerializer(typeof(T));
            StorageFolder folder = KnownFolders.MusicLibrary;
            StorageFile file = await folder.GetFileAsync(filename);
            Stream stream = await file.OpenStreamForReadAsync();
            objectFromXml = (T) serializer.Deserialize(stream);
            stream.Dispose();
            return objectFromXml;
        }

      public  async Task SaveObjectToXml<T>(T objectToSave, string filename)
        {
         
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                StorageFolder folder = KnownFolders.MusicLibrary;
                StorageFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                Stream stream = await file.OpenStreamForWriteAsync();

                using (stream)
                {
                    serializer.Serialize(stream, objectToSave);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
