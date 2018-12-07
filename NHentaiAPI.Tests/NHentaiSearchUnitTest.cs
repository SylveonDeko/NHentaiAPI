using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHentaiAPI.Model.Book;

namespace NHentaiAPI.Tests
{
    /// <summary>
    /// see :
    /// https://github.com/NHMoeDev/NHentai-android/issues/27
    /// </summary>
    [TestClass]
    public class NHentaiSearchUnitTest
    {
        [TestMethod]
        public async Task TestSearchHomePageResult()
        {
            //generate client
            var client = new NHentaiClient();

            //https://nhentai.net/api/galleries/all?page=1
            var result = await client.GetHomePageListAsync(1);

            Assert.AreEqual(25, result.PerPage);
            Assert.AreEqual(25, result.Result.Count);
        }

        [TestMethod]
        public async Task TestSearchResult()
        {
            //generate client
            var client = new NHentaiClient();

            //https://nhentai.net/api/galleries/search?query=school%20swimsuit%20loli%20full%20color&page=2
            var result = await client.GetSearchPageListAsync("school swimsuit loli full color",2);

            Assert.AreEqual(25, result.PerPage);
            Assert.AreEqual(25, result.Result.Count);
        }

        [TestMethod]
        public async Task TestTagResult()
        {
            //generate client
            var client = new NHentaiClient();

            //https://nhentai.net/api/galleries/tagged?tag_id=1&page=1&sort=popular
            var tag = new Tag
            {
                Id = 1
            };
            var result = await client.GetTagPageListAsync(tag,true,1);

            Assert.AreEqual(25, result.PerPage);
            Assert.AreEqual(true, result.Result.Any());
        }
    }
}
