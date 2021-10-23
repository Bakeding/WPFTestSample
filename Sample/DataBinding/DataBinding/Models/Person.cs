using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding.Models
{
    public class Person : INotifyPropertyChanged
    {
        private String _name = "张三";
        private int _age = 24;
        private String _hobby = "篮球";

        public String Name
        {
            set
            {
                _name = value;
                if (PropertyChanged != null)//有改变  
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));//对Name进行监听  
                }
            }
            get
            {
                return _name;
            }
        }

        public int Age
        {
            set
            {
                _age = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Age"));//对Age进行监听  
                }
            }
            get
            {
                return _age;
            }
        }
        public String Hobby
        {
            get { return _hobby; }
            set 
            { 
                _hobby = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Hobby"));//对Hobby进行监听  
                }
            }
        }

        private List<UserInfo> userList =new List<UserInfo>()
        {
            new UserInfo(){UserName="张三",UserLevel="12",PassWord="ddd",LoginState=true},
            new UserInfo(){UserName="李四",UserLevel="23",PassWord="的撒",LoginState=true},
            new UserInfo(){UserName="王五",UserLevel="21",PassWord="绿卡的撒",LoginState=true},
            new UserInfo(){UserName="赵六",UserLevel="55",PassWord="件夹具",LoginState=true}
        };

        public List<UserInfo> UserList
        {
            get { return userList; }
            set 
            { 
                userList = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("UserList"));//对UserList进行监听  
                }
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
