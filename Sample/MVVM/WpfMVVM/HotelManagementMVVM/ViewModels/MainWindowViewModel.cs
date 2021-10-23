using HotelManagementMVVM.Models;
using HotelManagementMVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManagementMVVM.ViewModels
{
    class MainWindowViewModel:NotificationObject
    {
        public DelegateCommand PlaceOrderCommand { get; set; }
        public DelegateCommand SeletedMenuCommand { get; set; }

        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                this.RaisePropertyChanged("Count");
            }
        }

        private Restaurant restaurant;
        public Restaurant Restaurant
        {
            get { return restaurant; }
            set
            {
                restaurant = value;
                this.RaisePropertyChanged("Restaurant");
            }
        }

        public List<DishMenuItemViewModel> dishMenu;
        public List<DishMenuItemViewModel> DishMenu
        {
            get { return dishMenu; }
            set
            {
                dishMenu = value;
                this.RaisePropertyChanged("DishMenu");
            }
        }

        public MainWindowViewModel()
        {
            this.LoadRestaurant();
            this.LoadDishMenu();
            this.PlaceOrderCommand = new DelegateCommand(new Action(this.PlaceOrderCommandExecute));
            this.SeletedMenuCommand = new DelegateCommand(new Action(this.SeletedMenuCommandExecute));
        }

        private void LoadDishMenu()
        {
            XmlDataService ds = new XmlDataService();
            var dishes = ds.GetAllDishes();
            this.dishMenu = new List<DishMenuItemViewModel>();
            foreach (var dish in dishes)
            {
                DishMenuItemViewModel item = new DishMenuItemViewModel();
                item.Dish = dish;
                this.DishMenu.Add(item);
            }
        }

        private void LoadRestaurant()
        {
            this.Restaurant = new Restaurant();
            this.Restaurant.Name = "我的餐馆";
            this.Restaurant.Address = "这是地址";
            this.Restaurant.Telephone = "1234566789897";
        }

        private void PlaceOrderCommandExecete()
        {
            var selectedDishes =this.DishMenu.Where(i=>i.IsSelected==true).Select(i=>i.Dish.Name).ToList();
            IOrderService orderService = new MockOrderService();
            orderService.PlaceOrder(selectedDishes);
            MessageBox.Show("订餐成功");
        }

        private void SelectMenuItemExecute()
        { 
            this.Count=this.DishMenu.Count(i=>i.IsSelected==true);
        }
    }
}
