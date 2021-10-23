using System;
using System.Windows.Media;
using System.Windows.Shapes;
using Caliburn.Micro;
using System.Windows;

namespace WpfChart.ChartModel
{
    public class LineSeries : PropertyChangedBase
    {
        public LineSeries()
        {
            LinePoints = new BindableCollection<Point>();
        }

        public BindableCollection<Point> LinePoints { get; set; }

        private Brush lineColor = Brushes.Black;
        public Brush LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }
        private double lineThickness = 1;
        public double LineThickness
        {
            get { return lineThickness; }
            set { lineThickness = value; }
        }

        public LinePatternEnum LinePattern { get; set; }

        private string seriesName = "Default";
        public string SeriesName 
        {
            get { return seriesName; }
            set { seriesName = value; }
        }

        private DoubleCollection lineDashPattern;
        public DoubleCollection LineDashPattern
        {
            get { return lineDashPattern; }
            set
            {
                lineDashPattern = value;
                NotifyOfPropertyChange(() => LineDashPattern);
            }
        }

        public void SetLinePattern()
        {
            switch (LinePattern)
            {
                case LinePatternEnum.Dash:
                    LineDashPattern = new DoubleCollection() { 4, 3 };
                    break;
                case LinePatternEnum.Dot:
                    LineDashPattern = new DoubleCollection() { 1, 2 };
                    break;
                case LinePatternEnum.DashDot:
                    LineDashPattern = new DoubleCollection() { 4, 2, 1, 2 };
                    break;
            }
        }
    }

    public enum LinePatternEnum
    {
        Solid = 1,
        Dash = 2,
        Dot = 3,
        DashDot = 4,
    }
}
