using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHentaiAPI.Model.Book;
using NHentaiAPI.Model.Search;

namespace NHentaiAPI.Tests
{
    /// <summary>
    /// see :
    /// https://github.com/NHMoeDev/NHentai-android/issues/27
    /// </summary>
    [TestClass]
    public class NHentaiSearchUnitTest
    {
        /// <summary>
        /// Get home page search result
        /// https://nhentai.net/galleries/all?page=1
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get search result by keyword
        /// https://nhentai.net/galleries/search?query=school%20swimsuit%20loli%20full%20color&page=2
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get search result by tag, can be sort by popular
        /// https://nhentai.net/galleries/tagged?tag_id=1&page=1&sort=popular
        /// </summary>
        /// <returns></returns>
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
            var result = await client.GetTagPageListAsync(tag, SortBy.Popular, 1);

            Assert.AreEqual(25, result.PerPage);
            Assert.AreEqual(true, result.Result.Any());
        }
    }
}
