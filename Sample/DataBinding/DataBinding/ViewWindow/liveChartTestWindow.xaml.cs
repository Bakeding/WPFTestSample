using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace DataBinding.ViewWindow
{
    /// <summary>
    /// liveChartTestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class liveChartTestWindow : Window
    {
        private double _axisMax;
        private double _axisMin;
        private DateTime startTime;
        LineSeries lineSerie;
        
        public liveChartTestWindow()
        {
            InitializeComponent();

            var mapper = Mappers.Xy<ObservablePoint>()
                .X(point => point.X)   //use DateTime.Ticks as X
                .Y(point => point.Y);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<ObservablePoint>(mapper);

            //the values property will store our values array
            ChartValues = new ChartValues<ObservablePoint>();
            ChartValuesZDL = new ChartValues<ObservablePoint>();
            ChartValuesCDL = new ChartValues<ObservablePoint>();
            ChartValuesDL = new ChartValues<ObservablePoint>();

            //lets set how to display the X Labels
            //DateTimeFormatter = value => new DateTime((long)value).ToString("mm:ss");

            //AxisStep forces the distance between each separator in the X axis
            //AxisStep = 1;
            //AxisUnit forces lets the axis know that we are plotting seconds
            //this is not always necessary, but it can prevent wrong labeling
            //AxisUnit = 1;

            SetAxisLimits(TimeSpan.FromTicks(0));

            //The next code simulates data changes every 300 ms

            IsReading = false;

            //LineSeries = new SeriesCollection();
            //LineSeries.Add(new LineSeries()
            //{
            //    Title = "主动轮转速",
            //    PointGeometry = DefaultGeometries.Circle,
            //    LineSmoothness = 1,
            //    StrokeThickness = 2,
            //    Stroke = Brushes.Black,
            //    Fill = Brushes.Transparent,
            //    ScalesYAt = 0
            //});

            //LineSeries.Add(new LineSeries()
            //{
            //    Title = "从动轮转速",
            //    PointGeometry = DefaultGeometries.Circle,
            //    LineSmoothness = 1,
            //    StrokeThickness = 2,
            //    Stroke = Brushes.Blue,
            //    Fill = Brushes.Transparent,
            //    ScalesYAt = 1
            //});
            //LineSeries.Add(new LineSeries()
            //{
            //    Title = "带轮转速",
            //    PointGeometry = DefaultGeometries.Circle,
            //    LineSmoothness = 1,
            //    StrokeThickness = 2,
            //    Stroke = Brushes.Blue,
            //    Fill = Brushes.Transparent,
            //    ScalesYAt = 2
            //});
            
            
            DataContext = this;
        }

        public ChartValues<ObservablePoint> ChartValues { get; set; }
        public ChartValues<ObservablePoint> ChartValuesZDL { get; set; }
        public ChartValues<ObservablePoint> ChartValuesCDL { get; set; }
        public ChartValues<ObservablePoint> ChartValuesDL { get; set; }

        //public Func<double, string> DateTimeFormatter { get; set; }

        //private SeriesCollection _LineSeries;

        //public SeriesCollection LineSeries
        //{
        //    get { return _LineSeries; }
        //    set
        //    {
        //        _LineSeries = value;
        //        OnPropertyChanged("LineSeries");
        //    }
        //}

        //public double AxisStep { get; set; }
        //public double AxisUnit { get; set; }

        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                //OnPropertyChanged("AxisMax");
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                //OnPropertyChanged("AxisMin");
            }
        }

        public bool IsReading { get; set; }

        private void Read()
        {
            var r = new Random();
            

            while (IsReading)
            {
                Thread.Sleep(100);
                var now = DateTime.Now - startTime;

                ChartValues.Add(new ObservablePoint(now.TotalSeconds, r.Next(0, 100)));
                ChartValuesZDL.Add(new ObservablePoint(now.TotalSeconds, Math.Sin(Math.PI* now.TotalSeconds/10)));
                ChartValuesCDL.Add(new ObservablePoint(now.TotalSeconds, Math.Cos(Math.PI * now.TotalSeconds / 10)));
                ChartValuesDL.Add(new ObservablePoint(now.TotalSeconds, Math.Tan(Math.PI * now.TotalSeconds / 10)));

                //LineSeries[0].Values.Add(new ObservablePoint(now.TotalSeconds, r.Next(0, 100)));
                //LineSeries[1].Values.Add(new ObservablePoint(now.TotalSeconds, r.Next(0, 1000)));
                //LineSeries[2].Values.Add(new ObservablePoint(now.TotalSeconds, r.Next(0, 10)));

                SetAxisLimits(now);

                if (ChartValuesZDL.Count() > 500)
                {
                    ChartValues.RemoveAt(0);
                    ChartValuesZDL.RemoveAt(0);
                    ChartValuesCDL.RemoveAt(0);
                    ChartValuesDL.RemoveAt(0);
                }
                Dispatcher.Invoke(new Action(() => {
                    lvcChartValuesZDL.Values = ChartValuesZDL;
                    lvcChartValuesCDL.Values = ChartValuesCDL;
                    lvcChartValuesDL.Values = ChartValuesDL;
                    axisX.MaxValue = AxisMax;
                    axisX.MinValue = AxisMin;
                }));
            }
        }

        private void SetAxisLimits(TimeSpan now)
        {
            AxisMax = now.TotalSeconds + 1; // lets force the axis to be 1 second ahead
            AxisMin = now.TotalSeconds - 80; // and 8 seconds behind
            if (AxisMin < 0)
            {
                AxisMin = 0;
            }
        }

        private void InjectStopOnClick(object sender, RoutedEventArgs e)
        {

            IsReading = !IsReading;
            startTime = DateTime.Now;
            if (IsReading) Task.Factory.StartNew(Read);
        }
    }


}
