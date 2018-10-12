using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SQLite;
using DashSOS.Model;
using System.Linq;
using Xamarin.Forms;
using System.Collections;
namespace DashSOS.Database
{
    public class ContactDatabase
    {
        private SQLiteAsyncConnection conn;
        //CREATE  
        public ContactDatabase(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Contact>().Wait();
        }

        //READ  
        public Task<Contact> GetContactAsync(string emergency)
        {
            return conn.Table<Contact>().Where(i => i.EmergencyName == emergency).FirstOrDefaultAsync();
        }
        public Task<List<Contact>> GetContactsAsync(string emergency)
        {
            return conn.Table<Contact>().Where(i => i.EmergencyName == emergency).ToListAsync();
        }
        //READ  
        public Task<int> CountContact(string emergency)
        {
            return conn.Table<Contact>().Where(i => i.EmergencyName == emergency).CountAsync();
        }
        ////CHECK IF EMPTY
        //public bool CheckEmergency()
        //{
        //    var entryCount = conn.Table<Emergency>().Count();
        //    if (entryCount > 0)
        //        return true;
        //    else
        //        return false;
        //}

        //INSERT  
        public string AddContact(Contact contact)
        {
            conn.InsertAsync(contact);
            return "success";
        }
        //DELETE  
        public string DeleteContact(int id)
        {
            conn.DeleteAsync<Contact>(id);
            return "success";
        }

        //EDIT
        public string UpdateEmergency(Contact contact)
        {
            conn.UpdateAsync(contact);
            return "success";
        }

    }
}
