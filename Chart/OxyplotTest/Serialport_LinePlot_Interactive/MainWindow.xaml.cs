using InteractiveDataDisplay.WPF;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
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
using System.Windows.Threading;

namespace Serialport_LinePlot_Interactive
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        LineGraph lg, lg2;
        DispatcherTimer dTimer;
        ConcurrentQueue<double> qXt = new ConcurrentQueue<double>();
        ConcurrentQueue<double> qYt1 = new ConcurrentQueue<double>();
        ConcurrentQueue<double> qYt2 = new ConcurrentQueue<double>();
        int qlimit = 100;
        DateTime startTime = DateTime.Now;
        double xAxisMax = 10;
        double yAxisMax = 10;
        double lastTime = 0;
        double lastData1 = 0;
        double lastData2 = 0;

        bool openFlag = false;
        public SerialPort serialPort;//串口对象类

        public MainWindow()
        {
            InitializeComponent();
            plotter.PlotWidth = xAxisMax;
            plotter.PlotHeight = yAxisMax;
            DisplayChartTimer();
        }

        /// <summary>
        /// 定时器绘图Timer
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

            qXt = LimitedQueue(0, qXt, qlimit);
            qYt1 = LimitedQueue(0, qYt1, qlimit);
            qYt2 = LimitedQueue(0, qYt2, qlimit);

            //Thread.Sleep(3000);

            dTimer = new DispatcherTimer();
            dTimer.Interval = TimeSpan.FromMilliseconds(100);
            dTimer.Tick += new EventHandler(UpdateChartTimer);
            dTimer.Start();

            readData();
        }

        private void UpdateChartTimer(object sender, EventArgs e)
        {
            qXt = LimitedQueue(lastTime, qXt, qlimit);
            qYt1 = LimitedQueue(lastData1, qYt1, qlimit);
            qYt2 = LimitedQueue(lastData2, qYt2, qlimit);
            lg.Plot(qXt, qYt1);
            lg2.Plot(qXt, qYt2);
        }

        /// <summary>
        /// 读取串口数据
        /// </summary>
        private void readData()
        {
            Thread receiveThread = new Thread(new ParameterizedThreadStart(NewPort));
            receiveThread.IsBackground = true;
            receiveThread.Start("COM1");
        }
        private void NewPort(object portNum)
        {
            openFlag = InitCOM((string)portNum);
        }
        public bool InitCOM(string PortName)
        {
            serialPort = new SerialPort(PortName, 9600, Parity.None, 8, StopBits.One);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);//DataReceived事件委托
            serialPort.ReceivedBytesThreshold = 1;
            serialPort.RtsEnable = true;
            return OpenPort();//串口打开
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string readData = serialPort.ReadLine().Trim();

            TimeSpan ts = DateTime.Now - startTime;
            lastTime = Convert.ToDouble(ts.TotalMilliseconds) / 1000;
            lastData1 = Convert.ToDouble(readData)-50;
            lastData2 = lastData1 / 2;
            try
            {
                plotter.Dispatcher.Invoke(new Action(() =>
                {
                    if (lastTime > xAxisMax)
                    {
                        plotter.PlotOriginX = lastTime - 10;
                        xAxisMax = lastTime;
                    }

                    if (lastData1 > yAxisMax)
                    {
                        plotter.PlotHeight = lastData1;
                        yAxisMax = lastData1;
                    }
                    if (lastData2 > yAxisMax)
                    {
                        plotter.PlotHeight = lastData2;
                        yAxisMax = lastData2;
                    }
                        
                    
                }));
            }
            catch (Exception) { }


            //Debug.WriteLine(lastTime1 + ":" + lastData1);
        }

        public bool OpenPort()
        {
            try//这里写成异常处理的形式以免串口打不开程序崩溃
            {
                serialPort.Open();
            }
            catch { }
            if (serialPort.IsOpen)
            {
                return true;
            }
            else
            {
                //MessageBox.Show("串口打开失败!");
                openFlag = false;
                return false;
            }
        }

        private ConcurrentQueue<double> LimitedQueue(double x, ConcurrentQueue<double> qx, int limit)
        {
            double outdata = 0;
            if (qx.Count >= limit)
                qx.TryDequeue(out outdata);
            qx.Enqueue(x);
            return qx;
        }
    }


}
