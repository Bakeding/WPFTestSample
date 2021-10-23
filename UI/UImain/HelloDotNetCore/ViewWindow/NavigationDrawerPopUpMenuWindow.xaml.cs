using HelloDotNetCore.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HelloDotNetCore.ViewWindow
{
    /// <summary>
    /// NavigationDrawerPopUpMenuWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NavigationDrawerPopUpMenuWindow : Window
    {
        public NavigationDrawerPopUpMenuWindow()
        {
            InitializeComponent();
            //var menuRegister = new List<ItemMenu>();
            //menuRegister.Add(new ItemMenu("Customer", PackIconKind.Register));
            //menuRegister.Add(new ItemMenu("Providers", PackIconKind.Register));
            //menuRegister.Add(new ItemMenu("Employees", PackIconKind.Register));
            //menuRegister.Add(new ItemMenu("Products", PackIconKind.Register));
            //var item6 = new ItemMenu("Register", menuRegister, PackIconKind.Register);

            //var menuSchedule = new List<ItemMenu>();
            //menuSchedule.Add(new ItemMenu("Services", PackIconKind.Schedule));
            //menuSchedule.Add(new ItemMenu("Meetings", PackIconKind.Schedule));
            //var item1 = new ItemMenu("Appointments", menuSchedule, PackIconKind.Schedule);

            //var menuReports = new List<ItemMenu>();
            //menuReports.Add(new ItemMenu("Customers", PackIconKind.FileReport));
            //menuReports.Add(new ItemMenu("Providers", PackIconKind.FileReport));
            //menuReports.Add(new ItemMenu("Products", PackIconKind.FileReport));
            //menuReports.Add(new ItemMenu("Stock", PackIconKind.FileReport));
            //menuReports.Add(new ItemMenu("Sales", PackIconKind.FileReport));
            //var item2 = new ItemMenu("Reports", menuReports, PackIconKind.FileReport);

            //var menuExpenses = new List<ItemMenu>();
            //menuExpenses.Add(new ItemMenu("Fixed", PackIconKind.ShoppingBasket));
            //menuExpenses.Add(new ItemMenu("Variable", PackIconKind.ShoppingBasket));
            //var item3 = new ItemMenu("Expenses", menuExpenses, PackIconKind.ShoppingBasket);

            //var menuFinancial = new List<ItemMenu>();
            //menuFinancial.Add(new ItemMenu("Cash flow", PackIconKind.ScaleBalance));
            //var item4 = new ItemMenu("Financial", menuFinancial, PackIconKind.ScaleBalance);

            //var item0 = new ItemMenu("Dashboard", new UserControl(), PackIconKind.ViewDashboard);

            //Menu.Children.Add(new UserControlMenuItem(item6));
            //Menu.Children.Add(new UserControlMenuItem(item1));
            //Menu.Children.Add(new UserControlMenuItem(item2));
            //Menu.Children.Add(new UserControlMenuItem(item3));
            //Menu.Children.Add(new UserControlMenuItem(item4));
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = new UserControlHome();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCreate":
                    usc = new UserControlCreate();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
