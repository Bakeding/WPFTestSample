using Caliburn.Micro;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfChart.ChartModel
{
    public class ChartStyle
    {
        private double xmin = 0;
        private double xmax = 6.5;
        private double ymin = -1.1;
        private double ymax = 1.1;
        private string title = "Title";
        private string xLabel = "X Axis";
        private string yLabel = "Y Axis";
        private bool isXGrid = true;
        private bool isYGrid = true;
        private Brush gridlineColor = Brushes.LightGray;
        private double xTick = 1;
        private double yTick = 0.5;
        private LinePatternEnum gridlinePattern;
        private double leftOffset = 20;
        private double bottomOffset = 15;
        private double rightOffset = 10;
        private Line gridline = new Line();
        public Canvas TextCanvas { get; set; }

        public Canvas ChartCanvas { get; set; }

        public double Xmin
        {
            get { return xmin; }
            set { xmin = value; }
        }

        public double Xmax
        {
            get { return xmax; }
            set { xmax = value; }
        }

        public double Ymin
        {
            get { return ymin; }
            set { ymin = value; }
        }

        public double Ymax
        {
            get { return ymax; }
            set { ymax = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string XLabel
        {
            get { return xLabel; }
            set { xLabel = value; }
        }

        public string YLabel
        {
            get { return yLabel; }
            set { yLabel = value; }
        }

        public Line Gridline
        {
            get { return gridline; }
            set { gridline = value; }
        }

        public LinePatternEnum GridlinePattern
        {
            get { return gridlinePattern; }
            set { gridlinePattern = value; }
        }

        public double XTick
        {
            get { return xTick; }
            set { xTick = value; }
        }

        public double YTick
        {
            get { return yTick; }
            set { yTick = value; }
        }

        public Brush GridlineColor
        {
            get { return gridlineColor; }
            set { gridlineColor = value; }
        }

        public bool IsXGrid
        {
            get { return isXGrid; }
            set { isXGrid = value; }
        }

        public bool IsYGrid
        {
            get { return isYGrid; }
            set { isYGrid = value; }
        }


        public void AddChartStyle(TextBlock tbTitle, TextBlock tbXLabel, TextBlock tbYLabel)
        {
            Point pt = new Point();
            Line tick = new Line();
            double offset = 0;
            double dx, dy;
            TextBlock tb = new TextBlock();

            //  determine right offset:
            tb.Text = Xmax.ToString();
            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Size size = tb.DesiredSize;
            rightOffset = 5;

            // Determine left offset:
            for (dy = Ymin; dy <= Ymax; dy += YTick)
            {
                pt = NormalizePoint(new Point(Xmin, dy));
                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.TextAlignment = TextAlignment.Right;
                tb.Measure(new Size(Double.PositiveInfinity,
                                    Double.PositiveInfinity));
                size = tb.DesiredSize;
                if (offset < size.Width)
                    offset = size.Width;
            }
            leftOffset = offset + 5;
            Canvas.SetLeft(ChartCanvas, leftOffset);
            Canvas.SetBottom(ChartCanvas, bottomOffset);
            ChartCanvas.Width = Math.Abs(TextCanvas.Width - leftOffset - rightOffset);
            ChartCanvas.Height = Math.Abs(TextCanvas.Height - bottomOffset - size.Height / 2);
                       
            Rectangle chartRect = new Rectangle();
            chartRect.Stroke = Brushes.Black;
            chartRect.Width = ChartCanvas.Width;
            chartRect.Height = ChartCanvas.Height;
            ChartCanvas.Children.Add(chartRect);           
            
            // Create vertical gridlines:
            if (IsYGrid == true)
            {
                for (dx = Xmin + XTick; dx < Xmax; dx += XTick)
                {
                    gridline = new Line();
                    AddLinePattern();
                    gridline.X1 = NormalizePoint(new Point(dx, Ymin)).X;
                    gridline.Y1 = NormalizePoint(new Point(dx, Ymin)).Y;
                    gridline.X2 = NormalizePoint(new Point(dx, Ymax)).X;
                    gridline.Y2 = NormalizePoint(new Point(dx, Ymax)).Y;
                    ChartCanvas.Children.Add(gridline);
                }
            }

            // Create horizontal gridlines:
            if (IsXGrid == true)
            {
                for (dy = Ymin + YTick; dy < Ymax; dy += YTick)
                {
                    gridline = new Line();
                    AddLinePattern();
                    gridline.X1 = NormalizePoint(new Point(Xmin, dy)).X;
                    gridline.Y1 = NormalizePoint(new Point(Xmin, dy)).Y;
                    gridline.X2 = NormalizePoint(new Point(Xmax, dy)).X;
                    gridline.Y2 = NormalizePoint(new Point(Xmax, dy)).Y;
                    ChartCanvas.Children.Add(gridline);
                }
            }

            // Create x-axis tick marks:
            for (dx = Xmin; dx <= Xmax; dx += xTick)
            {
                pt = NormalizePoint(new Point(dx, Ymin));
                tick = new Line();
                tick.Stroke = Brushes.Black;
                tick.X1 = pt.X;
                tick.Y1 = pt.Y;
                tick.X2 = pt.X;
                tick.Y2 = pt.Y - 5;
                ChartCanvas.Children.Add(tick);

                tb = new TextBlock();
                tb.Text = dx.ToString();
                tb.Measure(new Size(Double.PositiveInfinity,
                                    Double.PositiveInfinity));
                size = tb.DesiredSize;
                TextCanvas.Children.Add(tb);
                Canvas.SetLeft(tb, leftOffset + pt.X - size.Width / 2);
                Canvas.SetTop(tb, pt.Y + 2 + size.Height / 2);

            }

            // Create y-axis tick marks:
            for (dy = Ymin; dy <= Ymax; dy += YTick)
            {
                pt = NormalizePoint(new Point(Xmin, dy));
                tick = new Line();
                tick.Stroke = Brushes.Black;
                tick.X1 = pt.X;
                tick.Y1 = pt.Y;
                tick.X2 = pt.X + 5;
                tick.Y2 = pt.Y;
                ChartCanvas.Children.Add(tick);

                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.Measure(new Size(Double.PositiveInfinity,
                                    Double.PositiveInfinity));
                size = tb.DesiredSize;
                TextCanvas.Children.Add(tb);
                Canvas.SetRight(tb, ChartCanvas.Width + rightOffset + 2);
                Canvas.SetTop(tb, pt.Y);
            }

            // Add title and labels:
            tbTitle.Text = Title;
            tbXLabel.Text = XLabel;
            tbYLabel.Text = YLabel;
            tbXLabel.Margin = new Thickness(leftOffset + 2, 2, 2, 2);
            tbTitle.Margin = new Thickness(leftOffset + 2, 2, 2, 2);
        }

        public void AddLinePattern()
        {
            gridline.Stroke = GridlineColor;
            gridline.StrokeThickness = 1;

            switch (GridlinePattern)
            {
                case LinePatternEnum.Dash:
                    gridline.StrokeDashArray = new DoubleCollection() { 4, 3 };
                    break;
                case LinePatternEnum.Dot:
                    gridline.StrokeDashArray = new DoubleCollection() { 1, 2 };
                    break;
                case LinePatternEnum.DashDot:
                    gridline.StrokeDashArray = new DoubleCollection() { 4, 2, 1, 2 };
                    break;
            }
        }

        public Point NormalizePoint(Point pt)
        {
            if (Double.IsNaN(ChartCanvas.Width) || ChartCanvas.Width <= 0)
                ChartCanvas.Width = 270;
            if (Double.IsNaN(ChartCanvas.Height) || ChartCanvas.Height <= 0)
                ChartCanvas.Height = 250;
            Point result = new Point();
            result.X = (pt.X - Xmin) * ChartCanvas.Width / (Xmax - Xmin);
            result.Y = ChartCanvas.Height - (pt.Y - Ymin) * ChartCanvas.Height / (Ymax - Ymin);
            return result;
        }

        public void SetLines(BindableCollection<LineSeries> dc)
        {
            if (dc.Count <= 0)
                return;

            int i = 0;
            foreach (var ds in dc)
            {
                PointCollection pts = new PointCollection();

                if (ds.SeriesName == "Default")
                    ds.SeriesName = "LineSeries" + i.ToString();
                ds.SetLinePattern();
                for (int j = 0; j < ds.LinePoints.Count; j++)
                {
                    var pt = NormalizePoint(ds.LinePoints[j]);
                    pts.Add(pt);
                }

                Polyline line = new Polyline();
                line.Points = pts;
                line.Stroke = ds.LineColor;
                line.StrokeThickness = ds.LineThickness;
                line.StrokeDashArray = ds.LineDashPattern;
                ChartCanvas.Children.Add(line);
                i++;
            }
        }
    }
}
