using HelloDotNetCore.ViewModels;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;

using System.Windows.Media;

namespace HelloDotNetCore.ViewWindow
{
    /// <summary>
    /// MenuMVVM.xaml 的交互逻辑
    /// </summary>
    public partial class MenuMVVM : Window
    {
        public MenuMVVM()
        {
            InitializeComponent();
            List<MenuItem> menu = new List<MenuItem>();

            menu.Add(new MenuItem("Images", PackIconKind.Image, new ItemCount(Brushes.Black, 2)));
            menu.Add(new MenuItem("Sounds", PackIconKind.Music, new ItemCount(Brushes.DarkBlue, 4)));
            menu.Add(new MenuItem("Video", PackIconKind.Video, new ItemCount(Brushes.DarkGreen, 7)));
            menu.Add(new MenuItem("Documents", PackIconKind.Folder, new ItemCount(Brushes.DarkOrange, 9)));

            ListViewMenu.ItemsSource = menu;
        }
    }
}
