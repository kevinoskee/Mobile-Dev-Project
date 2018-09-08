using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DashSOS.ViewModel;
namespace DashSOS
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            var mainviewModel = new MainViewModel();
            this.BindingContext = mainviewModel;
            InitializeComponent();
		}
	}
}
