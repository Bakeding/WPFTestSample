using System;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using WpfChart.ChartModel;

namespace WpfChart
{
    [Export(typeof(IScreen)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ChartViewModel :Screen
    {
        [ImportingConstructor]
         public ChartViewModel()
        {
            DisplayName = "02. Chart";          
        }
        
        private ChartView view;
        private ChartStyle cs;
        
        private void SetChartStyle()
        {
            view = this.GetView() as ChartView;
            view.chartCanvas.Children.Clear();
            view.textCanvas.Children.RemoveRange(1, view.textCanvas.Children.Count - 1);
            cs = new ChartStyle();
            cs.ChartCanvas = view.chartCanvas;
            cs.TextCanvas = view.textCanvas;
            cs.Title = "Sine and Cosine Chart";
            cs.Xmin = 0;
            cs.Xmax = 7;
            cs.Ymin = -1.5;
            cs.Ymax = 1.5;
            cs.YTick = 0.5;
            cs.GridlinePattern = LinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Green;
            cs.AddChartStyle(view.tbTitle, view.tbXLabel, view.tbYLabel);

        }

        public void AddChart()
        {
            SetChartStyle();

            BindableCollection<LineSeries> dc = new BindableCollection<LineSeries>();
            var ds = new LineSeries();
            ds.LineColor = Brushes.Blue;
            ds.LineThickness = 2;
            ds.LinePattern = LinePatternEnum.Solid;
            for (int i = 0; i < 50; i++)
            {
                double x = i / 5.0;
                double y = Math.Sin(x);
                ds.LinePoints.Add(new Point(x, y));
            }
            dc.Add(ds);

            ds = new LineSeries();
            ds.LineColor = Brushes.Red;
            ds.LineThickness = 2;
            ds.LinePattern = LinePatternEnum.Dash;
            ds.SetLinePattern();
            for (int i = 0; i < 50; i++)
            {
                double x = i / 5.0;
                double y = Math.Cos(x);
                ds.LinePoints.Add(new Point(x, y));
            }
            dc.Add(ds);
            cs.SetLines(dc);
        }
    }
}
