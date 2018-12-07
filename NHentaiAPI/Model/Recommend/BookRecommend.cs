using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHentaiAPI.Model.Recommend
{
    public class BookRecommend
    {
        [JsonProperty("result")]
        public List<Search.Book> Result { get; set; }
    }
}
