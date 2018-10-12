using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DashSOS.ViewModel;
using DashSOS.Model;
using DashSOS.Database;
using System.IO;

namespace DashSOS.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetUpView : ContentPage
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DashSOS.db3");
        public bool hasEntry = false;
        public static int contactToSave;
        public static bool isFirstContact = true;
        int contactCount;
        string emergencyName;
        public SetUpView(string emergency)
        {
            emergencyName = emergency;
            var SetupViewModel = new SetUpViewModel(emergency);
            this.BindingContext = SetupViewModel;
            //SetupViewModel.ShowAlert += (string status) => ShowAlert(status);
            SetupViewModel.AddContact += (string _emergency) => AddContact(emergency);
            InitializeComponent();
            CountContact(emergency);
            //   CheckContact(emergency);
            if (contactCount > 0)
                ShowContact(emergency);
            else
            {
                isFirstContact = true;
                noContact.IsVisible = true;
                contacts.IsVisible = false;
            }
            //  CheckContact(emergency);
        }
        //    public void Hide(object o, EventArgs e)
        //    {
        //        PopupNavigation.Instance.PopAsync(true);
        //    }
        public void AddContact(string emergency)
        {
            #region AddTest
            //        //Grid contact = new Grid
            //        //{
            //        //    Padding = new Thickness(20, 5, 20, 10),
            //        //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //        //    VerticalOptions = LayoutOptions.Fill,
            //        //    BackgroundColor = Xamarin.Forms.Color.FromHex("#B3486684"),
            //        //    RowSpacing = 5,

            //        //};
            //        //contact.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            //        //contact.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });

            //        //Entry contactName = new Entry
            //        //{

            //        //    FontSize = 15,
            //        //    TextColor = System.Drawing.Color.White,
            //        //    HorizontalOptions = LayoutOptions.Fill,
            //        //    VerticalOptions = LayoutOptions.Center
            //        //};
            //        //Entry contactNumber = new Entry
            //        //{

            //        //    FontSize = 15,
            //        //    TextColor = System.Drawing.Color.White,
            //        //    HorizontalOptions = LayoutOptions.Fill,
            //        //    VerticalOptions = LayoutOptions.Center
            //        //};
            //        //Label contactId = new Label
            //        //{

            //        //    FontSize = 15,
            //        //    TextColor = System.Drawing.Color.Transparent,
            //        //};
            //        //contactName.SetBinding(Entry.TextProperty,"Contact.ContactName",BindingMode.TwoWay);
            //        //contactNumber.SetBinding(Entry.TextProperty, "Contact.ContactNumber", BindingMode.TwoWay);
            //        //contactId.SetBinding(Entry.TextProperty, "Contact.ContactId", BindingMode.TwoWay);
            //        //contact.Children.Add(contactId, 0, 0);
            //        //contact.Children.Add(contactName, 0, 0);
            //        //contact.Children.Add(contactNumber, 1, 0);

            //        //contactList.Children.Add(contact);
            //        //var swipeGestureRecognizer = new SwipeGestureRecognizer
            //        //{
            //        //    Direction = SwipeDirection.Right
            //        //};
            //        //swipeGestureRecognizer.Swiped += (s, e) =>
            //        //{
            //        //    contact.IsVisible = false;
            //        //    hasEntry = false;
            //        //    CheckChild();
            //        //};
            //        //contact.GestureRecognizers.Add(swipeGestureRecognizer);
            #endregion

            NewContact newContact = new NewContact(emergency);
            newContact.contactId.Text = "-1";
            contactList.Children.Add(newContact);
            contactToSave++;
            isFirstContact = false;
            noContact.IsVisible = false;
            contacts.IsVisible = true;

        }

        public async void ShowContact(string emergency)
        {
            ContactDatabase db = new ContactDatabase(dbPath);
            var list = await db.GetContactsAsync(emergency);
            foreach (Contact contact in list)
            {
                NewContact newContact = new NewContact(emergency);
                newContact.contactId.Text = contact.ContactId.ToString();
                newContact.contactName.Text = contact.ContactName;
                newContact.contactNumber.Text = contact.ContactNumber;
                contactList.Children.Add(newContact);
                contactToSave++;
            }
        }
        public void OnLayoutChanged(object s, EventArgs e)
        {
            DependencyService.Get<IToast>().Toasts("custom", contactToSave.ToString());
            hasEntry = false;
            if (contactToSave == 0)
            {
                DependencyService.Get<IHideKeyboard>().HideKeyboard();
                SetUpView.isFirstContact = true;
                noContact.IsVisible = true;
                contacts.IsVisible = false;
            }
        }

        public async void CountContact(string emergency)
        {
            ContactDatabase db = new ContactDatabase(dbPath);
            contactCount = await db.CountContact(emergency);
            DependencyService.Get<IToast>().Toasts("custom", contactCount.ToString());

        }
        public async void CheckContact(string emergency)
        {

            ContactDatabase db = new ContactDatabase(dbPath);
            var _db = await db.GetContactAsync(emergency);
            if (_db == null)
            {
                noContact.IsVisible = true;
                contacts.IsVisible = false;

            }
            else
            {
                noContact.IsVisible = false;
                contacts.IsVisible = true;
            }
        }

        public void OnAdd(object s, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new EntryContactView(emergencyName));
        }
    }
}