using MP.Business.Facade;

namespace MP.Business.Implementation
{
    public class MoodService : IMoodService
    {
        public static Mood CurrentMood { get; set; }

        public  Mood GetMoodFromString(string mood)
        {

            switch (mood)
            {
                case "Happy":
                    return Mood.Happiness;
                case "Sad":
                    return Mood.Sadness;
                case "Suprise":
                    return Mood.Surprise;

            }
            return Mood.Neutral;
        }

        public Mood GetCurrentMood()
        {
            return CurrentMood;
        }

        public void SetCurrentMood(Mood mood)
        {
            CurrentMood = mood;
        }
    }
}