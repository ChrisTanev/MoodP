using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace MP.Data.Facade
{
    public interface IMusicFileAccess
    {
        string GetStorageFolder();
        Task<Tuple<IRandomAccessStream, string>> GetMusicFileAsync();
        Task<Tuple<List<IRandomAccessStream>, string>> GetListOfMusicFilesAsync();
        Task<IReadOnlyList<StorageFile>> GetMusicFromFolder(IFolderAccess ifc);
        Task<StorageFile> GetSingleFileAsync(string deserializedPlaylistData,string folderpath);
        string GetStorageFolderPath();
        Task<List<StorageFile>> GetMusic(IFolderAccess ifc);
    }
}