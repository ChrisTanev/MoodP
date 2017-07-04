using HyperMock;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MP.Application.Facade;
using MP.Business.Facade;
using MP.Business.Implementation;
using MP.Data.Facade;
using MP.Data.Implementation;
using MP.Data.Implementation.Json;
namespace MP.Data.UnitTests
{
    [TestClass]
    public class ApiUnitTests
    {
        [TestMethod]
        public void ApiTest()
        {
            var imood = new Mock<IMoodService>(new MoodService(), new MockProxyDispatcher(), MockBehavior.Loose);
            //var iser = new Mock<ISerializer>(new JsonSerializer(), new MockProxyDispatcher(), MockBehavior.Loose);
            //var laa = new LyricsApiAccess(iser.Object);
            //var s = new SongBinder
            //{
            //    Artist = "Justin bieber",
            //    Title = "Baby"
            //};
            //var result = laa.GetTrackLyrics(s);
            //var ers = StringManipulation.GetWords(result);
            //Assert.IsNotNull(result);
            //Assert.IsTrue(result.Length >1);
        }


     }
}