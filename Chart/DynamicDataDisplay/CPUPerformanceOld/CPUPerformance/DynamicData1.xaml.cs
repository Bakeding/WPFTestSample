using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
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
using System.Windows.Threading;

namespace CPUPerformance
{
    /// <summary>
    /// DynamicData1.xaml 的交互逻辑
    /// </summary>
    public partial class DynamicData1 : Window
    {
        public DynamicData1()
        {
            InitializeComponent();
             // Loaded += new RoutedEventHandler(Window_Loaded);
        }

        Random random = new Random();
        private DispatcherTimer timer = new DispatcherTimer();
        CompositeDataSource compositeDataSource1;
        CompositeDataSource compositeDataSource2;
 
        EnumerableDataSource<DateTime> datesDataSource = null;
        EnumerableDataSource<int> numberOpenDataSource=null;
        EnumerableDataSource<int> numberClosedDataSource = null;
 
        List<DateTime> vardatetime = new List<DateTime>(); 
        int i = 0;
 
        List<int> numberOpen = new List<int>();
        List<int> numberClosed = new List<int>();
        /*
        int[] numberOpen = new int[100];
        int[] numberClosed = new int[100];
        */
        

        private void Window1_Loaded(object sender, EventArgs e)
        {
 
            DateTime tempDateTime = new DateTime();
            tempDateTime = DateTime.Now;
 
            vardatetime.Add(tempDateTime);
 
            numberOpen.Add(random.Next(40));
            numberClosed.Add(random.Next(100));
            
 
 
            datesDataSource.RaiseDataChanged();
            numberOpenDataSource.RaiseDataChanged();
            numberClosedDataSource.RaiseDataChanged();
 
        
        
         i++;
 
      } // Window1_Loaded() 
        private void DynamicData1_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
           
            DateTime tempDateTime=new DateTime();
            
                tempDateTime = DateTime.Now;
                vardatetime.Add(tempDateTime);
               
                numberOpen.Add(random.Next(40));
                numberClosed.Add(random.Next(100));
               
            i++;
 
            datesDataSource = new EnumerableDataSource<DateTime>(vardatetime);
            datesDataSource.SetXMapping(x => dateAxis.ConvertToDouble(x));
 
             numberOpenDataSource = new EnumerableDataSource<int>(numberOpen);
            numberOpenDataSource.SetYMapping(y => y);
 
            numberClosedDataSource = new EnumerableDataSource<int>(numberClosed);
            numberClosedDataSource.SetYMapping(y => y);
 
            compositeDataSource1 = new CompositeDataSource(datesDataSource, numberOpenDataSource);
            compositeDataSource2 = new CompositeDataSource(datesDataSource, numberClosedDataSource);


            plotter.AddLineGraph(compositeDataSource1, Colors.Red, 1, "Percentage2");
            plotter.AddLineGraph(compositeDataSource2, Colors.Green, 1, "Percentage2");
            plotter.Viewport.FitToView();
            
            
            timer.Interval = TimeSpan.FromSeconds(1/100);
            timer.Tick += new EventHandler(Window1_Loaded);
            timer.IsEnabled = true;
            
           
           
        }

        private void plotter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ChartPlotter chart = sender as ChartPlotter;
            //Point p = e.GetPosition(this).ScreenToData(chart.Transform);
            //MessageBox.Show(p.X + ":" + p.Y);
            
        }

        private void plotter_MouseMove_1(object sender, MouseEventArgs e)
        {
            ChartPlotter chart = sender as ChartPlotter;
            Point p = e.GetPosition(this).ScreenToData(chart.Transform);
            px.Text = p.X.ToString();
            py.Text = p.Y.ToString();
        }


    }
}
