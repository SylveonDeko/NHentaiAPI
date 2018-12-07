using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHentaiAPI.Model.Book
{
    public class Thumbnail
    {
        [JsonProperty("t")]
        public string Type { get; set; }

        [JsonProperty("w")]
        public int Width { get; set; }

        [JsonProperty("h")]
        public int Height { get; set; }
    }
}
