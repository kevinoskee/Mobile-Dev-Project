using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DashSOS.ViewModel;
using DashSOS.Database;

namespace DashSOS.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterView : ContentPage
	{
		public MasterView ()
		{
            var masterviewModel = new MasterViewModel();
            this.BindingContext = masterviewModel;

            InitializeComponent();
            SetData();
            
		}
        public void SetData()
        {
            var _db = App.ProfileDatabase.GetProfile();
            string _name = "";
            string _address = "";
            int _age;

            _name += _db.FirstName + " " + ((_db.MiddleName != null) ? _db.MiddleName.ElementAt(0).ToString() : "") + ". " + _db.LastName ;
            name.Text = _name;
            _address += _db.HouseNumber + " " + _db.Street + " St. Brgy. " + _db.Barangay + " " + _db.Town + ", " + _db.City;
            address.Text = _address;
            _age = ((DateTime.Now.DayOfYear < _db.Birthdate.DayOfYear) ? DateTime.Now.Year - _db.Birthdate.Year - 1 : DateTime.Now.Year - _db.Birthdate.Year);
            age.Text = _age.ToString();
            blood.Text = _db.BloodGroup;
            other.Text = _db.OtherInfo;
        }
	}
}