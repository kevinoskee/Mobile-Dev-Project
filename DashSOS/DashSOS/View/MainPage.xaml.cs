using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DashSOS.ViewModel;
namespace DashSOS.View
{
	public partial class MainPage : MasterDetailPage
	{
		public MainPage()
		{
            /* var mainviewModel = new MainViewModel();
             this.BindingContext = mainviewModel;*/
            InitializeComponent();
            var detail = new NavigationPage(new DetailView())
            {
                BarBackgroundColor = Color.FromHex("#34495e")
            };
            var master = new NavigationPage(new MasterView())
            {
                BarBackgroundColor = Color.FromHex("#2c3e50"),
                Title="Master",
            };
            this.Master = master;
            this.Detail = detail;
            App.MasterDetail = this;



        }
	}
}
