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
using Android.Util;
using Plugin.Geolocator;
using System.Threading.Tasks;
using DashSOS.Model;
using Xamarin.Forms.Maps;
using Android.Views.InputMethods;
using Android.Telephony;
using Xamarin.Android;
using Plugin.LocalNotifications;
using System.ServiceProcess;
[assembly: Dependency(typeof(DashSOS.Droid.Method))]
namespace DashSOS.Droid
{
    [Activity(Label = "Method", ParentActivity = typeof(MainActivity))]
    public class Method : IGetLocation, IToast, IHideKeyboard, ISendSMS
    {
        int powercount = 0;
        internal string _randomNumber;

        Toast toast = Toast.MakeText(Forms.Context, "", ToastLength.Short);
      
        public LocationModel locationModel = new LocationModel { };
        
        Geocoder geoCoder;
        public void Message(string number,string message)
        {
        //    Toast.MakeText(Forms.Context, locationModel.Location, ToastLength.Short).Show();

        }

        public async Task Location()
        {
           
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(5));
                //locationModel.Location = "Location : Longitude - " + position.Longitude.ToString() + ",\n\tLatitude - " + position.Latitude.ToString();


                var reversePosition = new Position(position.Latitude, position.Longitude);
                var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(reversePosition);
                foreach (var address in possibleAddresses)
                    locationModel.Location += address + "\n";
                //reverseGeocodedOutputLabel.Text += address + "\n";
                Toast.MakeText(Forms.Context, locationModel.Location, ToastLength.Short).Show();
            }
            catch(Exception e)
            {
                Toast.MakeText(Forms.Context, e.ToString(), ToastLength.Short).Show();
            }
       
        }

        public void Send(string number, string message) //temporary
        {

            // Get the default instance of SmsManager
            try
            {
                SmsManager.Default.SendTextMessage(number, null, message, null, null);
            }
            catch(Exception e)
            {
                Toasts("permission", "Send SMS");
            }
           
            // For when the SMS has been sent

        
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
                        toast.SetText("Contact Deleted");
                        toast.Show();
                    }
                    else
                    {
                        toast.SetText("Something wrong");
                        toast.Show();
                    }
                    break;
                case "updateContact":
                    if (status == "success")
                    {
                        toast.SetText("Contact Updated");
                        toast.Show();
                    }
                    else
                    {
                        toast.SetText("Please enter data");
                        toast.Show();
                    }
                    break;
                case "updateMessage":
                    if (status == "success")
                    {
                        toast.SetText("Message Updated");
                        toast.Show();
                    }
                    else
                    {
                        toast.SetText("Default Message Template");
                        toast.Show();
                    }
                    break;
                case "permission":
                    toast.SetText("Grant " + status + " permission in the settings first.");
                    toast.Show();
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
        public void Test()
        { 

            Random random = new Random();
            int randomNumber = random.Next(9999 - 1000) + 1000;
            var localNotification = new LocalNotification
            {
                Title = "DashSOS",
                Body = "Sending emergency",
                Id = 0,
                NotifyTime = DateTime.Now,
                IconId = Resource.Drawable.ic_dialog_close_light
                
            };
            Intent newIntent = new Intent(Android.App.Application.Context, typeof(MainActivity));
             Android.Support.V4.App.TaskStackBuilder stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(Android.App.Application.Context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(newIntent);
            PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);
     
            var builder = new Notification.Builder(Android.App.Application.Context)
                .SetContentTitle(localNotification.Title)
                .SetContentText(localNotification.Body)
                .SetSmallIcon(localNotification.IconId)
                .SetContentIntent(resultPendingIntent)
                .SetAutoCancel(true);
            var notificationManager = NotificationManager.FromContext(Android.App.Application.Context);
            notificationManager.Notify(randomNumber, builder.Build());
            


            //LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.icon;
            //CrossLocalNotifications.Current.Show("title", "body");
            //Toast.MakeText(Forms.Context, "Getting Location", ToastLength.Short).Show();
            //string strLocation = "";
            //var locator =  CrossGeolocator.Current;
            //locator.DesiredAccuracy = 50;
            //var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20));
            //locationModel.Location = "Location : Longitude - " + position.Longitude.ToString() + ",\n\tLatitude - " + position.Latitude.ToString();
            //Toasts("custom", locationModel.Location);

            //Toasts("custom", "CHECKPOINT 1 : " + position.Longitude.ToString());
            //var reversePosition = new Position(40.714224, -73.961452);
            ////var reversePosition = new Position(position.Latitude, position.Longitude);
            //Toasts("custom", "CHKPT2 : " + position.Latitude + " " + reversePosition.Latitude);
            //try
            //{
            //    var possibleAddresses = (await geoCoder.GetAddressesForPositionAsync(reversePosition)).FirstOrDefault();

            //    //foreach (var address in possibleAddresses)
            //    Toasts("custom", strLocation += possibleAddresses + "\n");
            //}
            //catch (Exception e)
            //{
            //    Toasts("custom", "CANT FIND REVERSE at " + reversePosition.Latitude + ", " + reversePosition.Longitude);
            //}
            ///*
            //var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(reversePosition);
            //Console.WriteLine("CHKPT3");
            //foreach (var address in possibleAddresses)
            //    strLocation += address + "\n";
            //*/



        }
       






    }
}