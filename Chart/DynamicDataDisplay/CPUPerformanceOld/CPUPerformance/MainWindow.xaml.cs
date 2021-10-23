using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Common;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;

namespace CPUPerformance
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 参数
        private ObservableDataSource<Point> dataSource = new ObservableDataSource<Point>();//曲线数据源
        private ObservableDataSource<Point> dataSource2 = new ObservableDataSource<Point>();
        private PerformanceCounter cpuPerformance = new PerformanceCounter();

        private List<ObservableDataSource<Point>> dataSourceList = new List<ObservableDataSource<Point>>();

        private DispatcherTimer timer = new DispatcherTimer();
        private int xAaxievalue_base = 0;
        Random random = new Random();
        LineGraph plot1 = new LineGraph();
        LineGraph plot2 = new LineGraph();
        bool scrollflag = true;//是否滚屏

        double xaxis = 0;//x轴显示范围
        double yaxis = 0;//y轴显示范围
        bool isStopScroll = false;//是否停止滚屏
        double interval = 10;//默认屏幕显示宽度
        double xShowSpan = 0.1;//默认x步进距离
        double intervalNoScroll = 0;//是否滚屏显示曲线
        double yOverShowValue = 1.1;//y轴曲线屏幕显示超出y值最大值比率
        Queue<Color> colorQueue = new Queue<Color>();//曲线颜色
        int lineStroke = 3;//设置曲线宽度
        DateTime datetime;


        Queue q = new Queue();
        #endregion

        public MainWindow()
        {
            
            InitializeComponent();
        }

        #region 事件
        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            colorQueue.Enqueue(Colors.Blue);
            colorQueue.Enqueue(Colors.Black);
            colorQueue.Enqueue(Colors.Red);
            colorQueue.Enqueue(Colors.Green);
            colorQueue.Enqueue(Colors.YellowGreen);
            colorQueue.Enqueue(Colors.Brown);
            colorQueue.Enqueue(Colors.BlueViolet);
            colorQueue.Enqueue(Colors.Cyan);
            colorQueue.Enqueue(Colors.DarkGreen);
            colorQueue.Enqueue(Colors.Orange);
            btnSave.IsEnabled = false;
            datetime = DateTime.Now;
            DrawLine();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plotter_MouseMove(object sender, MouseEventArgs e)
        {
            ChartPlotter chart = sender as ChartPlotter;
            Point p = e.GetPosition(this).ScreenToData(chart.Transform);
            px.Text = p.X.ToString();
            py.Text = p.Y.ToString();
        }

        /// <summary>
        /// 清除曲线，重新开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            timer.IsEnabled = false;                          //先FALSE一下，保证同一时刻只有一个TIMER运行。
            //plotter.Children.RemoveAll(typeof(LineGraph));
            plotter.Children.Remove(plot1);
            plotter.Children.Remove(plot2);

            dataSource = new ObservableDataSource<Point>();   //重点在这里，重新实例化一个 
            dataSource2 = new ObservableDataSource<Point>();
            //xaxis = xaxis - interval; yaxis = 0;
            //plotter.Viewport.Visible = new System.Windows.Rect(xaxis, -yaxis, interval, 2 * yaxis);//主要注意这里一行  
            DrawLine();
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled == false)
            {
                IPointEnumerator ipe2 = dataSource2.GetEnumerator(plotter);
                Point p = new Point();
                ipe2.GetCurrent(ref p);
                string str = "x:" + p.X + "; y:" + p.Y + "\n";
                while (ipe2.MoveNext())
                {
                    ipe2.GetCurrent(ref p);
                    str = str + "x:" + p.X + "; y:" + p.Y + "\n";
                }
                Debug.Write("{0}\n", str.ToString());
            }
            else
            {

            }

        }

        /// <summary>
        /// 增加曲线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ObservableDataSource<Point> dataSourceNew = GetNewCurveDataSource();//增加曲线数据源
            dataSourceList.Add(GetNewCurveDataSource());
            DrawLine();

        }

        /// <summary>
        /// 停止曲线显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled == false)
            {
                timer.IsEnabled = true;
                btnSave.IsEnabled = false;
            }
            else
            {
                timer.IsEnabled = false;
                btnSave.IsEnabled = true;
            }

        }

        /// <summary>
        /// 是否滚屏显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScroll_Click(object sender, RoutedEventArgs e)
        {
            foreach (double c in q)
                if (c > yaxis)
                    yaxis = c;
            plotter.Viewport.Visible = new System.Windows.Rect(xaxis, -yaxis * yOverShowValue, interval, 2 * yaxis * yOverShowValue);//主要注意这里一行  

            if (scrollflag)
            {
                //screenShowNoScreenEveryStep = 0;
                scrollflag = false;
            }
            else
            {
                scrollflag = true;
            }
        }

        /// <summary>
        /// 显示范围改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataShowinterval_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                string intervalText = dataShowinterval.Text;
                if (IsNumber(intervalText))
                {
                    interval = Double.Parse(dataShowinterval.Text);
                    DrawLine();
                }
                else
                {
                    dataShowinterval.Text = "";
                }
                return;
            }
            
        }
        #endregion


        #region 方法

        private Color SetLineColor()
        {
            if (colorQueue.Count()>0)
            {
                Color color = (Color)colorQueue.Dequeue();
                colorQueue.Enqueue(color);
                return color;
            }
            else
            {
                return Colors.Red;
            }
        }

        /// <summary>
        /// 曲线绘制方法
        /// </summary>
        private void DrawLine()
        {
            plotter.Children.RemoveAll(typeof(LineGraph));
            plot1 = plotter.AddLineGraph(dataSource, SetLineColor(), lineStroke, "百分比");
            plot2 = plotter.AddLineGraph(dataSource2, SetLineColor(), lineStroke, "百分比2");


            foreach (ObservableDataSource<Point> item in dataSourceList)
            {
                plotter.AddLineGraph(item, SetLineColor(), lineStroke, "百分比3");
            }

            plotter.LegendVisible = true;
            timer.Interval = TimeSpan.FromSeconds(xShowSpan);
            timer.Tick += new EventHandler(AnimatedPlot);
            timer.IsEnabled = true;
            //plotter.Viewport.FitToView();



        }

        /// <summary>
        /// 获取曲线数据源
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private ObservableDataSource<Point> GetNewCurveDataSource()
        {
            ObservableDataSource<Point> ods = new ObservableDataSource<Point>();
            return ods;
        }

        /// <summary>
        /// 获取曲线点数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Point GetNewPoint(double x, double y)
        {
            Point point = new Point(x, y);
            return point;
        }

        /// <summary>
        /// 动态绘图方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimatedPlot(object sender, EventArgs e)
        {
            TimeSpan timespan=DateTime.Now-datetime;
            
            cpuPerformance.CategoryName = "Processor";
            cpuPerformance.CounterName = "% Processor Time";
            cpuPerformance.InstanceName = "_Total";

            //double xAaxievalue = xShowSpan * xAaxievalue_base;
            double xAaxievalue =timespan.TotalMilliseconds/1000;

            Debug.Write(timespan.ToString() + "---" +xAaxievalue+ "\n");

            double yAaxievalue = cpuPerformance.NextValue();
            dataSource.AppendAsync(base.Dispatcher, GetNewPoint(xAaxievalue, yAaxievalue));

            double yAaxievalue2 = 100 * Math.Sin(xAaxievalue);
            dataSource2.AppendAsync(base.Dispatcher, GetNewPoint(xAaxievalue, yAaxievalue2));

            fitScreen(xAaxievalue, yAaxievalue);
            fitScreen(xAaxievalue, yAaxievalue2);

            foreach (ObservableDataSource<Point> item in dataSourceList)
            {
                double yAaxievalueNew = random.Next(100) * Math.Sin(xAaxievalue);
                item.AppendAsync(base.Dispatcher, GetNewPoint(xAaxievalue, yAaxievalueNew));
                fitScreen(xAaxievalue, yAaxievalueNew);

            }

            cpuUsageText.Text = String.Format("{0:0}%", yAaxievalue);
            xAaxievalue_base++;
        }

        /// <summary>
        /// 数据显示窗口设置
        /// </summary>
        /// <param name="xAaxievalue"></param>
        /// <param name="yAaxievalue"></param>
        private void fitScreen(double xAaxievalue, double yAaxievalue)
        {
            if (scrollflag)
            {

                if (q.Count < interval)
                {
                    q.Enqueue((double)yAaxievalue);//入队  
                    if (Math.Abs(yAaxievalue) > Math.Abs(yaxis))
                    {
                        yaxis = Math.Abs(yAaxievalue);
                        //yaxisMax = Math.Abs(yAaxievalue);
                    }
                }
                else
                {
                    if ((double)q.Dequeue() > yaxis)//出队 
                    {

                    }
                    q.Enqueue((double)yAaxievalue);//入队 
                    if (Math.Abs(yAaxievalue) > Math.Abs(yaxis))
                    {
                        yaxis = Math.Abs(yAaxievalue);
                        //yaxisMax = Math.Abs(yAaxievalue);
                    }
                }

                if (xAaxievalue - interval > 0)
                    xaxis = xAaxievalue - interval;
                else
                    xaxis = 0;

                //Debug.Write("{0}\n", yaxis.ToString());
                isStopScroll = false;
                plotter.Viewport.Visible = new System.Windows.Rect(xaxis, -yaxis * yOverShowValue, interval, 2 * yaxis * yOverShowValue);//主要注意这里一行  
            }
            else
            {
                if (q.Count >= interval)
                {
                    intervalNoScroll = interval;

                    if (xAaxievalue - interval > 0)
                    {
                        if (!isStopScroll)
                        {
                            isStopScroll = true;
                            xaxis = xAaxievalue - interval;
                        }
                    }
                    else
                        xaxis = 0;

                    if (xAaxievalue - interval < 0)
                    {
                        intervalNoScroll = xAaxievalue;
                    }
                    else
                    {
                        //screenShowNoScreenEveryStep++;
                        //intervalNoScroll = interval + xShowSpan * screenShowNoScreenEveryStep;
                        intervalNoScroll = xAaxievalue - xaxis;

                    }
                    plotter.Viewport.Visible = new System.Windows.Rect(xaxis, -yaxis * yOverShowValue, intervalNoScroll, 2 * yaxis * yOverShowValue);//主要注意这里一行  
                }

            }
        }



        /// <summary>
        /// 判断字符串是否为数字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumber(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            const string pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(s);
        }
        #endregion

        
        

    }
}
