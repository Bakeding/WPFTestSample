using DataBinding.Models;
using DataBinding.Services;
using DataBinding.ViewWindow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

namespace DataBinding
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void listBoxDataBinding_Click(object sender, RoutedEventArgs e)
        {
            ListBoxDataBinding lbb = new ListBoxDataBinding();
            lbb.ShowDialog();
        }

        private void printPreView_Click(object sender, RoutedEventArgs e)
        {
            Person od = new Person();
            PrintPreView ppv = new PrintPreView("ViewWindow/PrintDocument.xaml", od);
            ppv.ShowDialog();
        }

        private void doubleAxisPlot_Click(object sender, RoutedEventArgs e)
        {
            DoubleAxisPlotWindow dapw = new DoubleAxisPlotWindow();
            dapw.ShowDialog();
        }

        private void testDelegateAndEvent_Click(object sender, RoutedEventArgs e)
        {
            TestDelegateAndEventWindow tdew = new TestDelegateAndEventWindow();
            tdew.ShowDialog();
        }

        private void interactiveDataDisplayPlot_Click(object sender, RoutedEventArgs e)
        {
            InteractiveDataDisplayPlotWindow iddpw = new InteractiveDataDisplayPlotWindow();
            iddpw.ShowDialog();
        }

        private void resourcesTest_Click(object sender, RoutedEventArgs e)
        {
            ResourcesTestWindow rtw = new ResourcesTestWindow();
            rtw.ShowDialog();
        }

        private void listViewTest_Click(object sender, RoutedEventArgs e)
        {
            ListViewTestWindow lvtw = new ListViewTestWindow();
            lvtw.ShowDialog();
        }

        private void threadPauseTest_Click(object sender, RoutedEventArgs e)
        {
            ThreadPauseWindow tpw = new ThreadPauseWindow();
            tpw.ShowDialog();
        }

        private void liveChartTest_Click(object sender, RoutedEventArgs e)
        {
            liveChartTestWindow LCTW = new liveChartTestWindow();
            LCTW.ShowDialog();
        }

        private void doubleAxisDynamicPlot_Click(object sender, RoutedEventArgs e)
        {
            DoubleAxisDynamicPlotWindow w = new DoubleAxisDynamicPlotWindow();
            w.ShowDialog();
        }

        private void sqliteTest_Click(object sender, RoutedEventArgs e)
        {
            SQLiteService ss = new SQLiteService();
            AAA a = new AAA();
            //a.rowid = 1;
            //a.bk1 = "1";
            ////ss.Add<AAA>(a);
            //a.bk2 = "77676";
            //ss.Update<AAA>(a);
            a = ss.Query<AAA>("select * from AAA where bk1='1'").FirstOrDefault<AAA>();
            MessageBox.Show(a.rowid+"");

            DataTable dataTable = SQLiteHelper.ExecuteDataSet("Data Source=" + @"ACT.db;Version=3", @"select * from [UserInfo]  where Account='Account0'",null).Tables[0];
            //判断
            if (dataTable != null)
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dtRow = dataTable.Rows[0];
                    Console.WriteLine(dtRow["UserName"] as string);
                }

        }
        private void intToShort_Click(object sender, RoutedEventArgs e)
        {
            IntToShortWindow w = new IntToShortWindow();
            w.ShowDialog();
        }




    }
}
