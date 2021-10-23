using System;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using ChartControl;

namespace WpfChart
{
    [Export(typeof(IScreen)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ChartControlViewModel : Screen
    {
        [ImportingConstructor]
        public ChartControlViewModel()
        {
            DisplayName = "03. Chart Control";
            DataCollection = new BindableCollection<LineSeriesControl>();
        }

        public BindableCollection<LineSeriesControl> DataCollection{get;set;}

        public void AddChart()
        {
            DataCollection.Clear();
            LineSeriesControl ds = new LineSeriesControl();
            ds.LineColor = Brushes.Blue;
            ds.LineThickness = 2;
            ds.SeriesName = "Sine";
            ds.LinePattern = LinePatternEnum.Solid;
            for (int i = 0; i < 50; i++)
            {
                double x = i / 5.0;
                double y = Math.Sin(x);
                ds.LinePoints.Add(new Point(x, y));
            }
            DataCollection.Add(ds);

            ds = new LineSeriesControl();
            ds.LineColor = Brushes.Red;
            ds.LineThickness = 2;
            ds.SeriesName = "Cosine";
            ds.LinePattern = LinePatternEnum.Dash;
            for (int i = 0; i < 50; i++)
            {
                double x = i / 5.0;
                double y = Math.Cos(x);
                ds.LinePoints.Add(new Point(x, y));
            }
            DataCollection.Add(ds);
        }
    }
}
