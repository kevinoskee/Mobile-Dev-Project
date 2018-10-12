﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using DashSOS.View;
using DashSOS.Model;
namespace DashSOS.ViewModel
{
    public class ProfileViewModel
    {
        public ICommand EditProfile { protected set; get; }

        public ProfileViewModel()
        {
            EditProfile = new Command(OnEditProfile);

        }

        public async void OnEditProfile()
        {
            await App.NavigateMasterDetail(new EditProfileView("edit"));
        }
    }
}
