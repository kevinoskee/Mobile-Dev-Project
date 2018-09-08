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
    public class PersonModel
    {
        public string Name { get; set; }
        public string Number { get; set; }
    }
    public class MessageModel
    {
        public string Message { get; set; }
    }

}
