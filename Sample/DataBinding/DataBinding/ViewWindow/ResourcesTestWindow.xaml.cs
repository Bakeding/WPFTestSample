using System;
using System.Collections.Generic;
using System.IO;
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
    /// ResourcesTestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ResourcesTestWindow : Window
    {
        public ResourcesTestWindow()
        {
            InitializeComponent();
            tbshow.Text = DataBinding.Properties.Resources.testString;

            var testImg = DataBinding.Properties.Resources.brush;
            MemoryStream memory = new MemoryStream();
            testImg.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
            ImageSourceConverter converter = new ImageSourceConverter();
            ImageSource source = (ImageSource)converter.ConvertFrom(memory);
            imgShow.Source = source;
            tbtxt.Text = DataBinding.Properties.Resources.test;
            
        }
    }
}
