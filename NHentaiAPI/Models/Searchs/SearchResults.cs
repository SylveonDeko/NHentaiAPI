using System.Collections.Generic;
using Newtonsoft.Json;
using NHentaiAPI.Models.Books;

namespace NHentaiAPI.Models.Searchs
{
    public class SearchResults
    {
        [JsonProperty("result")]
        public List<Book> Result { get; set; }

        [JsonProperty("num_pages")]
        public int NumPages { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }
    }
}
