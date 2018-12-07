using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHentaiAPI.Tests
{
    [TestClass]
    public class NHentaiPictureUnitTest
    {
        [TestMethod]
        public async Task TestGetPictureResult()
        {
            //generate client
            var client = new NHentaiClient();

            //get book no 123
            var book = await client.GetBookAsync(123);

            //Check this book is 
            Assert.AreEqual(635, book.MediaId);

            //https://i.nhentai.net/galleries/635/1.jpg
            var result = await client.GetPictureAsync(book, 1);

            //make sure downloaded image
            Assert.AreEqual(true, result.Length>0);
        }

        [TestMethod]
        public async Task TestGetThumbPictureResult()
        {
            //generate client
            var client = new NHentaiClient();

            //get book no 123
            var book = await client.GetBookAsync(123);

            //Check this book is 
            Assert.AreEqual(635, book.MediaId);

            //https://t.nhentai.net/galleries/635/1t.jpg
            var result = await client.GetThumbPictureAsync(book, 1);

            //make sure downloaded image
            Assert.AreEqual(true, result.Length > 0);
        }

        [TestMethod]
        public async Task TestGetBigCoverPictureResult()
        {
            //generate client
            var client = new NHentaiClient();

            //get book no 123
            var book = await client.GetBookAsync(123);

            //Check this book is 
            Assert.AreEqual(635, book.MediaId);

            //https://i.nhentai.net/galleries/635/1.jpg
            var result = await client.GetBigCoverPictureAsync(book);

            //make sure downloaded image
            Assert.AreEqual(true, result.Length > 0);
        }

        [TestMethod]
        public async Task TestGetOriginPictureResult()
        {
            //generate client
            var client = new NHentaiClient();

            //get book no 123
            var book = await client.GetBookAsync(123);

            //Check this book is 
            Assert.AreEqual(635, book.MediaId);

            //https://i.nhentai.net/galleries/635/1.jpg
            var result = await client.GetOriginPictureAsync(book,1);

            //make sure downloaded image
            Assert.AreEqual(true, result.Length > 0);
        }

        [TestMethod]
        public async Task TestBookThumbPictureResult()
        {
            //generate client
            var client = new NHentaiClient();

            //get book no 123
            var book = await client.GetBookAsync(123);

            //Check this book is 
            Assert.AreEqual(635, book.MediaId);

            //https://t.nhentai.net/galleries/635/thumb.jpg
            var result = await client.GetBookThumbPictureAsync(book);

            //make sure downloaded image
            Assert.AreEqual(true, result.Length > 0);
        }
    }
}
