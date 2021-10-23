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
using System.Windows.Shapes;
using System.Windows.Controls.DataVisualization.Charting;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace DataBinding.ViewWindow
{
    /// <summary>
    /// DoubleAxisPlotWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DoubleAxisPlotWindow : Window
    {
        double xCeshiData = 0, yCeshiData = 0;
        Dictionary<double, double> d2;

        //chart2======================================
        ObservableCollection<ChartData> lineSeries1Data;
        Thread MyThread;
        LineSeries lineSeries;
        ChartData objChartData;
        Random r = new Random();
        LineSeries temp;

        public DoubleAxisPlotWindow()
        {
            InitializeComponent();

            PlotModel chart = new PlotModel();
            chart.Title = "我的图表";
            chart.xTitleList = new List<string>() { "主动轮转速 [r/min]" };
            chart.yTitleList = new List<string>() { "转矩 [N.m]", "传动效率 [%]" };
            chart.lineTitleList = new List<string> { "转矩", "传动效率" };

            List<Dictionary<int, int>> plotList = new List<Dictionary<int, int>>();
            Dictionary<int, int> d = new Dictionary<int, int>();
            d.Add(1, 50);
            d.Add(2, 40);
            d.Add(3, 50);
            d.Add(4, 40);
            d.Add(5, 50);
            plotList.Add(d);
            Dictionary<int, int> d1 = new Dictionary<int, int>();
            d1.Add(1, 100);
            d1.Add(2, 200);
            d1.Add(3, 300);
            d1.Add(4, 400);
            d1.Add(5, 500);

            plotList.Add(d1);
            chart.lines = plotList;
            grid.DataContext = chart;

            //动态曲线
            //d2 = new Dictionary<double, double>();

            //DispatcherTimer dTimer = new DispatcherTimer();
            //dTimer.Interval = TimeSpan.FromMilliseconds(100);
            //dTimer.Tick += new EventHandler(RefreshPlot);
            //dTimer.Start();

            //chart2=============================
            objChartData = new ChartData();
            lineSeries1Data = new ObservableCollection<ChartData>();
            AddLineSeries();
            InitializeDataPoints();
            MyThread = new Thread(new ThreadStart(StartChartDataSimulation));
        }

        private void DataCeshi()
        {
            while (true)
            {
                xCeshiData = xCeshiData + 1;
                yCeshiData = 1000 * Math.Sin(xCeshiData * Math.PI / 100);
                d2.Add(xCeshiData, yCeshiData);
                Thread.Sleep(1000);


            }
        }

        private void RefreshPlot(object sender, EventArgs e)
        {
            line3.Dispatcher.Invoke(new Action(() =>
            {
                xCeshiData = xCeshiData + 1;
                yCeshiData = 1000 * Math.Sin(xCeshiData * Math.PI / 100);
                d2.Add(xCeshiData, yCeshiData);
                line3.ItemsSource = d2;
                line3.Refresh();
                //line3.Title = "" + xCeshiData;
            }));
        }

        //chart2====================================
        private void AddLineSeries()
        {
            lineSeries = AddLineSeries(Brushes.Tomato);
            lineSeries.DependentValuePath = "Value";
            lineSeries.IndependentValuePath = "Name";
            lineSeries.ItemsSource = lineSeries1Data;
            simChart.Series.Add(lineSeries);
        }

        private LineSeries AddLineSeries(Brush lineColor)
        {
            temp = new LineSeries()
            {
                PolylineStyle = this.FindResource("PolylineStyle") as Style,
                DataPointStyle = this.FindResource("LineDataPointStyle") as Style,
                Style = this.FindResource("CommonLineSeries") as Style,
                Tag = lineColor
            };
            return temp;
        }

        private void InitializeDataPoints()
        {
            lineSeries1Data.Add(new ChartData() { Name = DateTime.Now, Value = 0.0 });
            Thread.Sleep(1000);
            for (int i = 0; i < 2; i++)
            {
                lineSeries1Data.Add(new ChartData() { Name = DateTime.Now, Value = new Random().NextDouble() * 10 });
                Thread.Sleep(1000);
            }

        }

        public void StartChartDataSimulation()
        {
            int i = 0;
            while (true)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    objChartData.Name = DateTime.Now;
                    objChartData.Value = r.NextDouble() * 50;
                    lineSeries1Data.Add(objChartData);
                    if (lineSeries1Data.Count > 20)
                    {
                        lineSeries1Data.RemoveAt(0);
                    }
                }));
                Thread.Sleep(500);
            }
        }


        private void btnStartCharting_Click(object sender, RoutedEventArgs e)
        {
            if (MyThread != null && MyThread.ThreadState != ThreadState.Running && MyThread.ThreadState != ThreadState.WaitSleepJoin && MyThread.ThreadState != ThreadState.Suspended)
            {
                MyThread.Start();
            }
            else if(MyThread.ThreadState!=ThreadState.Suspended)
            {
                MyThread.Suspend();
            }
            else
            {
                MyThread.Resume();
            }
        }

    }

    public class ChartData
    {
        DateTime _Name;
        double _Value;

        public DateTime Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public double Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

    }

    class PlotModel
    {
        public string Title { get; set; }
        public List<string> xTitleList { get; set; }
        public List<string> yTitleList { get; set; }
        public List<string> lineTitleList { get; set; }
        public List<Dictionary<int, int>> lines { get; set; }
        private Dictionary<double, double> lineDictionary;

        public Dictionary<double, double> LineDictionary
        {
            get { return lineDictionary; }
            set
            {
                lineDictionary = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LineDictionary"));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;



    }

}