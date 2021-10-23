using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
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
using HappyPrint;

namespace PrintHelperTestDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private double _btnHeight;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable("PrintHelper");

            DataColumn column;

            for (int i = 0; i < 2; i++)
            {
                column = new DataColumn(string.Format("控制器{0}", i));

                dt.Columns.Add(column);
            }

            for (int i = 0; i < 1000; i++)
            {
                var row = dt.NewRow();

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j % 2 == 0)
                    {
                        row[string.Format("控制器{0}", j)] = i + "探测器为了增加字符串长度而是用的" + j;
                    }
                    else
                    {
                        row[string.Format("控制器{0}", j)] = i + "探测器" + j;
                    }
                }

                dt.Rows.Add(row);
            }

            PrintHelper.PrintDataTable(dt);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("img/test.jpg", UriKind.RelativeOrAbsolute));

            PrintHelper.PrintPicture(bitmapImage);
        }

        private void ButtonBase3_OnClick(object sender, RoutedEventArgs e)
        {
            PrintHelper.PrintControl(window);
        }
    }
}
