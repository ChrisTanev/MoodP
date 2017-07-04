
using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace MP.Application.Implementation.Utility
{
  public  class FileService
    {

        public static StorageFolder GetPicturesFolder()
        {
           return KnownFolders.SavedPictures;
        }

        public static async Task<StorageFile> CreatePhotoAsync(StorageFolder storage)
        {

            StorageFile file = await storage.CreateFileAsync(
              "TestPhoto.jpg",
              CreationCollisionOption.ReplaceExisting);

            return file;
        }
    }
}
