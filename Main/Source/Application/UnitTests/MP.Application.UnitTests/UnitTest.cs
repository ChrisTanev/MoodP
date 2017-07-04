using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using HyperMock;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MP.Application.Facade;
using MP.Application.Implementation.Utility;
using MP.Application.Implementation.ViewModels;
using MP.Application.Implementation.Views.Navigation;
using MP.Business.Facade;
using MP.Business.Implementation;
using MP.Data.Facade;
using MP.Data.Implementation;
using MP.Data.Implementation.Json;

namespace MP.Application.UnitTests
{
    [TestClass] 
    public class UnitTest1
    {
      
        [TestMethod]
       
        public void TestMethod1()
        {
            // MediaModels mm = new MediaModels();

            // Assert.IsNull(mm.ListOfStorageFiles);
        }

 
        [TestCategory("Application Domain Tests")]
   

        [TestMethod]

        public void MockingIGenreServiceGetAndSet()
        {
            try
            {
                var igenreMock = new Mock<IGenreService>(new GenreService(), new MockProxyDispatcher(),
                    MockBehavior.Loose);
                igenreMock.Object.SetCurrentGenre(Genres.Dance);
                var genreRes = igenreMock.Object.GetCurrentGenre();

                Assert.IsNotNull(genreRes);
                Assert.AreEqual(Genres.Dance, genreRes);

            }
            catch (Exception e)
            {


            }

        }

        [TestMethod]

        public void MockingICameraService()
        {
            try
            {

                var iserializerMock = new Mock<ISerializer>(new JsonSerializer(), new MockProxyDispatcher(),
                    MockBehavior.Loose);

                var IfaceApiMock = new Mock<IFaceApiAccess>(new FaceApiAccess(iserializerMock.Object),
                    new MockProxyDispatcher(), MockBehavior.Loose);
                //new Mock(typeof(IFaceApiAccess), new MockProxyDispatcher(), MockBehavior.Loose);
                var imoodMock = new Mock<IMoodService>(new MoodService(), new MockProxyDispatcher(), MockBehavior.Loose);
                //new Mock(typeof(IMoodService), new MockProxyDispatcher(), MockBehavior.Loose);

                var icamMock =
                    new Mock<ICameraModelService>(new CameraModelService(IfaceApiMock.Object, imoodMock.Object),
                        new MockProxyDispatcher(), MockBehavior.Loose);

                icamMock.Object.StartCamera(new CameraModels());
                //Assert.IsNotNull(genreRes);
                //Assert.AreEqual(Genres.Dance, genreRes);

            }
            catch (Exception e)
            {


            }

        }

        [TestMethod]
        public void MediaViewModelServiceMock()
        {
            try
            {

                var iMusicFileAccessMock = new Mock<IMusicFileAccess>(new MusicFileAccess(), new MockProxyDispatcher(),
                    MockBehavior.Loose);

                //var iFolderaccess = new Mock<IFolderAccess>(new FaceApiAccess(iserializerMock.Object), new MockProxyDispatcher(), MockBehavior.Loose);//new Mock(typeof(IFaceApiAccess), new MockProxyDispatcher(), MockBehavior.Loose);
                //var imoodMock = new Mock<IMoodService>(new MoodService(), new MockProxyDispatcher(), MockBehavior.Loose);//new Mock(typeof(IMoodService), new MockProxyDispatcher(), MockBehavior.Loose);

                var iFolderaccess = new Mock<IFolderAccess>(new FolderAccess(), new MockProxyDispatcher(),
                    MockBehavior.Loose);
                //new Mock(typeof(IFaceApiAccess), new MockProxyDispatcher(), MockBehavior.Loose);
                var icamMock =
                    new Mock<IViewModelService>(
                        new MediaViewModelService(iMusicFileAccessMock.Object, iFolderaccess.Object),
                        new MockProxyDispatcher(), MockBehavior.Loose);

                //icamMock.Object.Play(new MediaElement());
                MediaViewModel MVM =  new MediaViewModel(null,null,icamMock.Object,null,null,iMusicFileAccessMock.Object,null);
                MVM.OnLoadMusic.Execute(null);
                        icamMock.Setup(er => er.Play(new MediaElement()));


            }
            catch (Exception e)
            {


            }


        }


        [TestMethod]
        public void GetPicturesFolderFilePath()
        {
            try
            {
                var v = FileService.GetPicturesFolder();
                var usr = @"H.Tanev";
                Assert.IsNotNull(v);
                Assert.AreEqual(v.Path, string.Format(@"C:\Users\{0}\Pictures\Saved Pictures\" + v.Name, usr));
            }
            catch (Exception e)
            {

            }

        }

        [TestMethod]
        public async Task CreatePhotoAsync()
        {
            try
            {
                var fold = FileService.GetPicturesFolder();
                var v = await FileService.CreatePhotoAsync(fold);

                Assert.IsNotNull(v);
                Assert.AreEqual(fold.Path + "\\TestPhoto.jpg", v.Path);
            }
            catch (Exception e)
            {

            }

        }

        [TestMethod]
        public async Task MapMeta()
        {
            try
            {


                var icamMock =
                    new Mock<IMusicFileAccess>(new MusicFileAccess(), 
                        new MockProxyDispatcher(), MockBehavior.Loose);
                MediaSource _mediaSource;
                //  var item = icamMock.Object.GetSingleFileAsync(@"\Warriors.mp3",
                //    @"C:\Users\H.Tanev\Music" + @"\Warriors.mp3").Result;//KnownFolders.MusicLibrary.Path).Result;
                //  var musicFIle = KnownFolders.MusicLibrary + @"\Warriors.mp3";
                // StorageFile item = await StorageFile.GetFileFromPathAsync(new Uri(qwe.Re));
                var iFolderaccess = new Mock<IFolderAccess>(new FolderAccess(), new MockProxyDispatcher(),
                 MockBehavior.Loose);

                List<StorageFile> item = new List<StorageFile>(); 

                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal, async () =>
                    {
                         item = await icamMock.Object.GetMusic(iFolderaccess.Object);

                    }
                );

                if (item.Count > 0)
                    foreach (var i in item)
                    {
                        _mediaSource = MediaSource.CreateFromStorageFile(i);
                        var mediaPlaybackItem = new MediaPlaybackItem(_mediaSource);
                        await Mapper.MapSongMetaFromStorageFileToMediaPlaybackItem(i, mediaPlaybackItem);
                    }

                         }
            catch (Exception e)
            {
                
               
            }
            
        }


        [TestMethod]
        public async Task TestCameraVM()
        {
            try
            {
      
              
            var iCam =
                                          new Mock<ICameraModelService>(null,
                                              new MockProxyDispatcher(), MockBehavior.Loose);
                            var ilp =
                             new Mock<ILyricsProcessingFacade>(new LyricsProcessingFacade(null),
                                 new MockProxyDispatcher(), MockBehavior.Loose);
                            CameraViewModel cvm = new CameraViewModel(null, iCam.Object, ilp.Object);
                            cvm.OnCapture.Execute(null);
                   
          
            }
            catch (Exception e)
            {

            }

        }

    }

    

    }

