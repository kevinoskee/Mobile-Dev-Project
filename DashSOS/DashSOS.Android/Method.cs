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
using Xamarin.Forms.Maps;
using Android.Views.InputMethods;

[assembly: Dependency(typeof(DashSOS.Droid.Method))]
namespace DashSOS.Droid
{
    public class Method : ISendSMS, IGetLocation, IToast, IHideKeyboard
    {
        Toast toast = Toast.MakeText(Forms.Context, "", ToastLength.Short);
        public LocationModel locationModel = new LocationModel { };
        
        Geocoder geoCoder;
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
            var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(reversePosition);
            foreach (var address in possibleAddresses)
                locationModel.Location += address + "\n";
                //reverseGeocodedOutputLabel.Text += address + "\n";

            //      Toast.MakeText(Forms.Context, locationModel.Location, ToastLength.Short).Show();
        }

        public void Send(string number,string message) //temporary
        {
            //string currLocation = "";
            //var locator = CrossGeolocator.Current;
            //locator.DesiredAccuracy = 50;
            //var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            //currLocation = "Location : " + position.Longitude.ToString() + "," + position.Latitude.ToString();
             SmsManager.Default.SendTextMessage(number, null, message, null, null);
        }
        public void Toasts(string function, string status)
        {
            switch (function)
            {
                case "hasData":
                    if (status == "success")
                    {
                        toast.SetText("Data Available");
                        toast.Show();
                    }
                    else
                    {
                        toast.SetText("No Data Yet. Please setup first.");
                        toast.Show();
                    }
                    break;

                case "hasEntry":
                    if (status == "success")
                    {
                        toast.SetText("Still has");
                        toast.Show();
                    }
                    else
                    {
                        toast.SetText("No More");
                        toast.Show();
                    }
                    break;
                case "addContact":
                    if (status == "success")
                    {
                        toast.SetText("Contact Added");
                        toast.Show();
                    }
                    else
                    {
                        toast.SetText("Please enter data");
                        toast.Show();
                    }
                    break;
                case "deleteContact":
                    if (status == "success")
                    {
                        toast.SetText("Contact Deleteed");
                        toast.Show();
                    }
                    else
                    {
                        toast.SetText("Something wrong");
                        toast.Show();
                    }
                    break;
                case "emergency":
                    toast.SetText(status);
                    toast.Show();
                    break;
                case "custom":
                    toast.SetText(status);
                    toast.Show();
                    break;
            }
        }
        public async void Test()
        {
            Toast.MakeText(Forms.Context, "Getting Location", ToastLength.Short).Show();
            string strLocation="";
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20));
            //locationModel.Location = "Location : Longitude - " + position.Longitude.ToString() + ",\n\tLatitude - " + position.Latitude.ToString();

            var reversePosition = new Position(position.Latitude, position.Longitude);
            var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(reversePosition);
            foreach (var address in possibleAddresses)
                strLocation += address + "\n";

            Toast.MakeText(Forms.Context, strLocation, ToastLength.Short).Show();
        }
        public void HideKeyboard()
        {
            var context = Forms.Context;
            var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null && context is Activity)
            {
                var activity = context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                activity.Window.DecorView.ClearFocus();
            }
        }

    }
}