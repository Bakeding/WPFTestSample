using HotelManagementMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementMVVM.ViewModels;

namespace HotelManagementMVVM.ViewModels
{
    class DishMenuItemViewModel:NotificationObject
    {
        public Dish Dish { get; set; }
        private bool isSelected;

        public bool IsSelected
        {
          get { return isSelected; }
          set 
          { 
              isSelected = value; 
              this.RaisePropertyChanged("isSelected");
          }
        }
    }
}
