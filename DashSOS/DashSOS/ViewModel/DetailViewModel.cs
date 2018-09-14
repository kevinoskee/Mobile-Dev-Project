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
namespace DashSOS.ViewModel
{
    public class DetailViewModel
    {
        public Action<string> ChangeButton;
        public bool configStat;
        public ICommand Configure { protected set; get; }
        public ICommand Save { protected set; get; }
        public ICommand Cancel { protected set; get; }
        public ICommand EmergencyTap { protected set; get; }


        public DetailViewModel()
        {
            Configure = new Command(OnConfigure);
            Save = new Command(OnSave);
            Cancel = new Command(OnCancel);
            EmergencyTap= new Command(OnEmergencyTap);
        }

        public void OnConfigure()
        {
            ChangeButton("configure");
            configStat = true;
        }
        public void OnSave()
        {
            ChangeButton("save");
            configStat = false;
        }
        public void OnCancel()
        {
            ChangeButton("cancel");
            configStat = false;
        }
        public void OnEmergencyTap(object emergency)
        {
            if(configStat == true)
            {
              SetModel(emergency);
              PopupNavigation.Instance.PushAsync(new ConfigureView());  
            }
            else
            {
                GetModel(emergency);
            }
        }
        public void SetModel(object emergency)
        {
            switch (emergency)
            {
                case "doctor":
                    //insert database process

                    //temporary
                    EmergencyModel.ImageSource = "doctor.png";
                    EmergencyModel.EmergencyName = "DOCTOR";
                    EmergencyModel.ContactName = "Asian Hospital";
                    EmergencyModel.ContactNumber = "09478227779";
                    EmergencyModel.MessageTemplate = "I need medical emergency";
                    break;
            }
        }
        public void GetModel(object emergency)
        {
            switch (emergency)
            {
                case "doctor":
                    //insert database process

                    //temporary
                    EmergencyModel.ContactName = "Asian Hospital";
                    EmergencyModel.ContactNumber = "09478227779";
                    EmergencyModel.MessageTemplate = "I need medical emergency";
                    DependencyService.Get<ISendSMS>().Send(EmergencyModel.ContactNumber, EmergencyModel.MessageTemplate);                 
                    break;
            }
        }
    }
}
