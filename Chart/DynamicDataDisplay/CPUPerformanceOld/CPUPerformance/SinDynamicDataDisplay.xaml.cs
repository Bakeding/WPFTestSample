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

namespace CPUPerformance
{
    /// <summary>
    /// SinDynamicDataDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class SinDynamicDataDisplay : Window
    {
        public SinDynamicDataDisplay()
		{
			InitializeComponent();
			Loaded += new RoutedEventHandler(MainWindow_Loaded);
		}
 
		void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			// Prepare data in arrays
			const int N = 1000;
			double[] x = new double[N];
			double[] y = new double[N];
 
			for (int i = 0; i < N; i++)
			{
				x[i] = i * 0.1;
				y[i] = Math.Sin(x[i]);
			}
 
			// Create data sources:
			var xDataSource = x.AsXDataSource();
			var yDataSource = y.AsYDataSource();
 
			CompositeDataSource compositeDataSource = xDataSource.Join(yDataSource);
			// adding graph to plotter
            
			plotter.AddLineGraph(compositeDataSource,Colors.Goldenrod,3,"Sine");
 
			// Force evertyhing plotted to be visible
			plotter.FitToView();
		}
    }
}
