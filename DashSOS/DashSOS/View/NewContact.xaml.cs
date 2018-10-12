using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashSOS;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DashSOS.ViewModel;
using DashSOS.Model;
using System.ComponentModel;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using DashSOS.View;
using DashSOS.Database;
using SQLite;
using System.IO;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace DashSOS.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewContact : ContentView
	{
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DashSOS.db3");
        public NewContact (string emergency)
		{
            var setupViewModel = new SetUpViewModel(emergency);
            this.BindingContext = setupViewModel;
            InitializeComponent();
		}
        public void OnSwiped(object s, EventArgs e)
        {
            if(Convert.ToInt32(this.contactId.Text) < 0)
                this.IsVisible = false;
            else
            {
                ContactDatabase db = new ContactDatabase(dbPath);
                DependencyService.Get<IToast>().Toasts("deleteContact", db.DeleteContact(Convert.ToInt32(this.contactId.Text))); 
            } 
            SetUpView.contactToSave--;
        }
        public void DisableContact()
        {
            this.IsEnabled = false;
        }
    }
}