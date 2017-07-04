using Windows.Storage;
using Windows.Storage.AccessCache;
using MP.Data.Facade;

namespace MP.Data.Implementation
{
  public   class FolderAccess : IFolderAccess
    {


        public void AddToFutureList( StorageFolder folder)
        {
            FolderAccessToken.Token = StorageApplicationPermissions.FutureAccessList.Add(folder);
            ApplicationData.Current.LocalSettings.Values["FolderAccessToken.Token"] = FolderAccessToken.Token;
        }

        public bool ContainsToken()
        {
            return ApplicationData.Current.LocalSettings.Values.ContainsKey("FolderAccessToken.Token");

        }

        public void SetToken()

        {
            FolderAccessToken.Token = (string)ApplicationData.Current.LocalSettings.Values["FolderAccessToken.Token"];
        }
    }
}
