using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace DashSOS
{
    public interface IGetLocation
    {
        Task Location();
        void Test();
    }
}
