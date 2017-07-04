using System;
using System.Threading.Tasks;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;

namespace MP.Application.Implementation.Utility
{
    public class PictureProperties
    {

        public static ImageEncodingProperties CreatePImageProperties()
        {

            ImageEncodingProperties imgFormat = ImageEncodingProperties.CreateJpeg();
            imgFormat.Height = 720;
            imgFormat.Width = 1280;
            return imgFormat;

        }

        public static async Task<byte[]> ConvertImageToByte(StorageFile file)
        {
            byte[] fileBytes;
            using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
            {
                fileBytes = new byte[stream.Size];
                using (DataReader reader = new DataReader(stream))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(fileBytes);
                }
            }

            return fileBytes;
        }
    }
}
