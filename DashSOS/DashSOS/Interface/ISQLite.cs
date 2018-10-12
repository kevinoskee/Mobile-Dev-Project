using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;
using SQLite;
namespace DashSOS.Interface
{
    public interface ISQLite
    {
       SQLiteAsyncConnection GetConnection();
    }
}
