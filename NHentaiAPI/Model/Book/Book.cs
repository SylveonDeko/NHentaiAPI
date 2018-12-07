using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NHentaiAPI.Model.Book;

namespace NHentaiAPI.Model.Search
{
    public class Book
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("media_id")]
        public string MediaId { get; set; }

        [JsonProperty("title")]
        public Title Title { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("scanlator")]
        public string Scanlator { get; set; }

        [JsonProperty("upload_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime UploadDate { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("num_pages")]
        public int NumPages { get; set; }

        [JsonProperty("num_favorites")]
        public int NumFavorites { get; set; }
    }
}
