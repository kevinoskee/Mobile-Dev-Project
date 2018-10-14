using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace DashSOS
{
    public interface ISendSMS
    {
        void Send(string number,string message);
    }
}
