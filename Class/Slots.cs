using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShotFinderMVC.Class
{
    public class Slots
    {
        [JsonProperty("9")]
        public bool _9 { get; set; }

        [JsonProperty("10")]

        public bool _10 { get; set; }

        [JsonProperty("11")]
        public bool _11 { get; set; }

        [JsonProperty("12")]

        public bool _12 { get; set; }

        [JsonProperty("13")]

        public bool _13 { get; set; }
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
