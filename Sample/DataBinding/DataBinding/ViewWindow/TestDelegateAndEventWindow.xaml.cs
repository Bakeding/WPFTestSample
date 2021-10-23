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
    /// TestDelegateAndEventWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TestDelegateAndEventWindow : Window
    {
        public class Test
        {
            public delegate void MyDelegate();
            //创建一个委托实例 
            public MyDelegate myDel;
            //声明一个事件
            public event MyDelegate EventMyDel;
            //事件触发机制(必须和事件在同一个类中) 外界无法直接用EventMyDel()来触发事件
            public void DoEventDel()
            {
                EventMyDel();
            }
        }


        Test test = new Test();
        public TestDelegateAndEventWindow()
        {
            InitializeComponent();
            //注册委托(挂载方法) 
            test.myDel += Fun_A;
            test.myDel += Fun_B;
            test.EventMyDel += Fun_A;
            test.EventMyDel += Fun_B;
        }

        //方法A 
        public void Fun_A()
        {
            MessageBox.Show("A 方法触发了");
        }
        //方法B 
        public void Fun_B()
        {
            MessageBox.Show("B 方法触发了");
        }
        //方法C 
        public void Fun_C()
        {
            MessageBox.Show("C 方法触发了");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            test.myDel();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            test.myDel = null;
            test.myDel += Fun_C;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            test.DoEventDel();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            test.EventMyDel += null;
            test.EventMyDel += Fun_C;
        }

    }
}
