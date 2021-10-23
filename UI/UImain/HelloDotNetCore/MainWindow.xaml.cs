using HelloDotNetCore.ViewWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HelloDotNetCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.ShowDialog();
        }

        private void btnMenuMVVM_Click(object sender, RoutedEventArgs e)
        {
            MenuMVVM menu = new MenuMVVM();
            menu.ShowDialog();
        }

        private void btnChat_Click(object sender, RoutedEventArgs e)
        {
            ViewWindow.Chat chat = new ViewWindow.Chat();
            chat.ShowDialog();
        }

        private void btnInputDataChange_Click(object sender, RoutedEventArgs e)
        {
            InputDataChangeValidate idcv = new InputDataChangeValidate();
            idcv.ShowDialog();
        }

        private void btnDropDownMenu_Click(object sender, RoutedEventArgs e)
        {
            DropDownMenuWindow ddmw = new DropDownMenuWindow();
            ddmw.ShowDialog();
        }

        private void btnNavigationDrawerPopUpMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationDrawerPopUpMenuWindow ndpmw = new NavigationDrawerPopUpMenuWindow();
            ndpmw.ShowDialog();
        }

        private void btnClock_Click(object sender, RoutedEventArgs e)
        {
            ClockWindow cw = new ClockWindow();
            cw.Show();
        }

        private void btnMenuSlideIn_Click(object sender, RoutedEventArgs e)
        {
            MenuSlideInWindow msw = new MenuSlideInWindow();
            msw.ShowDialog();
        }

        private void btnDashBoard_Click(object sender, RoutedEventArgs e)
        {
            DashBoardWindow dbw = new DashBoardWindow();
            dbw.ShowDialog();
        }

        private void btnCustomMenu_Click(object sender, RoutedEventArgs e)
        {
            CustomMenuWindow cmw = new CustomMenuWindow();
            cmw.ShowDialog();
        }
    }
}
