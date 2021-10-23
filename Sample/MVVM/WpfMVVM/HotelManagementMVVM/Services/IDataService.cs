using HotelManagementMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementMVVM.Services
{
    public interface IDataService
    {
        List<Dish> GetAllDishes();
    }
}
