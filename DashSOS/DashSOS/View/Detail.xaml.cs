using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DashSOS.ViewModel;

namespace DashSOS.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detail : ContentPage
    {
        public Detail()
        {
            var detailviewModel = new DetailViewModel();
            this.BindingContext = detailviewModel;

            detailviewModel.ChangeButton += (string name) =>
            {
                switch (name)
                {
                    case "configure":
                        configBtn.IsVisible = false;
                        saveBtn.IsVisible = true;
                        cancelBtn.IsVisible = true;
                        
                        break;
                    case "save":
                    case "cancel":
                        configBtn.IsVisible = true;
                        saveBtn.IsVisible = false;
                        cancelBtn.IsVisible = false;
                        break;

                }
               
            };
            InitializeComponent();
        }
	}
}