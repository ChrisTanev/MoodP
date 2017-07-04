namespace MP.Business.Facade
{
 
    public interface IMoodService
    {
        Mood GetCurrentMood();
        void SetCurrentMood(Mood mood);
        Mood GetMoodFromString(string mood);
    }
}