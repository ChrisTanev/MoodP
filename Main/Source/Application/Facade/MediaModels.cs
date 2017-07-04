using System.Collections.ObjectModel;
using Windows.Media.Playback;
using Windows.UI.Xaml.Controls;

namespace MP.Application.Facade
{
    public class MediaModels : ModelBase 
    {
        private ObservableCollection<MediaPlaybackItem> _listOfMediaPlaybackItems;
        public SongBinder SongBinder { get; set; }
        public MediaModels()
        {

            MediaElement = new MediaElement();
            MediaPlaybackList = new MediaPlaybackList();
            SongBinder = new SongBinder();
            _listOfMediaPlaybackItems =  new ObservableCollection<MediaPlaybackItem>();
        }

        public MediaElement MediaElement { get; set; }
        public MediaPlaybackList MediaPlaybackList { get; set; }

        public ObservableCollection<MediaPlaybackItem> ListOfMediaPlaybackItems
        {
            get { return _listOfMediaPlaybackItems; }
            set
            {
                _listOfMediaPlaybackItems = value;
                OnPropertyChanged();
            }
        }

        public void FactoryCreate(object obj)
        {
            MediaElement = new MediaElement();
            MediaElement = obj as MediaElement;
            ListOfMediaPlaybackItems = new ObservableCollection<MediaPlaybackItem>();
            MediaPlaybackList = new MediaPlaybackList();
        }

        public bool IsNull()
        {
            return _listOfMediaPlaybackItems.Count <= 1;
        }

        public void AfterPlaylistChange()
        {
            MediaPlaybackList = new MediaPlaybackList();
            foreach (var item in ListOfMediaPlaybackItems)
            {
               MediaPlaybackList.Items.Add(item);
            }
            MediaElement.SetPlaybackSource(MediaPlaybackList);
        }
    }
}