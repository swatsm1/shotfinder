using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShotFinder.Class
{
    public class CVSStoresStateFL
    {
        public class Rootobject
        {
            public Responsepayloaddata responsePayloadData { get; set; }
            public Responsemetadata responseMetaData { get; set; }
        }

        public class Responsepayloaddata
        {
            public DateTime currentTime { get; set; }
            public Data data { get; set; }
            public bool isBookingCompleted { get; set; }
        }

        public class Data
        {
            public FL[] FL { get; set; }
        }

        public class FL
        {
            public string city { get; set; }
            public string state { get; set; }
            public string status { get; set; }
        }

        public class Responsemetadata
        {
            public string statusDesc { get; set; }
            public string conversationId { get; set; }
            public string refId { get; set; }
            public string operation { get; set; }
            public string version { get; set; }
            public string statusCode { get; set; }
        }


    }
}