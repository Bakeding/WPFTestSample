using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding.Models
{
    public class UserInfo //: INotifyPropertyChanged//用户类
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }
        private string userLevel;

        public string UserLevel
        {
            get { return userLevel; }
            set { userLevel = value; }
        }

        private bool loginState;

        public bool LoginState
        {
            get { return loginState; }
            set { loginState = value; }
        }
        
        //public event PropertyChangedEventHandler PropertyChanged;
    }
}
