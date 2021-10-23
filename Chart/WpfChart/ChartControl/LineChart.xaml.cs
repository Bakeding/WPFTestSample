using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Caliburn.Micro;
using System.Collections.Specialized;

namespace ChartControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class LineChart : UserControl
    {
        private ChartStyleControl cs;
        public LineChart()
        {
            InitializeComponent();
            this.cs = new ChartStyleControl();
            this.cs.TextCanvas = textCanvas;
            this.cs.ChartCanvas = chartCanvas;
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeLineChart();
        }

        private void SetLineChart()
        {
            cs.Xmin = this.Xmin;
            cs.Xmax = this.Xmax;
            cs.Ymin = this.Ymin;
            cs.Ymax = this.Ymax;
            cs.XTick = this.XTick;
            cs.YTick = this.YTick;
            cs.XLabel = this.XLabel;
            cs.YLabel = this.YLabel;
            cs.Title = this.Title;
            cs.IsXGrid = this.IsXGrid;
            cs.IsYGrid = this.IsYGrid;
            cs.GridlineColor = this.GridlineColor;
            cs.GridlinePattern = this.GridlinePattern;
            
            ResizeLineChart();
        }

        private void ResizeLineChart()
        {
            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);

            if (DataCollection != null)
            {
                if (DataCollection.Count > 0)
                {
                    cs.SetLines(DataCollection);
                }
            }
        }

        public static DependencyProperty XminProperty =
                      DependencyProperty.Register("Xmin", typeof(double),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double Xmin
        {
            get { return (double)GetValue(XminProperty); }
            set { SetValue(XminProperty, value); }
        }

        public static DependencyProperty XmaxProperty =
                      DependencyProperty.Register("Xmax", typeof(double),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double Xmax
        {
            get { return (double)GetValue(XmaxProperty); }
            set { SetValue(XmaxProperty, value); }
        }

        public static DependencyProperty YminProperty =
                      DependencyProperty.Register("Ymin", typeof(double),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double Ymin
        {
            get { return (double)GetValue(YminProperty); }
            set { SetValue(YminProperty, value); }
        }

        public static DependencyProperty YmaxProperty =
                      DependencyProperty.Register("Ymax", typeof(double),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double Ymax
        {
            get { return (double)GetValue(YmaxProperty); }
            set { SetValue(YmaxProperty, value); }
        }

        public static DependencyProperty XTickProperty =
                      DependencyProperty.Register("XTick", typeof(double),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double XTick
        {
            get { return (double)GetValue(XTickProperty); }
            set { SetValue(XTickProperty, value); }
        }

        public static DependencyProperty YTickProperty =
                      DependencyProperty.Register("YTick", typeof(double),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double YTick
        {
            get { return (double)GetValue(YTickProperty); }
            set { SetValue(YTickProperty, value); }
        }

        public static DependencyProperty XLabelProperty =
                      DependencyProperty.Register("XLabel", typeof(string),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata("X Axis", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string XLabel
        {
            get { return (string)GetValue(XLabelProperty); }
            set { SetValue(XLabelProperty, value); }
        }

        public static DependencyProperty YLabelProperty =
                      DependencyProperty.Register("YLabel", typeof(string),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata("Y Axis", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string YLabel
        {
            get { return (string)GetValue(YLabelProperty); }
            set { SetValue(YLabelProperty, value); }
        }

        public static DependencyProperty TitleProperty =
                      DependencyProperty.Register("Title", typeof(string),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata("My Title", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static DependencyProperty IsXGridProperty =
                      DependencyProperty.Register("IsXGrid", typeof(bool),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool IsXGrid
        {
            get { return (bool)GetValue(IsXGridProperty); }
            set { SetValue(IsXGridProperty, value); }
        }

        public static DependencyProperty IsYGridProperty =
                      DependencyProperty.Register("IsYGrid", typeof(bool),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool IsYGrid
        {
            get { return (bool)GetValue(IsYGridProperty); }
            set { SetValue(IsYGridProperty, value); }
        }

        public static DependencyProperty GridlineColorProperty =
                      DependencyProperty.Register("GridlineColor", typeof(Brush),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata(Brushes.Gray, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush GridlineColor
        {
            get { return (Brush)GetValue(GridlineColorProperty); }
            set { SetValue(GridlineColorProperty, value); }
        }

        public static DependencyProperty GridlinePatternProperty =
                      DependencyProperty.Register("GridlinePattern",
                      typeof(LinePatternEnum),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata(LinePatternEnum.Solid, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public LinePatternEnum GridlinePattern
        {
            get { return (LinePatternEnum)GetValue(GridlinePatternProperty); }
            set { SetValue(GridlinePatternProperty, value); }
        }


        public static readonly DependencyProperty DataCollectionProperty = DependencyProperty.Register("DataCollection",
              typeof(BindableCollection<LineSeriesControl>), typeof(LineChart),
              new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDataChanged));
        public BindableCollection<LineSeriesControl> DataCollection
        {
            get { return (BindableCollection<LineSeriesControl>)GetValue(DataCollectionProperty); }
            set { SetValue(DataCollectionProperty, value); }
        }

        private static void OnDataChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var lc = sender as LineChart;
            var dc = e.NewValue as BindableCollection<LineSeriesControl>;
            if (dc != null)
                dc.CollectionChanged += lc.dc_CollectionChanged;
        }

        private void dc_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (DataCollection != null)
            {
                CheckCount = 0;
                if (DataCollection.Count > 0)
                    CheckCount = DataCollection.Count;
            }
        }


        public static DependencyProperty CheckCountProperty =
                      DependencyProperty.Register("CheckCount", typeof(int),
                      typeof(LineChart),
                      new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                      new PropertyChangedCallback(OnStartChart)));

        public int CheckCount
        {
            get { return (int)GetValue(CheckCountProperty); }
            set { SetValue(CheckCountProperty, value); }
        }

        private static void OnStartChart(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as LineChart).SetLineChart();
        }
    }
}
