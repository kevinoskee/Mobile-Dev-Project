using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Android.Telephony;
using Android.Util;
using Plugin.Geolocator;
using System.Threading.Tasks;
using DashSOS.Model;

[assembly: Dependency(typeof(DashSOS.Droid.SendSMS))]
namespace DashSOS.Droid
{
    public class SendSMS : ISendSMS,IGetLocation
    {
        public LocationModel locationModel = new LocationModel { };
        public void Message(string number,string message)
        {
        //    Toast.MakeText(Forms.Context, locationModel.Location, ToastLength.Short).Show();
            SmsManager.Default.SendTextMessage(number, null, message, null, null);
        }
        public async Task Location()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            locationModel.Location = "Location : Longitude - " + position.Longitude.ToString() + ",\n\tLatitude - " + position.Latitude.ToString();
      //      Toast.MakeText(Forms.Context, locationModel.Location, ToastLength.Short).Show();
        }
        public async void Send(string number,string message) //temporary
        {
            //string currLocation = "";
            //var locator = CrossGeolocator.Current;
            //locator.DesiredAccuracy = 50;
            //var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            //currLocation = "Location : " + position.Longitude.ToString() + "," + position.Latitude.ToString();
            SmsManager.Default.SendTextMessage(number, null, message, null, null);
        }
    }
}