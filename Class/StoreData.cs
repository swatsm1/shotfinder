using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShotFinderMVC.Class
{
    public class StoreData
    {
        public class SpecialHours
        {
            [JsonProperty("2021-03-08")]
            public string _20210308 { get; set; }

            [JsonProperty("2021-03-09")]
            public string _20210309 { get; set; }
        }

        public class PickupDateAndTimes
        {
            public List<string> regularHours { get; set; }
            public SpecialHours specialHours { get; set; }
            public string defaultTime { get; set; }
            public string earliest { get; set; }
        }

        public class Store
        {
            public int storeNumber { get; set; }
            public string address { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string zipcode { get; set; }
            public string timeZone { get; set; }
            public string fullZipCode { get; set; }
            public string fullPhone { get; set; }
            public string locationDescription { get; set; }
            public string storeHoursMonday { get; set; }
            public string storeHoursTuesday { get; set; }
            public string storeHoursWednesday { get; set; }
            public string storeHoursThursday { get; set; }
            public string storeHoursFriday { get; set; }
            public string storeHoursSaturday { get; set; }
            public string storeHoursSunday { get; set; }
            public string rxHrsMon { get; set; }
            public string rxHrsTue { get; set; }
            public string rxHrsWed { get; set; }
            public string rxHrsThu { get; set; }
            public string rxHrsFri { get; set; }
            public string rxHrsSat { get; set; }
            public string rxHrsSun { get; set; }
            public string storeType { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string name { get; set; }
            public double milesFromCenter { get; set; }
            public List<string> specialServiceKeys { get; set; }
            public object @event { get; set; }
            public List<object> holidayHours { get; set; }
            public PickupDateAndTimes pickupDateAndTimes { get; set; }
        }

        public class NorthEastElements
        {
            public int altitude { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class SouthWestElements
        {
            public int altitude { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class GeocodeBestView
        {
            public NorthEastElements northEastElements { get; set; }
            public SouthWestElements southWestElements { get; set; }
        }

        public class ResolvedAddress
        {
            public object addressLine { get; set; }
            public string adminDistrict { get; set; }
            public int altitude { get; set; }
            public string confidence { get; set; }
            public string calculationMethod { get; set; }
            public string countryRegion { get; set; }
            public string displayName { get; set; }
            public string district { get; set; }
            public string formattedAddress { get; set; }
            public GeocodeBestView geocodeBestView { get; set; }
            public double latitude { get; set; }
            public string locality { get; set; }
            public double longitude { get; set; }
            public string postalCode { get; set; }
            public string postalTown { get; set; }
        }

        public class Data
        {
            public List<Store> stores { get; set; }
            public object globalZipCode { get; set; }
            public ResolvedAddress resolvedAddress { get; set; }
            public object ambiguousAddresses { get; set; }
            public List<object> warnings { get; set; }
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
}
