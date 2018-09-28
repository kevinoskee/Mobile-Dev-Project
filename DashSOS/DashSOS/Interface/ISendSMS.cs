using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace DashSOS
{
    public interface ISendSMS
    {
        void Message(string number,string location);
        void Send(string number,string message);
    }
}
