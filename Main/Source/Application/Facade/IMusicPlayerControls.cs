using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace MP.Application.Facade
{
    public interface IMusicPlayerControls
    {
        void Play(MediaElement media);
        void NextSong(MediaModels mediaModels);
        void PreviousSong(MediaModels mediaModels);
        void Shuffle(MediaModels mediaModels);
        void Repeat(MediaModels mediaModels);

       Task LoadMusic(MediaElement media, ObservableCollection<MediaPlaybackItem> listOfSongs, MediaPlaybackList listOfMedia,
            MediaModels mm, List<StorageFile> listofStorageFiles);

        void SelectedSong(MediaModels mediaModels, object o);
    }
}