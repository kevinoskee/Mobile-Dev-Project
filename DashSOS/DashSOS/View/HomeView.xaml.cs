using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DashSOS.ViewModel;
using DashSOS.Extras;
using System.IO;
using DashSOS.Database;
using Rg.Plugins.Popup.Services;
using DashSOS.Model;
namespace DashSOS.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"DashSOS.db3");
        public HomeView()
        {
            var homeviewModel = new HomeViewModel();
            this.BindingContext = homeviewModel;
            #region change
            //homeviewModel.ChangeButton += (string name) =>
            //{
            //    switch (name)
            //    {
            //        case "edit":
            //            //configBtn.IsVisible = false;
            //            //doneBtn.IsVisible = true;
            //            //returnBtn.IsVisible = true;
            //            CheckEmergency("editMode");
            //            break;
            //        case "done":
            //        case "return":
            //            //configBtn.IsVisible = true;
            //            //doneBtn.IsVisible = false;
            //            //returnBtn.IsVisible = false;
            //            CheckEmergency("readyMode");
            //            break;

            //    }               
            //};
            #endregion
            InitializeComponent();
            CheckEmergency(emergencyMode.Text);
        }
        
        public async void CheckEmergency(string mode)
        {
          
            string[] emergencies = { "Police", "Medical", "Fire", "Family" };
            if (mode == "Emergency Mode")
            {
                foreach (string value in emergencies)
                {
                    ContactDatabase db = new ContactDatabase(dbPath);
                    var _db = await db.CountContact(value);
                    if (_db > 0)
                    {
                        switch (value)
                        {
                            case "Police":
                                Police.Source = "police.png";
                                PoliceText.TextColor = Color.White;
                                break;
                            case "Medical":
                                Medical.Source = "medical.png";
                                MedicalText.TextColor = Color.White;
                                break;
                            case "Fire":
                                Fire.Source = "fire.png";
                                FireText.TextColor = Color.White;
                                break;
                            case "Family":
                                Family.Source = "family.png";
                                FamilyText.TextColor = Color.White;
                                break;
                        }
                               

                    }
                    else
                    {
                            switch (value)
                            {
                                case "Police":
                                    Police.Source = "policeX.png";
                                    PoliceText.TextColor = Color.SlateGray;
                                    break;
                                case "Medical":
                                    Medical.Source = "medicalX.png";
                                    MedicalText.TextColor = Color.SlateGray;
                                    break;
                                case "Fire":
                                    Fire.Source = "fireX.png";
                                    FireText.TextColor = Color.SlateGray;
                                    break;
                                case "Family":
                                    Family.Source = "familyX.png";
                                    FamilyText.TextColor = Color.SlateGray;
                                    break;
                            }
                        
                    }
                }
             

                }

            else
            {
                foreach (string value in emergencies)
                {
                    switch (value)
                    {
                        case "Police":
                            Police.Source = "police.png";
                            PoliceText.TextColor = Color.White;
                            break;
                        case "Medical":
                            Medical.Source = "medical.png";
                            MedicalText.TextColor = Color.White;
                            break;
                        case "Fire":
                            Fire.Source = "fire.png";
                            FireText.TextColor = Color.White;
                            break;
                        case "Family":
                            Family.Source = "family.png";
                            FamilyText.TextColor = Color.White;
                            break;
                    }
                }

            }
        }

        public void OnToggled(object s, ToggledEventArgs e)
        {


            if (switchBtn.IsToggled)
            {
                emergencyMode.Text = "Emergency Mode";
                modeFrame.BackgroundColor = Color.FromHex("#e74c3c");
                CheckEmergency(emergencyMode.Text);
            }
            else
            {
                emergencyMode.Text = "Setup Mode";
                modeFrame.BackgroundColor = Color.FromHex("#2ecc71");
                CheckEmergency(emergencyMode.Text);
            }
        }
    }
}