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
    public partial class DetailView : ContentPage
    {
        public DetailView()
        {
            var detailviewModel = new DetailViewModel();
            this.BindingContext = detailviewModel;

            detailviewModel.ChangeButton += (string name) =>
            {
                switch (name)
                {
                    case "configure":
                        configBtn.IsVisible = false;
                        doneBtn.IsVisible = true;
                        returnBtn.IsVisible = true;
                        
                        break;
                    case "done":
                    case "return":
                        configBtn.IsVisible = true;
                        doneBtn.IsVisible = false;
                        returnBtn.IsVisible = false;
                        break;

                }
               
            };
            InitializeComponent();
        }
	}
}