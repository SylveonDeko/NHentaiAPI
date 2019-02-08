using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NHentaiAPI.Models.Books;
using NHentaiAPI.Models.Searchs;

namespace NHentaiAPI.Tests
{
    public class BaseUnitTest
    {
        protected NHentaiClient CreateNHentaiClient()
        {
            return new TestNHentaiClient();
        }
    }

    public class TestNHentaiClient : NHentaiClient
    {
        protected override string ApiRootUrl => "https://nhent.ai";

        public override async Task<SearchResults> GetHomePageListAsync(int pageNum)
        {
            var url = GetHomePageUrl(pageNum);
            var books = await GetData<List<Book>>(url);
            return new SearchResults
            {
                Result = books,
            };
        }

        public override async Task<SearchResults> GetSearchPageListAsync(string keyword, int pageNum)
        {
            var url = GetSearchUrl(keyword, pageNum);
            var books = await GetData<List<Book>>(url);
            return new SearchResults
            {
                Result = books
            };
        }

        public override async Task<SearchResults> GetTagPageListAsync(Tag tag, SortBy sortBy, int pageNum)
        {
            var url = GetTagUrl(tag, sortBy == SortBy.Popular, pageNum);
            var books = await GetData<List<Book>>(url);
            return new SearchResults
            {
                Result = books
            };
        }
    }
}
