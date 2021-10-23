using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementMVVM.Services
{
    class MockOrderService:IOrderService
    {

        public void PlaceOrder(List<string> dishes)
        {
            System.IO.File.WriteAllLines(@"G:\order.txt",dishes.ToArray());
        }
    }
}
