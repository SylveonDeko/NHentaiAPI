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
        public async Task TestSearchHomePageResult()
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
    }
}
