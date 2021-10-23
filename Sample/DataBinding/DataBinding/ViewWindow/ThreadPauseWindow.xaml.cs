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
using System.Windows.Shapes;

namespace DataBinding.ViewWindow
{
    /// <summary>
    /// ThreadPauseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ThreadPauseWindow : Window
    {
        static public AutoResetEvent TaskEvent = new AutoResetEvent(false);
        static public ManualResetEvent ManualTaskEvent = new ManualResetEvent(false);
        private bool bWhileFlag = true;
        int i = 0;
        public ThreadPauseWindow()
        {
            InitializeComponent();
            Thread thread = new Thread(ChangeText);
            thread.IsBackground = true;
            thread.Start();

            Task task = new Task(()=>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    tbTime.Dispatcher.Invoke(new Action(() => {
                        tbTime.Text = DateTime.Now.ToString("T");
                    }));
                }
            });
            task.Start();
            task.ContinueWith(x =>
            {
                Console.WriteLine("当父任务执行完毕时,CLR会唤起一个新线程,将父任务的返回值(子任务的返回值)输出,所以这里不会有任何的线程发生阻塞");
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bWhileFlag = true;
            //TaskEvent.Set();
            ManualTaskEvent.Set();
        }

        private void ChangeText()
        {

            while (true)
            {


                this.ShowText.Dispatcher.Invoke(new Action(delegate
                {
                    ShowText.Text += (++i).ToString();
                    ShowText.Text += " ";
                }));

                Thread.Sleep(500);
                //if (!bWhileFlag)
                {
                    //TaskEvent.WaitOne();
                    ManualTaskEvent.WaitOne();
                }

            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //bWhileFlag = false;
            //TaskEvent.Reset();
            ManualTaskEvent.Reset();
        }
    }
}
