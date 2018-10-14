using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Plugin.Permissions;
using Plugin.Geolocator;
using Android.Locations;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using Plugin.CurrentActivity;
namespace DashSOS.Droid
{
    [Activity(Label = "DashSOS", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            
            Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            //Xamarin.Essentials.Platform.Init(this, bundle);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;

            LoadApplication(new App());

            LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);

            

            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
              //  ShowGPSDisabledAlertToUser();
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void ShowGPSDisabledAlertToUser()
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("LOCATION SERVICE");
            alert.SetMessage("GPS is disabled in your device. Would you like to enable it?");
            alert.SetButton("Sure", (c, ev) =>
            {
                Intent i = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                StartActivity(i);
            });
            alert.SetButton2("No", (c, ev) =>
            {
                var activity = (Activity)Forms.Context;
                activity.FinishAffinity();
            });
            alert.Show();
        }
    }
}

