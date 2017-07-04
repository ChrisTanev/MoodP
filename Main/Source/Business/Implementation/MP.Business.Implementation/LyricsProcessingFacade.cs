using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Media.Playback;
using MP.Application.Facade;
using MP.Business.Facade;
using MP.Business.Implementation.NaiveBayes;
using MP.Data.Facade;

namespace MP.Business.Implementation
{
    public class LyricsProcessingFacade : ILyricsProcessingFacade
    {
         Classifier classifier = new Classifier();
        private ILyricsAccess iLyricsAccess;
        public LyricsProcessingFacade(ILyricsAccess ila)
        {
          
            iLyricsAccess = ila;
            Teach();
        }

        public ObservableCollection<MediaPlaybackItem> ProcessLyricsAndMood(ObservableCollection<MediaPlaybackItem> listOfMediaPlaybackItems)
        {
            var temp = new ObservableCollection<MediaPlaybackItem>();
            try
            {
                SongBinder songBinder;
                string[] words = new string[] { };
                foreach (var song in listOfMediaPlaybackItems)
                {
                    songBinder = new SongBinder
                    {
                        Artist = song.GetDisplayProperties().MusicProperties.Artist,
                        Title = song.GetDisplayProperties().MusicProperties.Title
                    };

                    var laa = iLyricsAccess.GetTrackLyrics(songBinder);
                   var  PorterStemmer = new WordStemmer();
                 var stemmed =   PorterStemmer.StemWord(laa);
                    words = StringManipulation.GetWords(stemmed);
                    var resultDict = ClassifyLyrics(words);
                   var max = resultDict.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                    if (MatchingMoodAndLyricsResults(max))
                    {
                        temp.Add(song);
                    }
                }


            }
            catch (Exception e)
            {


            }
            listOfMediaPlaybackItems = new ObservableCollection<MediaPlaybackItem>(temp);
            return listOfMediaPlaybackItems;
        }

        public bool MatchingMoodAndLyricsResults(string lyricsMood)
        {
            return lyricsMood == MoodService.CurrentMood.ToString();

        }

        public void Teach()
        {

            var happyCategory = new Category(Mood.Happiness.ToString(), null);
            var sadCategory = new Category(Mood.Sadness.ToString(), null);
            sadCategory.TeachPhrases(LyricsTrainingSet.SadPhrases);
            happyCategory.TeachPhrases(LyricsTrainingSet.HappyPhrases);
            classifier.Categories.Add(happyCategory.Name, happyCategory);
            classifier.Categories.Add(sadCategory.Name, sadCategory);
        }
        public Dictionary<string, double> ClassifyLyrics(string[] lyrics)
        {


            return classifier.Classify(lyrics);

        }
    }
}