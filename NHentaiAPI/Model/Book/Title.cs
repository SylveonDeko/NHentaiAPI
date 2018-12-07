using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHentaiAPI.Model.Book
{
    public class Title
    {
        [JsonProperty("english")]
        public string English { get; set; }

        [JsonProperty("japanese")]
        public string Japanese { get; set; }

        [JsonProperty("pretty")]
        public string Pretty { get; set; }
    }
}
