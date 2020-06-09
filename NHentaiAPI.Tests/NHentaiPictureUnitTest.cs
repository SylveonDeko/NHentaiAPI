using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHentaiAPI.Tests
{
    [TestClass]
    public class NHentaiPictureUnitTest : BaseUnitTest
    {
        /// <summary>
        /// Get picture by book's media id and pageNumber
        /// https://nhentai.net/g/123/
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestGetPictureResult()
        {
            //get book no 123
            var book = await NHentaiClient.GetBookAsync(123);

            //Check this book is 
            Assert.AreEqual(635, book.MediaId);

            //https://i.nhentai.net/galleries/635/1.jpg
            var result = await NHentaiClient.GetPictureAsync(book, 1);

            //make sure downloaded image
            Assert.AreEqual(true, result.Length>0);
        }

        /// <summary>
        /// Get picture by book's media id and pageNumber
        /// https://nhentai.net/g/288869 /
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestGetGifPictureResult()
        {
            //get book no 123
            var book = await NHentaiClient.GetBookAsync(288869);

            //Check this book is 
            Assert.AreEqual(1504878, book.MediaId);

            //https://i.nhentai.net/galleries/288869/22.jpg
            var result = await NHentaiClient.GetPictureAsync(book, 22);

            //make sure downloaded image
            Assert.AreEqual(true, result.Length > 0);
        }

        /// <summary>
        /// Get thumbnail by book's media id and pageNumber
        /// https://nhentai.net/g/123/
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestGetThumbPictureResult()
        {
            //get book no 123
            var book = await NHentaiClient.GetBookAsync(123);

            //Check this book is 
            Assert.AreEqual(635, book.MediaId);

            //https://t.nhentai.net/galleries/635/1t.jpg
            var result = await NHentaiClient.GetThumbPictureAsync(book, 1);

            //make sure downloaded image
            Assert.AreEqual(true, result.Length > 0);
        }

        /// <summary>
        /// Get big cover by book's media id
        /// https://nhentai.net/g/123/
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestGetBigCoverPictureResult()
        {
            //get book no 123
            var book = await NHentaiClient.GetBookAsync(123);

            //Check this book is 
            Assert.AreEqual(635, book.MediaId);

            //https://i.nhentai.net/galleries/635/1.jpg
            var result = await NHentaiClient.GetBigCoverPictureAsync(book);

            //make sure downloaded image
            Assert.AreEqual(true, result.Length > 0);
        }

        /// <summary>
        /// Get origin picture by book's media id and pageNumber
        /// https://nhentai.net/g/123/
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestGetOriginPictureResult()
        {
            //get book no 123
            var book = await NHentaiClient.GetBookAsync(123);

            //Check this book is 
            Assert.AreEqual(635, book.MediaId);

            //https://i.nhentai.net/galleries/635/1.jpg
            var result = await NHentaiClient.GetOriginPictureAsync(book,1);

            //make sure downloaded image
            Assert.AreEqual(true, result.Length > 0);
        }

        /// <summary>
        /// Get thumbnail cover by book's media id
        /// https://nhentai.net/g/123/
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestBookThumbPictureResult()
        {
            //get book no 123
            var book = await NHentaiClient.GetBookAsync(123);

            //Check this book is 
            Assert.AreEqual(635, book.MediaId);

            //https://t.nhentai.net/galleries/635/thumb.jpg
            var result = await NHentaiClient.GetBookThumbPictureAsync(book);

            //make sure downloaded image
            Assert.AreEqual(true, result.Length > 0);
        }
    }
}
