using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace OxyplotTest
{
    class PlotViewModelEx
    {
        /// <summary>
        /// 画直线
        /// </summary>
        public PlotModel SimplePlotModel { get; set; }

        //每条线对应一个队列用作实时数据统计
        private ConcurrentQueue<DataPoint> queueDataPoint1 = new ConcurrentQueue<DataPoint>();
        private ConcurrentQueue<DataPoint> queueDataPoint2 = new ConcurrentQueue<DataPoint>();
        private double lastTime1 = DateTimeAxis.ToDouble(DateTime.Now);
        private double lastData1 = 0;
        private double lastTime2 = DateTimeAxis.ToDouble(DateTime.Now);
        private double lastData2 = 0;

        bool openFlag = false;
        public SerialPort serialPort;//串口对象类

        public void addDataPoint1(DataPoint datapoint)
        {

            queueDataPoint1.Enqueue(datapoint);
        }

        public void addDataPoint2(DataPoint datapoint)
        {
            queueDataPoint2.Enqueue(datapoint);
        }

        public DataPoint getDataPoint1()
        {
            DataPoint topDataPoint = new DataPoint(lastTime1, 0);
            queueDataPoint1.TryDequeue(out topDataPoint);
            return topDataPoint;
        }

        public DataPoint getDataPoint2()
        {
            DataPoint topDataPoint = new DataPoint(lastTime2, 0);
            queueDataPoint2.TryDequeue(out topDataPoint);
            return topDataPoint;
        }

        public PlotViewModelEx()
        {

            SimplePlotModel = new PlotModel();
            PlotChart();
            readData();
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

            string readData= serialPort.ReadLine();
            lastTime1=DateTimeAxis.ToDouble(DateTime.Now);
            lastData1=Convert.ToDouble(readData.Trim());
            addDataPoint1(new DataPoint(lastTime1, lastData1));
            addDataPoint2(new DataPoint(lastTime1, lastData1/2));
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

        /// <summary>
        /// 绘图
        /// </summary>
        private void PlotChart()
        {
            //创建于建立初始化数据节点
            var lineDataPoint1 = new LineSeries() { Title = "DataPoint1" };
            lineDataPoint1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), 0));
            SimplePlotModel.Series.Add(lineDataPoint1);

            var lineDataPoint2 = new LineSeries() { Title = "DataPoint2" };
            lineDataPoint2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), 0));
            SimplePlotModel.Series.Add(lineDataPoint2);

            //定义y轴
            LinearAxis leftAxis = new LinearAxis()
            {

                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 100,
                Title = "笔数",//显示标题内容
                TitlePosition = 0,//显示标题位置
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
            };

            //定义x轴 报盘监控界面x轴统一为时间
            DateTimeAxis bottomAxis = new DateTimeAxis()
            {

                Position = AxisPosition.Bottom,
                StringFormat = "hh:mm:ss",
                Minimum = DateTimeAxis.ToDouble(DateTime.Now),
                Maximum = DateTimeAxis.ToDouble(DateTime.Now.AddMinutes(1)),
                Title = "时间",
                TitlePosition = 0,
                IntervalLength = 60,
                MinorIntervalType = DateTimeIntervalType.Seconds,
                IntervalType = DateTimeIntervalType.Seconds,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
            };

            SimplePlotModel.Axes.Add(leftAxis);
            SimplePlotModel.Axes.Add(bottomAxis);

            bool bToMove = false;
            Task.Factory.StartNew(() =>
            {

                while (true)
                {

                    lineDataPoint1.Points.Add(getDataPoint1());
                    lineDataPoint2.Points.Add(getDataPoint2());

                    if (!bToMove)
                    {

                        //当前时间减去起始时间达到30秒后开始左移时间轴
                        TimeSpan timeSpan = DateTimeAxis.ToDateTime(lastTime1) - DateTimeAxis.ToDateTime(bottomAxis.Minimum);
                        if (timeSpan.TotalSeconds >= 30)
                        {
                            bToMove = true;
                        }
                    }
                    else
                    {

                        //左移时间轴，跨度维持在60秒
                        bottomAxis.Minimum = DateTimeAxis.ToDouble(DateTime.Now.AddSeconds(-30));
                        bottomAxis.Maximum = DateTimeAxis.ToDouble(DateTime.Now.AddSeconds(30));

                        //删除历史节点，防止DataPoint过多影响效率，也防止出现内存泄漏                        
                        lineDataPoint1.Points.RemoveAt(0);
                        lineDataPoint2.Points.RemoveAt(0);
                    }

                    //根据报单笔数判断是否需要更新y轴刻度                    

                    //首先找出四条统计线中当前最大的节点
                    double iMax = lineDataPoint1.MaxY;
                    if (iMax < lineDataPoint2.MaxY)
                    {
                        iMax = lineDataPoint2.MaxY;
                    }

                    //如果当前的y轴最大刻度小于数据集中的最大值，放大
                    leftAxis.Maximum = iMax + (100 - iMax % 100);
                    leftAxis.IntervalLength = leftAxis.Maximum / 5;

                    //每秒刷新一次视图                                       
                    SimplePlotModel.InvalidatePlot(true);
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
