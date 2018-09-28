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

namespace DashSOS.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigureView
    {

        public ConfigureView(string emergency)
        {
            var configureViewModel = new ConfigureViewModel(emergency);
            this.BindingContext = configureViewModel;
            configureViewModel.ShowAlert += (string status) => ShowAlert(status);
            InitializeComponent();
        }
        public void Hide(object o, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }
        
        public async void ShowAlert(string status)
        {
            switch (status)
            {
                case "success":
                    await DisplayAlert("Success", "Emergency has been updated", "OK");
                    await PopupNavigation.Instance.PopAsync(true);
                    break;

                case "failed":
                    await DisplayAlert("Error", "Please complete the required info", "OK");
                    break;
            }

        }
    }
}