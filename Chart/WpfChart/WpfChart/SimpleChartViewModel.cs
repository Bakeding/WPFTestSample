using System;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;


namespace WpfChart
{
    [Export(typeof(IScreen)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class SimpleChartViewModel : Screen
    {
        [ImportingConstructor]
        public SimpleChartViewModel()
        {
            DisplayName = "01. Simple Line";
        }

        private double chartWidth = 300;
        private double chartHeight = 300;
        private double xmin = 0;
        private double xmax = 6.5;
        private double ymin = -1.1;
        private double ymax = 1.1;

        private PointCollection solidLinePoints;
        public PointCollection SolidLinePoints
        {
            get { return solidLinePoints; }
            set
            {
                solidLinePoints = value;
                NotifyOfPropertyChange(() => SolidLinePoints);
            }
        }

        private PointCollection dashLinePoints;
        public PointCollection DashLinePoints
        {
            get { return dashLinePoints; }
            set
            {
                dashLinePoints = value;
                NotifyOfPropertyChange(() => DashLinePoints);
            }
        }

        public void AddChart(double width, double height)
        {
            chartWidth = width;
            chartHeight = height;

            SolidLinePoints = new PointCollection();
            DashLinePoints = new PointCollection();
            double x = 0;
            double y = 0;
            double z = 0;
            for (int i = 0; i < 70; i++)
            {
                x = i / 5.0;
                y = Math.Sin(x);
                z = Math.Cos(x);

                DashLinePoints.Add(NormalizePoint(new Point(x, z)));
                SolidLinePoints.Add(NormalizePoint(new Point(x, y)));
            }
        }

        public Point NormalizePoint(Point pt)
        {
            var res = new Point();
            res.X = (pt.X - xmin) * chartWidth / (xmax - xmin);
            res.Y = chartHeight - (pt.Y - ymin) * chartHeight / (ymax - ymin);
            return res;
        }
    }
}
