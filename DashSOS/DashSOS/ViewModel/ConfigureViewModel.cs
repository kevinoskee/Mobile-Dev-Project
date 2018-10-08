using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using DashSOS.View;
using DashSOS.Interface;
using DashSOS.Model;
using SQLite;
namespace DashSOS.ViewModel
{
    public class ConfigureViewModel
    {
        private ImageSource emergencyImage;
        public ImageSource EmergencyImage
        {
            get { return emergencyImage; }
            set
            {
                emergencyImage = value;
            }
        }
        private string emergencyName;
        public string EmergencyName
        {
            get { return emergencyName; }
            set
            {
                emergencyName = value;
            }
        }
        private string contactName;
        public string ContactName
        {
            get { return contactName; }
            set
            {
                contactName = value;
            }
        }
        private string contactNumber;
        public string ContactNumber
        {
            get { return contactNumber; }
            set
            {
                contactNumber = value;
            }
        }
        private string messageTemplate;
        public string MessageTemplate
        {
            get { return messageTemplate; }
            set
            {
                messageTemplate = value;
            }
        }
        public ICommand Save { protected set; get; }
        public Action<string> ShowAlert;

        public ConfigureViewModel(string emergency)
        {
            Save = new Command(OnSave);
            EmergencyImage = "profile.png";
            EmergencyName = emergency.ToUpper();
            //ShowData(emergency);
        }

        public void ShowData(string emergency)
        {
            var _db = App.EmergencyDatabase.GetEmergency(emergency);
            EmergencyImage = emergency.ToLower() + ".png";
            EmergencyName = _db.EmergencyName;
            ContactName = _db.ContactName;
            ContactNumber = _db.ContactNumber;
            MessageTemplate = _db.MessageTemplate;
        }
        public void OnSave()
        {
            if (CheckFields())
            {
                var Emergency = new Emergency()
                {
                    EmergencyName = emergencyName,
                    ContactName = contactName,
                    ContactNumber = contactNumber,
                    MessageTemplate = messageTemplate
                };
                ShowAlert(App.EmergencyDatabase.UpdateEmergency(Emergency));
            }
            else
                ShowAlert("failed");
        }
        public bool CheckFields()
        {
            if (contactName!=null&&contactNumber!=null&&messageTemplate!=null)
                return true;
            else
                return false;
        }
    }
}

