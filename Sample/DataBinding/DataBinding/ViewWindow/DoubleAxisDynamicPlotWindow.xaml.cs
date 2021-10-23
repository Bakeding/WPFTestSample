using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataBinding.ViewWindow
{
    /// <summary>
    /// DoubleAxisDynamicPlotWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DoubleAxisDynamicPlotWindow : Window,INotifyPropertyChanged
    {
        #region INotifyPropertyChanged 函数实现

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private ObservableCollection<KeyValuePair<double, double>> _LineZDLSpeed;
        public ObservableCollection<KeyValuePair<double, double>> LineZDLSpeed
        {
            get { return _LineZDLSpeed; }
            set
            {
                _LineZDLSpeed = value;
                OnPropertyChanged("LineZDLSpeed");
            }
        }

        private ObservableCollection<KeyValuePair<double, double>> _LineCDLSpeed;
        public ObservableCollection<KeyValuePair<double, double>> LineCDLSpeed
        {
            get { return _LineCDLSpeed; }
            set
            {
                _LineCDLSpeed = value;
                OnPropertyChanged("LineCDLSpeed");
            }
        }

        private ObservableCollection<KeyValuePair<double, double>> _LineDLSpeed;
        public ObservableCollection<KeyValuePair<double, double>> LineDLSpeed
        {
            get { return _LineDLSpeed; }
            set
            {
                _LineDLSpeed = value;
                OnPropertyChanged("LineDLSpeed");
            }
        }
        private ObservableCollection<KeyValuePair<double, double>> _LineZDLTorque;
        public ObservableCollection<KeyValuePair<double, double>> LineZDLTorque
        {
            get { return _LineZDLTorque; }
            set
            {
                _LineZDLTorque = value;
                OnPropertyChanged("LineZDLTorque");
            }
        }
        private ObservableCollection<KeyValuePair<double, double>> _LineCDLTorque;
        public ObservableCollection<KeyValuePair<double, double>> LineCDLTorque
        {
            get { return _LineCDLTorque; }
            set
            {
                _LineCDLTorque = value;
                OnPropertyChanged("LineCDLTorque");
            }
        }
        private ObservableCollection<KeyValuePair<double, double>> _LineRate;
        public ObservableCollection<KeyValuePair<double, double>> LineRate
        {
            get { return _LineRate; }
            set
            {
                _LineRate = value;
                OnPropertyChanged("LineRate");
            }
        }
        LinearAxis AxisSpeed;
        LinearAxis AxisTorque;
        LinearAxis AxisRate;
        DateTime StartTime = DateTime.Now;
        int linePointNum = 0;

        CancellationTokenSource cts;
        Task DrawTask;

        public DoubleAxisDynamicPlotWindow()
        {
            InitializeComponent();
            LineZDLSpeed = new ObservableCollection<KeyValuePair<double, double>>();
            LineCDLSpeed = new ObservableCollection<KeyValuePair<double, double>>();
            LineDLSpeed = new ObservableCollection<KeyValuePair<double, double>>();
            LineZDLTorque = new ObservableCollection<KeyValuePair<double, double>>();
            LineCDLTorque = new ObservableCollection<KeyValuePair<double, double>>();
            LineRate = new ObservableCollection<KeyValuePair<double, double>>();

            InitialChart();
            DataContext = this;
        }

        private void DrawLine(int DrawInterval, int showPoints)
        {
            StartTime = DateTime.Now;
            while (!cts.IsCancellationRequested)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(DrawInterval));
                var ts = (DateTime.Now - StartTime).TotalSeconds;

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    LineZDLSpeed.Add(new KeyValuePair<double, double>(ts, 100 * Math.Sin(ts * Math.PI / 10)));
                    LineCDLSpeed.Add(new KeyValuePair<double, double>(ts, 100 * Math.Cos(ts * Math.PI / 10)));
                    LineDLSpeed.Add(new KeyValuePair<double, double>(ts + 1, 100 * Math.Sin(ts * Math.PI / 10)));
                    LineZDLTorque.Add(new KeyValuePair<double, double>(ts + 2, 1000 * Math.Sin(ts * Math.PI / 10)));
                    LineCDLTorque.Add(new KeyValuePair<double, double>(ts + 1, 1000 * Math.Cos(ts * Math.PI / 10)));
                    LineRate.Add(new KeyValuePair<double, double>(ts + 3, Math.Sin(ts * Math.PI / 10)));
                    linePointNum++;
                    //tkLineZDLSpeed.ItemsSource = LineZDLSpeed;
                    //tkLineCDLSpeed.ItemsSource = LineCDLSpeed;
                    //tkLineDLSpeed.ItemsSource = LineDLSpeed;
                    //tkLineZDLTorque.ItemsSource = LineZDLTorque;
                    //tkLineCDLTorque.ItemsSource = LineCDLTorque;
                    //tkLineRate.ItemsSource = LineRate;


                    if (linePointNum > showPoints)
                    {
                        LineZDLSpeed.RemoveAt(0);
                        LineCDLSpeed.RemoveAt(0);
                        LineDLSpeed.RemoveAt(0);
                        LineZDLTorque.RemoveAt(0);
                        LineCDLTorque.RemoveAt(0);
                        LineRate.RemoveAt(0);
                        linePointNum--;
                    }
                }));
            }
        }

        private void InitialChart()
        {
            //设置坐标轴
            Style titleStyle = new Style();//坐标轴字体样式
            titleStyle.Setters.Add(new Setter(FontStyleProperty, FontStyles.Normal));

            AxisSpeed = new LinearAxis();
            AxisSpeed.Title = "转速[r/min]";
            AxisSpeed.Location = AxisLocation.Left;
            AxisSpeed.Orientation = AxisOrientation.Y;
            AxisSpeed.Maximum = 110;
            AxisSpeed.Minimum = -110;
            AxisSpeed.FontWeight = FontWeights.Bold;
            AxisSpeed.FontSize = 10;
            AxisSpeed.TitleStyle = titleStyle;
            tkLineZDLSpeed.DependentRangeAxis = AxisSpeed;
            tkLineCDLSpeed.DependentRangeAxis = AxisSpeed;
            tkLineDLSpeed.DependentRangeAxis = AxisSpeed;

            AxisTorque = new LinearAxis();
            AxisTorque.Title = "力矩[Nm]";
            AxisTorque.Location = AxisLocation.Left;
            AxisTorque.Orientation = AxisOrientation.Y;
            AxisTorque.Maximum = 1100;
            AxisTorque.Minimum = -1100;
            AxisTorque.FontWeight = FontWeights.Bold;
            AxisTorque.FontSize = 10;
            AxisTorque.TitleStyle = titleStyle;
            AxisTorque.Foreground = Brushes.Blue;
            tkLineZDLTorque.DependentRangeAxis = AxisTorque;
            tkLineCDLTorque.DependentRangeAxis = AxisTorque;

            AxisRate = new LinearAxis();
            AxisRate.Title = "传动效率";
            AxisRate.Location = AxisLocation.Right;
            AxisRate.Orientation = AxisOrientation.Y;
            AxisRate.Maximum = 1.1;
            AxisRate.Minimum = -1.1;
            AxisRate.FontWeight = FontWeights.Bold;
            AxisRate.FontSize = 10;
            AxisRate.TitleStyle = titleStyle;
            AxisRate.Foreground = Brushes.DeepPink;
            tkLineRate.DependentRangeAxis = AxisRate;

            //设置曲线点样式
            Style PointStyleSpeed = new Style(typeof(DataPoint));
            PointStyleSpeed.Setters.Add(new Setter(LineDataPoint.TemplateProperty, null));
            PointStyleSpeed.Setters.Add(new Setter(DataPoint.BackgroundProperty, Brushes.Black));
            tkLineZDLSpeed.DataPointStyle = PointStyleSpeed;
            tkLineCDLSpeed.DataPointStyle = PointStyleSpeed;
            tkLineDLSpeed.DataPointStyle = PointStyleSpeed;

            Style PointStyleTorque = new Style(typeof(DataPoint));
            PointStyleTorque.Setters.Add(new Setter(LineDataPoint.TemplateProperty, null));
            PointStyleTorque.Setters.Add(new Setter(DataPoint.BackgroundProperty, Brushes.Blue));
            tkLineZDLTorque.DataPointStyle = PointStyleTorque;
            tkLineCDLTorque.DataPointStyle = PointStyleTorque;

            Style PointStyleRate = new Style(typeof(DataPoint));
            PointStyleRate.Setters.Add(new Setter(LineDataPoint.TemplateProperty, null));
            PointStyleRate.Setters.Add(new Setter(DataPoint.BackgroundProperty, Brushes.DeepPink));
            tkLineRate.DataPointStyle = PointStyleRate;

            //设置曲线样式
            Style PolylineStyleCDL = new Style(typeof(Polyline));
            //PolylineStyleCDL.Setters.Add(new Setter(Polyline.StrokeThicknessProperty, 5));
            PolylineStyleCDL.Setters.Add(new Setter(Shape.StrokeDashArrayProperty, new DoubleCollection(new[] { 5.0, 5.0, 2.0 })));
            tkLineCDLSpeed.PolylineStyle = PolylineStyleCDL;
            tkLineCDLTorque.PolylineStyle = PolylineStyleCDL;

            Style PolylineStyleDL = new Style(typeof(Polyline));
            //PolylineStyleDL.Setters.Add(new Setter(Shape.StrokeThicknessProperty, 2));
            PolylineStyleDL.Setters.Add(new Setter(Shape.StrokeDashArrayProperty, new DoubleCollection(new[] { 2.0, 5.0 })));
            tkLineDLSpeed.PolylineStyle = PolylineStyleDL;
            tkLineRate.PolylineStyle = PolylineStyleDL;
        }

        private void ResetLines()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                LineZDLSpeed.Clear();
                LineCDLSpeed.Clear();
                LineDLSpeed.Clear();
                LineZDLTorque.Clear();
                LineCDLTorque.Clear();
                LineRate.Clear();
                linePointNum = 0;
            }));
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            ResetLines();
            cts = new CancellationTokenSource();
            DrawTask = Task.Factory.StartNew(() => DrawLine(200, 1000), cts.Token);
        }
    }
}
