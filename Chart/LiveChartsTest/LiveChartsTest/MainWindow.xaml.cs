using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace LiveChartsTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public  List <string> Labels { get; set; }
        private double _trend;
        //private double[] temp = { 1, 3, 2, 4,-3,5,2,1 };
        private double[] temp = { };
        private int span=8;

        public MainWindow()
        {		
            InitializeComponent();
			//实例化一条折线图
            LineSeries mylineseries = new LineSeries();
            
            //设置折线的标题
            mylineseries.Title = "温度";
            //折线图直线形式
            mylineseries.LineSmoothness = 0;
            //折线图的无点样式
            mylineseries.PointGeometry = null;
            //添加横坐标
            //Labels = new List<int> { 1,2,3,4,5,6,7,8 };
            Labels = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8" };
            //Labels = new List<string> {  };
            //添加折线图的数据
            mylineseries.Values = new ChartValues<double>(temp);
            SeriesCollection = new SeriesCollection { };
            SeriesCollection.Add(mylineseries);
            _trend = 2;
			linestart();
			DataContext = this;
        }
        //连续折线图的方法
        public void linestart()
        {
            Task.Run(() =>
            {
                var r = new Random();
                while (true)
                {
                    Thread.Sleep(1000);
                    _trend = r.Next(-10, 10);
                    //通过Dispatcher在工作线程中更新窗体的UI元素
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        //更新横坐标时间
                        Labels.Add(DateTime.Now.ToString());
                        //更新纵坐标数据
                        SeriesCollection[0].Values.Add(_trend);
                        //IChartValues icv=SeriesCollection[0].Values;
                        //IEnumerable<ChartPoint> cp = icv.GetPoints(SeriesCollection[0]);
                        //int cpC=cp.Count();
                        if (SeriesCollection[0].Values.GetPoints(SeriesCollection[0]).Count() > span)
                        {
                            Labels.RemoveAt(0);
                            SeriesCollection[0].Values.RemoveAt(0);
                        }
                        
                        
                        
                    });
                }
            });
        }
    }
}
