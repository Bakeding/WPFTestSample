using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfMVVM.Models;

namespace WpfMVVM
{
    /// <summary>
    /// DataGridTest.xaml 的交互逻辑
    /// </summary>
    public partial class DataGridTest : Window
    {
        public DataGridTest()
        {
            InitializeComponent();
        }

        ObservableCollection<People> peopleList = new ObservableCollection<People>();


        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            peopleList.Add(new People()
            {
                name = "小明",
                age = "18",
                sexual = sexual_enum.BOY,
            });
            peopleList.Add(new People()
            {
                name = "小红",
                age = "18",
                sexual = sexual_enum.GIRL
            });

            ((this.FindName("DATA_GRID")) as DataGrid).ItemsSource = peopleList;
        }

        private void BTN_CHK_DATA_Click(object sender, RoutedEventArgs e)
        {
            string txt = "";
            foreach (People peo in peopleList)
            {
                txt += peo.name;
                txt += peo.age;
                txt += peo.sexual.ToString();
                txt += "\r\n";

            }

            MessageBox.Show(txt);
        }
    }

}