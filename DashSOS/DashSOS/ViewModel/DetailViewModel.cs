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
namespace DashSOS.ViewModel
{
    public class DetailViewModel
    {
        public Action<string> ChangeButton;
        public bool configStat;
        public ICommand Configure { protected set; get; }
        public ICommand Done { protected set; get; }
        public ICommand Return { protected set; get; }
        public ICommand EmergencyTap { protected set; get; }


        public DetailViewModel()
        {
            Configure = new Command(OnConfigure);
            Done = new Command(OnDone);
            Return = new Command(OnReturn);
            EmergencyTap= new Command(OnEmergencyTap);
        }

        public void OnConfigure()
        {
            ChangeButton("configure");
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
        public void OnEmergencyTap(object emergency)
        {
            if(configStat == true)
            {
              PopupNavigation.Instance.PushAsync(new ConfigureView(emergency.ToString()));  
            }
            else
            {
                GetModel(emergency);
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
    }
}
