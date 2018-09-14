using System;
using System.Collections.Generic;
using System.Text;
using DashSOS.ViewModel;

namespace DashSOS.Model
{
    public class LocationModel
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Location { get; set; }
    }
    public class EmergencyModel
    {
        public static string ImageSource { get; set; }
        public static string EmergencyName { get; set; }
        public static string ContactName { get; set; }
        public static string ContactNumber { get; set; }
        public static string MessageTemplate { get; set; }
    }
   

}
