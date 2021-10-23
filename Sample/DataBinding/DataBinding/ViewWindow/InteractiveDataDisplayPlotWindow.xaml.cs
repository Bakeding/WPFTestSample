using InteractiveDataDisplay.WPF;
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

namespace DataBinding.ViewWindow
{
    /// <summary>
    /// InteractiveDataDisplayPlotWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InteractiveDataDisplayPlotWindow : Window
    {
        public InteractiveDataDisplayPlotWindow()
        {
            InitializeComponent();
            double[] x = new double[200];
            for (int i = 0; i < x.Length; i++)
                x[i] = 3.1415 * i / (x.Length - 1);

            for (int i = 0; i < 3; i++)
            {
                var lg = new LineGraph();
                lines.Children.Add(lg);
                lg.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, (byte)(i * 60), 0));
                lg.Description = String.Format("Data series {0}", i + 1);
                lg.StrokeThickness = 2;
                lg.Plot(x, x.Select(v => Math.Sin(v + i / 10.0)).ToArray());
            }

            double[] x1 = Enumerable.Range(0, 90).Select(i => i / 10.0).ToArray();
            double[] y1 = x1.Select(v => 17 * (Math.Abs(v) < 1e-10 ? 1 : Math.Sin(v) / v) + 1).ToArray();
            lineGraph1.Stroke = new SolidColorBrush(Colors.Green);
            lineGraph1.StrokeThickness = 2.0;
            lineGraph1.Plot(x1, y1);

            double[] y2 = x1.Select(v => 57 * (Math.Abs(v) < 1e-10 ? 1 : Math.Sin(v) / v) + 1).ToArray();
            lineGraph2.Stroke = new SolidColorBrush(Colors.Green);
            lineGraph2.StrokeThickness = 2.0;
            lineGraph2.Plot(x1, y2);

            
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
