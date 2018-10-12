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
using DashSOS.Database;
using SQLite;
using System.IO;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

using System.Linq;
using Xamarin.Forms.Xaml;
using DashSOS.ViewModel;

namespace DashSOS.ViewModel
{
    public class SetUpViewModel : INotifyPropertyChanged
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DashSOS.db3");
        public event PropertyChangedEventHandler PropertyChanged;
        //private Contact contact;
        //public Contact Contact
        //{
        //    get { return contact; }
        //    set
        //    {
        //        contact = value;
        //        NotifyPropertyChanged("Contact");
        //    }
        //}
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
        private string contactId;
        public string ContactId
        {
            get { return contactId; }
            set
            {
                contactId = value;
            }
        }
        public ICommand Save { protected set; get; }
        public ICommand Add { protected set; get; }
        //public Action<string> ShowAlert;
        public Action<string> AddContact;
        public Action DisableContact;

        public SetUpViewModel(string emergency)
        {
            Save = new Command(OnSave);
            Add = new Command<string>(OnAdd);
            //contact.EmergencyName = emergency;
            //ShowData(emergency);
        }

        public void ShowData(string emergency)
        {
          
            //var _db = App.EmergencyDatabase.GetEmergency(emergency);
            //EmergencyImage = emergency.ToLower() + ".png";
            //EmergencyName = _db.EmergencyName;
            //ContactName = _db.ContactName;
            //ContactNumber = _db.ContactNumber;
            //MessageTemplate = _db.MessageTemplate;
        }
        public void OnSave()
        {

            Application.Current.MainPage = new MainPage();
            DependencyService.Get<IToast>().Toasts("custom", "Contacts saved");
        }

        public void OnAdd(string emergency)
        {
          
            //if (SetUpView.isFirstContact)
            //{
            //    AddContact(emergency);
            //    DependencyService.Get<IToast>().Toasts("custom", "Here's your first contact");
            //}

            //else
            //{
            //    if (CheckFields())
            //    {
            //        ContactDatabase db = new ContactDatabase(dbPath);
            //        var Contact = new Contact()
            //        {
            //            EmergencyName = emergency,
            //            ContactName = contactName,
            //            ContactNumber = contactNumber
            //        };

            //        DependencyService.Get<IToast>().Toasts("addContact", db.AddContact(Contact));
            //        DisableContact();
            //        AddContact(emergency);
            //        SetUpView.isFirstContact = false;
            //    }
            //    else
            //        DependencyService.Get<IToast>().Toasts("custom", contactName);
            //}

        }
  
        bool CheckFields()
        {
           
            if (contactName != null && contactNumber != null)
                return true;
            else
                return false;
           
        }
      
        public void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

