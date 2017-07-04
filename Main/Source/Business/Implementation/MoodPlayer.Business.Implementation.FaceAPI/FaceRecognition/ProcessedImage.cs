namespace MP.Business.Implementation.FaceAPI.FaceRecognition
{
    public enum Emotion
    {
        Happy,
        Sad,
        Suprise
    }
    public class ProcessedImage
    {
  
        public string ImagePath { get; set; } = "";
        public long Score { get; set; } = 0;
        public string Mood { get; set; }


    }
}