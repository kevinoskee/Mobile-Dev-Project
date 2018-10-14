﻿using System;
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
using Xamarin.Forms.Maps;

//TEST HELLO WORLD GITHUB CHANGE CHECK -JM


[assembly: Dependency(typeof(DashSOS.Droid.SendSMS))]
namespace DashSOS.Droid
{
    public class SendSMS : ISendSMS,IGetLocation
    {
        public LocationModel locationModel = new LocationModel { };
        Geocoder geoCode;
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
            //locationModel.Location = "Location : Longitude - " + position.Longitude.ToString() + ",\n\tLatitude - " + position.Latitude.ToString();


            var reversePosition = new Position(position.Latitude, position.Longitude);
            var possibleAddresses = await geoCode.GetAddressesForPositionAsync(reversePosition);
            foreach (var address in possibleAddresses)
                locationModel.Location += address + "\n";
                //reverseGeocodedOutputLabel.Text += address + "\n";

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
        public async void Test()
        {
            Toast.MakeText(Forms.Context, "Getting Location", ToastLength.Short).Show();
            string strLocation="";
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20));
            locationModel.Location = "Location : Longitude - " + position.Longitude.ToString() + ",\n\tLatitude - " + position.Latitude.ToString();
            Console.WriteLine(locationModel.Location);

            Console.WriteLine("CHECKPOINT 1 : " + position.Longitude.ToString());
            var reversePosition = new Position(40.714224, -73.961452);
            //var reversePosition = new Position(position.Latitude, position.Longitude);
            Console.WriteLine("CHKPT2 : " + position.Latitude + " " + reversePosition.Latitude);
            try
            {
                var possibleAddresses = (await geoCode.GetAddressesForPositionAsync(reversePosition)).FirstOrDefault();

                //foreach (var address in possibleAddresses)
                Console.WriteLine(strLocation += possibleAddresses + "\n");
            }
            catch(Exception e)
            {
                Console.WriteLine("CANT FIND REVERSE at " + reversePosition.Latitude + ", " + reversePosition.Longitude);
            }
            /*
            var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(reversePosition);
            Console.WriteLine("CHKPT3");
            foreach (var address in possibleAddresses)
                strLocation += address + "\n";
            */


            Toast.MakeText(Forms.Context, strLocation, ToastLength.Short).Show();
        }
    }
}