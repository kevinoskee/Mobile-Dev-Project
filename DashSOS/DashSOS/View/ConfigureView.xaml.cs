using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ConfigureView ()
		{
            
            InitializeComponent ();
            SetLayout();
        }
        public void Hide(object o, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }
        public void SetLayout()
        {
            emergencyImage.Source = EmergencyModel.ImageSource;
            emergencyName.Text = EmergencyModel.EmergencyName;
            contactName.Text = EmergencyModel.ContactName;
            contactNumber.Text = EmergencyModel.ContactNumber;
            messageTemplate.Text = EmergencyModel.MessageTemplate;

        }
	}
}