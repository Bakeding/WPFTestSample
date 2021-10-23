using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using System.Windows.Shapes;

namespace DataBinding.ViewWindow
{
    /// <summary>
    /// ListViewTestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ListViewTestWindow : MetroWindow
    {
        public ListViewTestWindow()
        {
            InitializeComponent();
            this.listView.DataContext = CreateDataTable();
        }
        DataTable CreateDataTable()
        {
            DataTable tbl = new DataTable("Customers");

            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("Balance", typeof(decimal));

            tbl.Rows.Add(1, "John Doe", 100m);
            tbl.Rows.Add(2, "Jane Dorkenheimer", -209m);
            tbl.Rows.Add(3, "Fred Porkroomio", 0m);
            tbl.Rows.Add(4, "Mike Spike", 550m);
            tbl.Rows.Add(5, "Doris Yakovakovich", 0m);
            tbl.Rows.Add(6, "Boris Zinkwolf", -25m);

            return tbl;
        }
    }

    [ValueConversion(typeof(object), typeof(int))]
    public class NumberToPolarValueConverter : IValueConverter
    {
        public object Convert(
        object value, Type targetType,
        object parameter, CultureInfo culture)
        {
            double number = (double)System.Convert.ChangeType(value, typeof(double));

            if (number < 0.0)
                return -1;

            if (number == 0.0)
                return 0;

            return +1;
        }

        public object ConvertBack(
        object value, Type targetType,
        object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack not supported");
        }
    }
}
