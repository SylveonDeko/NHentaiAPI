using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHentaiAPI.Model
{
    public class Tag
    {
        [JsonProperty("id")]
        public int Id{ get; set;}

        [JsonProperty("type")]
        public string Type{ get; set;}

        [JsonProperty("name")]
        public string Name{ get; set;}

        [JsonProperty("url")]
        public string Url { get; set;}

        [JsonProperty("count")]
        public int Count { get; set;}

        [JsonProperty("id")]
        public bool IsFavourite { get; set;}
    }
}
