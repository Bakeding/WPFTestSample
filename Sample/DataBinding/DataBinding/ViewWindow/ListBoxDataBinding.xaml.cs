using DataBinding.Models;
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
    /// ListBoxDataBinding.xaml 的交互逻辑
    /// </summary>
    public partial class ListBoxDataBinding : Window
    {
        public ListBoxDataBinding()
        {
            InitializeComponent();
        }

        private Person p1 = new Person();
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            grid.DataContext = p1;//绑定数据 
            Binding binding = new Binding();
            binding.Path = new PropertyPath("UserList");
            lsUser.SetBinding(ListBox.ItemsSourceProperty,binding);
            p1.Name = "李四";
            p1.Hobby = "足球";
            p1.UserList = new List<UserInfo>()
            {
                new UserInfo(){UserName="张三",UserLevel="12",PassWord="ddd",LoginState=true},
                new UserInfo(){UserName="李四",UserLevel="23",PassWord="的撒",LoginState=true},
                new UserInfo(){UserName="王五",UserLevel="21",PassWord="绿卡的撒",LoginState=true},
                new UserInfo(){UserName="赵六",UserLevel="55",PassWord="件夹具",LoginState=true}
            };
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            p1.Age = p1.Age + 1;
            p1.Hobby = "足球";
            p1.UserList = new List<UserInfo>()
            {
                new UserInfo(){UserName="三",UserLevel="2",PassWord="呃呃呃呃",LoginState=true},
                new UserInfo(){UserName="四",UserLevel="3",PassWord="地空导弹",LoginState=true},
                new UserInfo(){UserName="五",UserLevel="1",PassWord="dfas",LoginState=true},
                new UserInfo(){UserName="六",UserLevel="5",PassWord="踩踩踩",LoginState=true},
                new UserInfo(){UserName="1i",UserLevel="1",PassWord="dfas",LoginState=true},
                new UserInfo(){UserName="df",UserLevel="5",PassWord="踩踩踩",LoginState=true}
            };
        }
    }
}
