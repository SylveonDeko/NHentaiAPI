using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHentaiAPI.Models.Books;
using NHentaiAPI.Models.Searches;

namespace NHentaiAPI.Tests;

/// <summary>
///     see :
///     https://github.com/NHMoeDev/NHentai-android/issues/27
/// </summary>
[TestClass]
public class NHentaiSearchUnitTest : BaseUnitTest
{
    /// <summary>
    ///     Target number of record in single page
    /// </summary>
    protected virtual int ResultNumber => 25;

    /// <summary>
    ///     Get home page search result
    ///     https://nhentai.net/galleries/all?page=1
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task TestSearchHomePageResult()
    {
        // https://nhentai.net/api/galleries/all?page=1
        var result = await NHentaiClient.GetHomePageListAsync(1);

        Assert.AreEqual(ResultNumber, result.PerPage);
        Assert.AreEqual(ResultNumber, result.Result.Count);
    }

    /// <summary>
    ///     Get search result by keyword
    ///     https://nhentai.net/galleries/search?query=school%20swimsuit%20loli%20full%20color&page=2
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task TestSearchResult()
    {
        // https://nhentai.net/api/galleries/search?query=school
        var result = await NHentaiClient.GetSearchPageListAsync("school", 1);

        Assert.AreEqual(ResultNumber, result.PerPage);
        Assert.AreEqual(ResultNumber, result.Result.Count);
    }

    /// <summary>
    ///     Get search result by tag, can be sort by popular
    ///     https://nhentai.net/galleries/tagged?tag_id=1&page=1&sort=popular
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task TestTagResult()
    {
        // https://nhentai.net/api/galleries/tagged?tag_id=1&page=1&sort=popular
        var tag = new Tag
        {
            Id = 1
        };
        var result = await NHentaiClient.GetTagPageListAsync(tag, SortBy.Popular, 1);

        Assert.AreEqual(ResultNumber, result.PerPage);
        Assert.AreEqual(true, result.Result.Any());
    }
}