using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using DashSOS.View;
using DashSOS.Model;
using System.Linq;
using Xamarin.Forms.Xaml;
using DashSOS.ViewModel;
using DashSOS.Database;
using System.IO;

namespace DashSOS.ViewModel
{
    public class HomeViewModel
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"DashSOS.db3");
        public Action<string> ChangeButton;
        public bool configStat;
        public ICommand SetUp { protected set; get; }
        public ICommand Done { protected set; get; }
        public ICommand Return { protected set; get; }
        public ICommand EmergencyTap { protected set; get; }


        public HomeViewModel()
        {
            SetUp = new Command(OnConfigure);
            Done = new Command(OnDone);
            Return = new Command(OnReturn);
            EmergencyTap= new Command(OnEmergencyTap);
        }

        public void OnConfigure()
        {
            ChangeButton("setup");
            configStat = true;
        }
        public void OnDone()
        {
            ChangeButton("done");
            configStat = false;
        }
        public void OnReturn()
        {
            ChangeButton("return");
            configStat = false;
        }
        public async void OnEmergencyTap(object emergency)
        {
            if(configStat == true)
            {
                await App.NavigateMasterDetail(new SetUpView(emergency.ToString()));
                //await App.NavigateMasterDetail(new SetUpView(emergency.ToString()));
            }
            else
            {
                //GetModel(emergency);
                // TestMessage();
                TestToast(emergency.ToString());
            }
        }
        public void GetModel(object emergency)
        {
        //    switch (emergency)
        //    {
        //        //case "medical":
        //        //    //insert database process

        //        //    //temporary
        //        //    EmergencyModel.ContactName = "Asian Hospital";
        //        //    EmergencyModel.ContactNumber = "09478227779";
        //        //    EmergencyModel.MessageTemplate = "I need medical emergency";
        //        //    DependencyService.Get<ISendSMS>().Send(EmergencyModel.ContactNumber, EmergencyModel.MessageTemplate);                 
        //        //    break;
        //    }
        }
        public void TestMessage()
        {
            DependencyService.Get<IToast>().Test();
        }
        public async void TestToast(string emergency)
        {
            string[] emergencies = { "Police", "Medical", "Fire", "Family" };
            foreach (string value in emergencies)
            {
                if(value == emergency)
                {

                    ContactDatabase db = new ContactDatabase(dbPath);
                    var _db = await db.GetContactAsync(value);
                    if (_db != null)
                    {
                        DependencyService.Get<IToast>().Toasts("hasData", "success");
                    }
                    else
                    {
                        DependencyService.Get<IToast>().Toasts("hasData", "failed");
                    }
                }
                
            }
        }
    }
}
