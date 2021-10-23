using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMVVM.Commands;

namespace WpfMVVM.ViewModels
{
    class RadioViewModelClass : INotifyPropertyChanged
    {
        string currentOption;

        public string CurrentOption
        {
            get
            {
                return currentOption;
            }
            set
            {

                if (value != null) //要判斷一下是否為 null，否則選了A，又選B時，最後一個回傳的會是A的值，這樣就抓不到了。
                {
                    currentOption = value;
                    MessageBox.Show("You Select " + currentOption);
                }
            }
        }

        //DelegateCommand cmdShowMessage;

        //public DelegateCommand CmdShowMessage
        //{
        //    get { return cmdShowMessage; }
        //    set { cmdShowMessage = value; }
        //}
        public RadioViewModelClass()
        {
        //    cmdShowMessage = new DelegateCommand();
        //    this.cmdShowMessage.ExecuteAction = new Action<object>(this.ShowMsg);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(prop));
        }

        //public void ShowMsg(object msg)
        //{
        //    System.Windows.MessageBox.Show("You Select " + currentOption);
        //}
    }
}
