﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HelloDotNetCore.ViewWindow
{
    /// <summary>
    /// ClockWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ClockWindow : Window
    {
        public ClockWindow()
        {
            InitializeComponent();
            Storyboard seconds = (Storyboard)second.FindResource("sbseconds");
            seconds.Begin();
            seconds.Seek(new TimeSpan(0, 0, 0, DateTime.Now.Second, 0));

            Storyboard minutes = (Storyboard)minute.FindResource("sbminutes");
            minutes.Begin();
            minutes.Seek(new TimeSpan(0, 0, DateTime.Now.Minute, DateTime.Now.Second, 0));

            Storyboard hours = (Storyboard)hour.FindResource("sbhours");
            hours.Begin();
            hours.Seek(new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 0));
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
