using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MP.Application.Facade
{
    /// <summary>
    ///     Holds properties for SongBinder
    /// </summary>
    /// 

    public class SongBinder : ModelBase
    {
        private string _artist;
        private string _title;
        private BitmapImage _bitmap;
        private ImageSource _image;

   

       #region Getters and Setters

        public ImageSource ArtCover
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage BitmapImage
        {
            get { return _bitmap; }
            set
            {
                _bitmap = value;
                OnPropertyChanged();
            }
        }
     
        public string Artist
        {
            get { return _artist; }
            set
            {
                _artist = value;
                OnPropertyChanged();
            }
        }
 
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}