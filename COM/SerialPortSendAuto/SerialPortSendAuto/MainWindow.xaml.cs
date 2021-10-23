using System;
using System.Collections.Generic;
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

namespace SerialPortSendAuto
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isSend = false;
        public SerialPort serialPort;//串口对象类
        bool openFlag = false;
        Random random = new Random();
        Thread threadSendAuto;
        Thread openPortThread;
        int interval = 1000;

        public MainWindow()
        {


            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //openPortThread = new Thread((new ParameterizedThreadStart(NewPort)));
            //openPortThread.IsBackground = true;
            //openPortThread.Start(sendPort);
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {


            if (!isSend && !openFlag)
            {
                isSend = true;
                cbPort.IsEnabled = false;
                cbInterval.IsEnabled = false;
                btnSend.Content = "停止发送数据";

                btnSend.FontWeight = FontWeights.Bold;
                btnSend.FontStyle = FontStyles.Italic;
                btnSend.Foreground = Brushes.Black;
                btnSend.Background = Brushes.Red;

                //if (!openFlag)
                {
                    interval = Convert.ToInt32(Double.Parse(cbInterval.Text) * 1000);

                    openPortThread = new Thread((new ParameterizedThreadStart(NewPort)));
                    openPortThread.IsBackground = true;
                    openPortThread.Start(cbPort.Text);

                    threadSendAuto = new Thread(sendDataAuto);
                    threadSendAuto.IsBackground = true;
                    threadSendAuto.Start();
                }
            }
            else if (isSend && openFlag)
            {
                isSend = false;

                cbPort.IsEnabled = true;
                cbInterval.IsEnabled = true;
                btnSend.Content = "自动发送数据";

                btnSend.FontWeight = FontWeights.Normal;
                btnSend.FontStyle = FontStyles.Normal;
                btnSend.Foreground = Brushes.Green;
                btnSend.Background = Brushes.LightGray;

                //if (openFlag)
                {
                    openFlag = false;
                    interval = Convert.ToInt32(Double.Parse(cbInterval.Text) * 1000);
                    openPortThread.Abort();
                    threadSendAuto.Abort();
                    try
                    {
                        serialPort.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("关闭端口错误");
                        MessageBox.Show("关闭端口错误：" + ex.ToString());
                    } 
                    
                }
            }

        }

        private void NewPort(object portNum)
        {
            openFlag = InitCOM((string)portNum);
        }

        /// 串口接收通信配置方法
        /// 端口名称
        public bool InitCOM(string PortName)
        {
            serialPort = new SerialPort(PortName, 9600, Parity.None, 8, StopBits.One);

            serialPort.RtsEnable = true;
            return OpenPort();//串口打开
        }

        //打开串口的方法
        public bool OpenPort()
        {
            try//这里写成异常处理的形式以免串口打不开程序崩溃
            {
                serialPort.Open();
            }
            catch {
                MessageBox.Show("串口打开错误!");
            }
            if (serialPort.IsOpen)
            {
                return true;
            }
            else
            {
                MessageBox.Show("串口打开失败!");
                openFlag = false;
                return false;
            }
        }

        //向串口发送数据
        public void SendCommand(string CommandString)
        {
            byte[] WriteBuffer = Encoding.Default.GetBytes(CommandString);


            serialPort.Write(WriteBuffer, 0, WriteBuffer.Length);
        }

        private void sendDataAuto()
        {
            //bool isBusy=false;
            int i = 0;
            Random random = new Random();
            string senddataType = "";
            DateTime time = DateTime.Now;
            while (true)
            {
                if (isSend)
                {
                    Thread.Sleep(interval);
                    try
                    {
                        cbCurveType.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            senddataType=cbCurveType.Text;
                            
                        }));
                        if (senddataType!=""&&senddataType!=null)
                        {
                            switch (senddataType)
                            {
                                case "自增":
                                    SendCommand(i++ + "\r\n");
                                    if (i==100)
                                    {
                                        i = 0;
                                    }
                                    break;
                                case "100以内随机数":
                                    SendCommand(random.Next(100) + "\r\n");
                                    break;
                                case "正弦":
                                    TimeSpan ts=DateTime.Now - time;
                                    SendCommand(((float)Math.Sin(ts.TotalMilliseconds*0.001)*50+50) + "\r\n");
                                    break;
                            }
                        }
                        
                        
                    }
                    catch (Exception ex)
                    {
                        //isBusy = true;
                        MessageBox.Show("发送数据错误");
                        //MessageBox.Show("发送数据错误：" + ex.ToString());
                        break;
                    }

                    
                }
               
            }
        }


    }
}
