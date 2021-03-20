using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShotFinderMVC.Class
{
    public class Slots
    {
        [JsonProperty("1")]
        public bool _1 { get; set; }

        [JsonProperty("2")]

        public bool _2 { get; set; }
    }

    public class Data
    {
        public Slots slots { get; set; }
    }

    public class Root
    {
        public Data Data { get; set; }
        public string Status { get; set; }
        public object ErrCde { get; set; }
        public object ErrMsg { get; set; }
        public object ErrMsgDtl { get; set; }
    }


}
