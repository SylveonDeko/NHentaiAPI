using System;
using System.Collections.Generic;
using System.Text;

namespace NHentaiAPI.Model
{
    public class Tag
    {
        public int Id{ get; set;}

        public string Type{ get; set;}

        public string Name{ get; set;}

        public string Url { get; set;}

        public int Count { get; set;}

        public bool IsFavourite { get; set;}
    }
}
