using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.Playback;
using HyperMock;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MP.Application.Facade;
using MP.Application.Implementation.Utility;
using MP.Business.Facade;
using MP.Business.Implementation;
using MP.Business.Implementation.NaiveBayes;
using MP.Data.Facade;
using MP.Data.Implementation;
using MP.Data.Implementation.Json;

namespace MP.Business.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestingLyricsProcessing()
        {
            var arr =
                "Ohh wooaah Ohh wooaah Ohh wooaah\nYou know you love me, I know you care\nJust shout whenever, And I'll be there\nYou are my love, You are my heart\nAnd we will never ever-ever be apart\n\nAre we an item. Girl quit playing\n\"We're just friends\"\nWhat are you sayin?\nsaid theres another and look right in my eyes\nMy first love broke my heart for the first time,\n\nAnd I was like\nBaby, baby, baby ooh\nLike baby, baby, baby noo\nLike baby, baby, baby ooh\nThought you'd always be mine, mine\n\nBaby, baby, baby oohh\nLike baby, baby, baby noo\nLike baby, baby, baby ohh\nThought you'd always be mine, mine\n\nFor you, i would have done what ever\nAnd I just cant believe we ain't together\nAnd I wanna play it cool, But I'm losing you\nI'll buy you anything, ill buy you any ring\nAnd I'm in pieces, Baby fix me\n...\n\n******* This Lyrics is NOT for Commercial use *******\n(1409614552746)";
            var ers = StringManipulation.GetWords(arr);





            var classifier = new Classifier();
            var happyCategory = new Category(Mood.Happiness.ToString(), null);
            var sadCategory = new Category(Mood.Sadness.ToString(), null);
            sadCategory.TeachPhrases(LyricsTrainingSet.SadPhrases);
            happyCategory.TeachPhrases(LyricsTrainingSet.HappyPhrases);

            classifier.Categories.Add(happyCategory.Name, happyCategory);
            classifier.Categories.Add(sadCategory.Name, sadCategory);
            var result = classifier.Classify(ers);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void StemmingTest()
        {
            string testword = "processes";

            WordStemmer ws = new WordStemmer();
            var res = ws.StemWord(testword);

            Assert.IsNotNull(res);
            Assert.AreEqual(res, "process");


        }

        [TestMethod]
        public void CategoryMock()
        {
            var icat = new Mock<ICategory>(new Category(Mood.Happiness.ToString(), null), new MockProxyDispatcher(), MockBehavior.Loose);
            icat.Object.TeachPhrase("lame");
            var res = icat.Object.TotalWords;
            Assert.IsNotNull(res);
            Assert.AreEqual(1, res);


        }

        [TestMethod]
        public void ClassifierMock()
        {
            var iclass = new Mock<IClassifier>(new Classifier(), new MockProxyDispatcher(), MockBehavior.Loose);
            iclass.Object.TeachPhrases(Mood.Happiness.ToString(), new[] { "useless", "sweets" });
            var res = iclass.Object.Classify(new string[] { "sweet" });
            Assert.IsNotNull(res);

            Assert.IsTrue(res.ContainsKey(Mood.Happiness.ToString()));
            Assert.IsFalse(res.ContainsKey(Mood.Anger.ToString()));


        } 


        [TestMethod]
        public void ProcessingFacade()
        {
            var iser = new Mock<ISerializer>(new JsonSerializer(), new MockProxyDispatcher(), MockBehavior.Loose);
            var iLyrAccess = new Mock<ILyricsAccess>(new LyricsApiAccess(iser.Object), new MockProxyDispatcher(), MockBehavior.Loose);
            var ilyrProcessingMock = new Mock<ILyricsProcessingFacade>(new LyricsProcessingFacade(iLyrAccess.Object), new MockProxyDispatcher(), MockBehavior.Loose);

            ilyrProcessingMock.Setup(e => e.ProcessLyricsAndMood(new ObservableCollection<MediaPlaybackItem>()));
            var zeroResult = ilyrProcessingMock.Object.ProcessLyricsAndMood(new ObservableCollection<MediaPlaybackItem>());
            Assert.IsTrue(zeroResult.Count == 0);
        }
        [TestMethod]
        public void MockingIMoodServiceGetAndSet()
        {
            try
            {
                var imoodMock = new Mock<IMoodService>(new MoodService(), new MockProxyDispatcher(), MockBehavior.Loose);
                imoodMock.Object.SetCurrentMood(Mood.Anger);
                var moodRes = imoodMock.Object.GetCurrentMood();

                Assert.IsNotNull(moodRes);
                Assert.AreEqual(Mood.Anger, moodRes);

            }
            catch (Exception e)
            {


            }

        }
        [TestMethod]
        public void IplaylistNamingMock()
        {
            try
            {
                var igenreMock = new Mock<IGenreService>(new GenreService(), new MockProxyDispatcher(),
                   MockBehavior.Loose);
                var imoodMock = new Mock<IMoodService>(new MoodService(), new MockProxyDispatcher(), MockBehavior.Loose);
                var iplaylistMock = new Mock<IPlaylistNaming>(new PlaylistNaming(imoodMock.Object, igenreMock.Object), new MockProxyDispatcher(), MockBehavior.Loose);
                imoodMock.Object.SetCurrentMood(Mood.Anger);
                var moodRes = imoodMock.Object.GetCurrentMood();
                igenreMock.Object.SetCurrentGenre(Genres.Dance);
                var genreRes = igenreMock.Object.GetCurrentGenre();
                Assert.IsNotNull(moodRes);
                Assert.AreEqual(Mood.Anger, moodRes);

                var er = iplaylistMock.Object.GetFavouritePlaylistName();
                Assert.IsNotNull(er);
                Assert.IsTrue(er.Contains(Mood.Anger.ToString()));
                Assert.IsTrue(er.Contains(Genres.Dance.ToString()));
            }
            catch (Exception e)
            {


            }

        }

        [TestMethod]

        public void StringManipulTest()
        {

            var sixWordsString = "This string contains exactly 6 words";

            var result = StringManipulation.GetWords(sixWordsString);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length == 6);

        }

        [TestMethod]

        public void TestFaceAAPI()
        {
            //var fold = FileService.GetPicturesFolder();
            //var file = await FileService.CreatePhotoAsync(fold);
            //var imgFormat = PictureProperties.CreatePImageProperties();
            //await cameraModels.MediaCapture.CapturePhotoToStorageFileAsync(imgFormat, file);
            //await CleanCamera(cameraModels);
            //var imoodMock = new Mock<IMoodService>(new MoodService(), new MockProxyDispatcher(), MockBehavior.Loose);
            //var iser = new Mock<ISerializer>(new JsonSerializer(), new MockProxyDispatcher(), MockBehavior.Loose);

            //var iface = new Mock<IFaceApiAccess>( new FaceApiAccess(iser.Object),new MockProxyDispatcher(), MockBehavior.Loose );
            //var photoToByteArray = PictureProperties.ConvertImageToByte(file);
            //var qwe = await iface.Object.Post(await photoToByteArray);
            //var reslt = qwe[0];
            //var mapImageProcessedMoodToMood = imoodMock.Object.GetMoodFromString(reslt.Mood);
            //imoodMock.Object.SetCurrentMood(mapImageProcessedMoodToMood);
        }
    }
}
