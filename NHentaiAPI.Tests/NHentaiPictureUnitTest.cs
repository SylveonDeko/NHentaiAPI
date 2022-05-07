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
            // Get book no 123
            var book = await NHentaiClient.GetBookAsync(123);

            // Check this book is right media number
            Assert.AreEqual(635, book.MediaId);
            
            // Check url
            var imageUrl = NHentaiClient.GetPictureUrl(book, 1);
            Assert.AreEqual(imageUrl, "https://i.nhentai.net/galleries/635/1.jpg");
            
            // Make sure image is downloaded
            var result = await NHentaiClient.GetPictureAsync(book, 1);
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
            // Get book no 288869
            var book = await NHentaiClient.GetBookAsync(288869);

            // Check this book is right media number
            Assert.AreEqual(1504878, book.MediaId);
            
            // Check url
            var imageUrl = NHentaiClient.GetPictureUrl(book, 22);
            Assert.AreEqual(imageUrl.AbsoluteUri, "https://i.nhentai.net/galleries/1504878/22.gif");
            
            // Make sure image is downloaded
            var result = await NHentaiClient.GetPictureAsync(book, 22);
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
            // Get book no 123
            var book = await NHentaiClient.GetBookAsync(123);

            // Check this book is right media number
            Assert.AreEqual(635, book.MediaId);

            // Check url
            var imageUrl = NHentaiClient.GetThumbPictureUrl(book, 1);
            Assert.AreEqual(imageUrl, "https://t.nhentai.net/galleries/635/1t.jpg");
            
            // Make sure image is downloaded
            var result = await NHentaiClient.GetThumbPictureAsync(book, 1);
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
            // Get book no 123
            var book = await NHentaiClient.GetBookAsync(123);

            // Check this book is right media number
            Assert.AreEqual(635, book.MediaId);
            
            // Check url
            var imageUrl = NHentaiClient.GetBigCoverUrl(book);
            Assert.AreEqual(imageUrl, "https://t.nhentai.net/galleries/635/cover.jpg");

            // Make sure image is downloaded
            var result = await NHentaiClient.GetBigCoverPictureAsync(book);
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
            // Get book no 123
            var book = await NHentaiClient.GetBookAsync(123);

            // Check this book is right media number
            Assert.AreEqual(635, book.MediaId);
            
            // Check url
            var imageUrl = NHentaiClient.GetOriginPictureUrl(book, 1);
            Assert.AreEqual(imageUrl, "https://i.nhentai.net/galleries/635/1.jpg");

            // Make sure image is downloaded
            var result = await NHentaiClient.GetOriginPictureAsync(book,1);
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
            // Get book no 123
            var book = await NHentaiClient.GetBookAsync(123);

            // Check this book is right media number
            Assert.AreEqual(635, book.MediaId);

            // Check url
            var imageUrl = NHentaiClient.GetBookThumbUrl(book);
            Assert.AreEqual(imageUrl, "https://t.nhentai.net/galleries/635/thumb.jpg");

            // Make sure image is downloaded
            var result = await NHentaiClient.GetBookThumbPictureAsync(book);
            Assert.AreEqual(true, result.Length > 0);
        }
    }
}
