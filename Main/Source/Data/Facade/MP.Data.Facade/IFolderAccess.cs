using Windows.Storage;

namespace MP.Data.Facade
{
    public interface IFolderAccess
    {
        void AddToFutureList(StorageFolder folder);
        bool ContainsToken();
        void SetToken();

    }
}
