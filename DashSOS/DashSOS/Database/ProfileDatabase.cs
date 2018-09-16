using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SQLite;
using DashSOS.Model;
using System.Linq;
using Xamarin.Forms;
using DashSOS.Interface;
using System.Collections;
namespace DashSOS.Database
{
    public class ProfileDatabase
    {
        private SQLiteConnection conn;
        //CREATE  
        public ProfileDatabase()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<Profile>();
        }

        //READ  
        public Profile GetProfile()
        {
            return conn.Table<Profile>().First();
        }

        //CHECK IF EMPTY
        public bool CheckProfile()
        {
            var entryCount = conn.Table<Profile>().Count();
            if (entryCount > 0)
                return true;
            else
                return false;
        }

        //INSERT  
        public string AddProfile(Profile profile)
        {
            conn.Insert(profile);
            return "success";
        }
        //DELETE  
        public string DeleteProfile(int id)
        {
            conn.Delete<Profile>(id);
            return "success";
        }
        //EDIT
        public string UpdateProfile(Profile profile)
        {
            conn.Update(profile);
            return "success";
        }

    }
}
