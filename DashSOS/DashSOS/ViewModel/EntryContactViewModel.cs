﻿using System;
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
    public class EntryContactViewModel : INotifyPropertyChanged
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
                NotifyPropertyChanged();
            }
        }
        private string contactNumber;
        public string ContactNumber
        {
            get { return contactNumber; }
            set
            {
                contactNumber = value;
                NotifyPropertyChanged();
            }
        }
        private int contactId;
        public int ContactId
        {
            get { return contactId; }
            set
            {
                contactId = value;
            }
        }
        public ICommand Add { protected set; get; }

        public EntryContactViewModel(string function, string emergency, int id = 0)
        {
            emergencyName = emergency;
            contactId = id;
            Add = new Command(OnAdd);
            if (function == "update")
                ShowData(emergency, id);
            // ShowContact("new", emergencyName);
            //contact.EmergencyName = emergency;
            //ShowData(emergency);
        }

        public async void ShowData(string emergency, int id)
        {
            ContactDatabase db = new ContactDatabase(dbPath);
            var Contact = await db.GetContactAsync(emergency, id);
            EmergencyName = Contact.EmergencyName;
            ContactName = Contact.ContactName;
            ContactNumber = Contact.ContactNumber;
            //var _db = App.EmergencyDatabase.GetEmergency(emergency);
            //EmergencyImage = emergency.ToLower() + ".png";
            //EmergencyName = _db.EmergencyName;
            //ContactName = _db.ContactName;
            //ContactNumber = _db.ContactNumber;
            //MessageTemplate = _db.MessageTemplate;
        }

        public async void OnAdd(object function)
        {
            ContactDatabase db = new ContactDatabase(dbPath);
            if (CheckFields())
            {
                switch (function)
                {
                    case "Add":
                        var Contact = new Contact()
                        {
                            EmergencyName = emergencyName,
                            ContactName = contactName,
                            ContactNumber = contactNumber
                        };
                        //   DependencyService.Get<IToast>().Toasts("custom",emergency);
                        DependencyService.Get<IToast>().Toasts("addContact", db.AddContact(Contact));
                        ContactName = "";
                        ContactNumber = "";
                        MessagingCenter.Send<App>((App)Application.Current, "OnContactAdded");
                        break;
                    case "Update":
                        var UpdateContact = await db.GetContactAsync(emergencyName, contactId);
                        UpdateContact.ContactName = contactName;
                        UpdateContact.ContactNumber = contactNumber;
                        DependencyService.Get<IToast>().Toasts("updateContact", db.UpdateContact(UpdateContact));
                        MessagingCenter.Send<App>((App)Application.Current, "OnContactUpdated");
                        await PopupNavigation.Instance.PopAsync(true);
                        break;
                }

            }
            else
                DependencyService.Get<IToast>().Toasts("addContact", "failed");

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