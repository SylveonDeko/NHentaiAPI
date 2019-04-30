using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHentaiAPI.Tests
{
    /// <summary>
    /// see :
    /// https://github.com/andy840119/NHentaiSharp/blob/master/NHentaiSharp.UnitTests/Program.cs
    /// </summary>
    [TestClass]
    public class NHentaiBookUnitTest : BaseUnitTest
    {
        /// <summary>
        /// Get book detail
        /// https://nhentai.net/g/161194/
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestBookResult()
        {
            //https://nhentai.net/api/gallery/161194
            var result = await NHentaiClient.GetBookAsync(161194);

            Assert.AreEqual("[ユイザキカズヤ] つなかん。 (COMIC ポプリクラブ 2013年8月号) [英訳]", result.Title.Japanese);
            Assert.AreEqual("Tsuna-kan. | Tuna Can", result.Title.Pretty);
            Assert.AreEqual("[Yuizaki Kazuya] Tsuna-kan. | Tuna Can (COMIC Potpourri Club 2013-08) [English] [PSYN]", result.Title.English);
            Assert.AreEqual("160413", result.UploadDate.ToString("yyMMdd"));
            Assert.AreEqual(true, result.Tags.Any(x => x.Id == 19440));
            Assert.AreEqual(17, result.NumPages);
            Assert.AreEqual(17, result.Images.Pages.Count);
            Assert.AreEqual(161194, result.Id);
        }

        /// <summary>
        /// Get recommend book
        /// https://nhentai.net/g/161194/
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestBookRecommendResult()
        {
            //https://nhentai.net/api/gallery/161194/related
            var result = await NHentaiClient.GetBookRecommendAsync(161194);

            //as least one recommend
            Assert.AreEqual(true, result.Result.Any());
        }
    }
}
