using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfPrintDemo
{
    public class User
    {
        public string OrderNo { get; set; }
        public string CustomerName { get; set; }
        public string ShipAddress { get; set; }
        public string Express { get; set; }
        public decimal Freight { get; set; }
        private OrderDetail orderDetails;

        public OrderDetail OrderDetails
        {
          get { return orderDetails; }
          set { orderDetails = value; }
        }
        

        private decimal totalPrice;

        public decimal TotalPrice
        {
          get { return totalPrice; }
          set { totalPrice = value; }
        }
        
    }

    
}
