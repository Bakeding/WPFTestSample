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

namespace WPFDrawPlot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //线程中更新曲线
            Thread threadTmp = new Thread(UpdateChart);
            threadTmp.IsBackground = true;
            threadTmp.Start();
        }

        private void UpdateChart()
        {
            int nPointNum = 100;
            Random randm = new Random();
            double[] dX = new double[nPointNum];
            double[] dY = new double[nPointNum];

            while (true)
            {
                Thread.Sleep(1000);//每秒刷新一次
               
                for (int n = 0; n < dX.Length; n++)
                {
                    dX[n] = n;
                    dY[n] = randm.Next(dX.Length);
                }

                //更新UI
                Dispatcher.Invoke(new Action(delegate
                {
                    this.LineChart.Plot(dX, dY);
                }));
            }
        }
    }
}
