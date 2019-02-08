using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NHentaiAPI.Models.Books;
using NHentaiAPI.Models.Recommends;
using NHentaiAPI.Models.Searches;

namespace NHentaiAPI.Tests
{
    public class BaseUnitTest
    {
        protected NHentaiClient CreateNHentaiClient()
        {
            return new TestNHentaiClient();
        }

        private class TestNHentaiClient : NHentaiClient
        {
            #region Urls

            protected override string ApiRootUrl => "https://nhent.ai";

            #endregion

            #region Search

            public override async Task<SearchResults> GetHomePageListAsync(int pageNum)
            {
                var url = GetHomePageUrl(pageNum);
                var books = await GetData<List<Book>>(url);
                return new SearchResults
                {
                    Result = books,
                    PerPage = books.Count
                };
            }

            public override async Task<SearchResults> GetSearchPageListAsync(string keyword, int pageNum)
            {
                var url = GetSearchUrl(keyword, pageNum);
                var books = await GetData<List<Book>>(url);
                return new SearchResults
                {
                    Result = books,
                    PerPage = books.Count
                };
            }

            public override async Task<SearchResults> GetTagPageListAsync(Tag tag, SortBy sortBy, int pageNum)
            {
                var url = GetTagUrl(tag, sortBy == SortBy.Popular, pageNum);
                var books = await GetData<List<Book>>(url);
                return new SearchResults
                {
                    Result = books,
                    PerPage = books.Count
                };
            }

            #endregion

            #region Books

            public override async Task<BookRecommend> GetBookRecommendAsync(int bookId)
            {
                var url = GetBookRecommendUrl(bookId);
                var book = await GetData<Book>(url);
                return new BookRecommend
                {
                    Result = new List<Book> { book }
                };
            }

            #endregion
        }
    }
}