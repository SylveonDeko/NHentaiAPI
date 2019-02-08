using System.Collections.Generic;
using Newtonsoft.Json;
using NHentaiAPI.Models.Books;

namespace NHentaiAPI.Models.Recommends
{
    public class BookRecommend
    {
        [JsonProperty("result")]
        public List<Book> Result { get; set; }
    }
}
