using InteractiveDataDisplay.WPF;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using System.IO.Ports;

namespace LineCurveTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LineGraph lg,lg2;
        DispatcherTimer dTimer;
        Queue<double> qXt = new Queue<double>();
        Queue<double> qYt1 = new Queue<double>();
        Queue<double> qYt2 = new Queue<double>();
        double xnum = 0;

        

        public MainWindow()
        {
            InitializeComponent();
            //DisplayChartThread();
            DisplayChartTimer();

            //plotter.BringIntoView(new Rect(0, 0, 5, 5));
        }

        /// <summary>
        /// 定时器绘图Timer'
        /// </summary>
        private void DisplayChartTimer()
        {
            lg = new LineGraph();
            lines.Children.Add(lg);
            lg.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            lg.Description = String.Format("Data series {0}", 1);
            lg.StrokeThickness = 2;

            lg2 = new LineGraph();
            lines.Children.Add(lg2);
            lg2.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, (byte)(150), 0));
            lg2.Description = String.Format("Data series {0}", 2);
            lg2.StrokeThickness = 2;

            qXt = LimitedQueue(0, qXt, 100);
            qYt1 = LimitedQueue(0, qYt1, 100);
            qYt2 = LimitedQueue(0 , qYt2, 100);

            Thread thread = new Thread(readData);
            thread.IsBackground = true;
            thread.Start();

            Thread.Sleep(3000);

            dTimer = new DispatcherTimer();
            dTimer.Interval = TimeSpan.FromMilliseconds(100);
            dTimer.Tick += new EventHandler(UpdateChartTimer);
            dTimer.Start();
        }

        private void UpdateChartTimer(object sender, EventArgs e)
        {
            //qXt.Enqueue(xnum);
            //qYt1.Enqueue(xnum);
            //qYt2.Enqueue(xnum / 2);
            qXt= LimitedQueue(xnum, qXt, 100);
            qYt1= LimitedQueue(xnum, qYt1, 100);
            qYt2= LimitedQueue(xnum / 2, qYt2, 100);
            lg.Plot(qXt, qYt1);
            lg2.Plot(qXt, qYt2);
        }

        private void readData()
        {
            while (true)
            {
                Thread.Sleep(100);
                xnum++;
                
            }
        }

       

        /// <summary>
        /// 线程绘图
        /// </summary>
        private void DisplayChartThread()
        {
            int i = 0;
            lg = new LineGraph();
            lines.Children.Add(lg);
            lg.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, (byte)(i * 10), 0));
            lg.Description = String.Format("Data series {0}", i + 1);
            lg.StrokeThickness = 2;

            lg2 = new LineGraph();
            lines.Children.Add(lg2);
            lg2.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, (byte)(150), 0));
            lg2.Description = String.Format("Data series {0}", 2);
            lg2.StrokeThickness = 2;

            Thread thread = new Thread(new ParameterizedThreadStart(UpdateChartThread));
            thread.IsBackground = true;
            thread.Start(i);

            //lg.Plot(x, x.Select(v => Math.Sin(v + i / 10.0)).ToArray());
        }

        private void UpdateChartThread(object obj)
        {
            double xAxis = 0;
            double offset = Convert.ToDouble(obj);
            //List<double> lX = new List<double>();
            //List<double> lY = new List<double>();
            Queue<double> qX = new Queue<double>();
            Queue<double> qY1 = new Queue<double>();
            Queue<double> qY2 = new Queue<double>();

            while (true)
            {
                xAxis += 3.14 / 10;
                Thread.Sleep(100);
                //lX.Add(xAxis);
                //lY.Add(Math.Sin(xAxis));
                //qX.Enqueue(xAxis);
                //qY.Enqueue(Math.Sin(xAxis));
                qX = LimitedQueue(xAxis, qX, 100);
                qY1 = LimitedQueue(xAxis , qY1, 100);
                qY2 = LimitedQueue(xAxis*2, qY2, 100);

                Dispatcher.BeginInvoke(new Action(delegate
                {
                    //lg.Plot(lX, lY);
                    lg.Plot(qX, qY1);
                    lg2.Plot(qX, qY2);
                }
                ));

            }
        }

        private Queue<double> LimitedQueue(double x, Queue<double> qx, int limit)
        {
            if (qx.Count >= limit)
                qx.Dequeue();
            qx.Enqueue(x);
            return qx;
        }
    }

    public class VisibilityToCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((Visibility)value) == Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
