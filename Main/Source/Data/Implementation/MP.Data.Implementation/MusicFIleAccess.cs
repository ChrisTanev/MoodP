using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using MP.Data.Facade;

namespace MP.Data.Implementation
{
    /// <summary>
    ///     Accesing files from local drive
    /// </summary>
    public class MusicFileAccess : IMusicFileAccess
    {


        public static StorageFolder StorageFolder;

        public async Task<Tuple<IRandomAccessStream, string>> GetMusicFileAsync()
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.MusicLibrary
            };
            openPicker.FileTypeFilter.Add(".mp3");
            var file = await openPicker.PickSingleFileAsync();
            var fileStream = await file.OpenAsync(FileAccessMode.Read);
            return Tuple.Create(fileStream, file.ContentType);
        }

        public string GetStorageFolder()
        {
            return StorageFolder.Path;
    }

        public async Task<Tuple<List<IRandomAccessStream>, string>> GetListOfMusicFilesAsync()
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.MusicLibrary
            };
            openPicker.FileTypeFilter.Add(".mp3");
            var files = await openPicker.PickMultipleFilesAsync();

            var listOfsongs = new List<IRandomAccessStream>();
            foreach (var x1 in files)
                listOfsongs.Add(await x1.OpenAsync(FileAccessMode.Read));
            return Tuple.Create(listOfsongs.ToList(), ".mp3");
        }

        public async Task<IReadOnlyList<StorageFile>> GetMusicFromFolder(IFolderAccess ifc)
        {
            var fp = new FolderPicker
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.MusicLibrary
            };
            fp.FileTypeFilter.Add(".mp3");
            fp.CommitButtonText = "Choose location";
            fp.ContinuationData["Operation"] = "OpenFolder";
            StorageFolder = await fp.PickSingleFolderAsync();
            ifc.AddToFutureList(StorageFolder);

            var files = await StorageFolder.GetFilesAsync();
            return files;
        }


        public async Task<StorageFile> GetSingleFileAsync(string deserializedPlaylistDataFilePath, string folderPath)
        {
            StorageFolder storageFolder = await StorageFolder.GetFolderFromPathAsync(folderPath);
            var storageFile = await storageFolder.GetFileAsync(deserializedPlaylistDataFilePath);
            return storageFile;
        }

        public string GetStorageFolderPath()
        {

            return StorageFolder.Path;
        }


        public async Task<List<StorageFile>> GetMusic(IFolderAccess ifc)
        {
            List<StorageFile> list = null;
            try
            {
                var fp = new FolderPicker
                {
                    ViewMode = PickerViewMode.List,
                    SuggestedStartLocation = PickerLocationId.MusicLibrary
                };
                fp.FileTypeFilter.Add(".mp3");
                fp.CommitButtonText = "Choose location";
                fp.ContinuationData["Operation"] = "OpenFolder";
                var strgFol = await fp.PickSingleFolderAsync();
                ifc.AddToFutureList(strgFol);

                StorageFolder = strgFol;
                var files = await StorageFolder.GetFilesAsync();
                list = files.ToList();
              
            }
            catch (Exception e)
            {

            }
            return list;
        }
    }

}