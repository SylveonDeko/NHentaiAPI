using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace NHentaiAPI.Model.Book
{
    public class Image
    {
        [JsonProperty("t")]
        public ImageType Type { get; set; }

        [JsonProperty("w")]
        public int Width { get; set; }

        [JsonProperty("h")]
        public int Height { get; set; }
    }

    public enum ImageType
    {
        [EnumMember(Value = "j")]
        Jpg,

        [EnumMember(Value = "p")]
        Png,
    }
}
