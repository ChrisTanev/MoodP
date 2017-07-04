using Windows.Media.Playback;

namespace MP.Application.Implementation.Utility
{
    public static class MediaPlaybackItemHelper
    {
        public static bool Liked;
        public static bool IsLiked(this MediaPlaybackItem mpi)
        {
            return Liked;

        }
    }
}
