using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHentaiAPI.Model.Search
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
