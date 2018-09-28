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
    public class EmergencyDatabase
    {
        private SQLiteConnection conn;
        //CREATE  
        public EmergencyDatabase()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<Emergency>();
        }

        //READ  
        public Emergency GetEmergency(string emergency)
        {
            return conn.Table<Emergency>().Where(i => i.EmergencyName == emergency).First();
        }

        //CHECK IF EMPTY
        public bool CheckEmergency()
        {
            var entryCount = conn.Table<Emergency>().Count();
            if (entryCount > 0)
                return true;
            else
                return false;
        }

        //INSERT  
        public string AddEmergency(Emergency emergency)
        {
            conn.Insert(emergency);
            return "success";
        }
        //DELETE  
        public string DeleteEmergency(string id)
        {
            conn.Delete<Emergency>(id);
            return "success";
        }

        //EDIT
        public string UpdateEmergency(Emergency emergency)
        {
            conn.Update(emergency);
            return "success";
        }

    }
}
