using System.Collections.ObjectModel;
using Windows.Media.Playback;

namespace MP.Business.Facade
{
    public interface ILyricsProcessingFacade
    {
        ObservableCollection<MediaPlaybackItem> ProcessLyricsAndMood(ObservableCollection<MediaPlaybackItem> listOfMediaPlaybackItems);
    }
}

