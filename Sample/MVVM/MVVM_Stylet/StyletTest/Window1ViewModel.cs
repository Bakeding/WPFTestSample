using Stylet;
using System;
using System.Collections.Generic;
using System.Text;

namespace StyletTest
{
    public class Window1ViewModel:Screen
    {
        private IWindowManager _windowManger;
        //private ShellViewModel _ChildDialog;

        public Window1ViewModel(IWindowManager windowManager)
        {
            _windowManger = windowManager;
            //_ChildDialog = ChildDialog;
        }

        public string FName { get; set; } = "ly";
        public string Name { get; set; } = "waku";  // C#6的语法, 声明自动属性并赋值

        public void SayHello() => Name = "Hello " + Name;    // C#6的语法, 表达式方法
        public bool CanSayHello => !string.IsNullOrEmpty(Name);  // 同上
        public void BtnCommand()
        {
            FName = DateTime.Now.ToString();
        }

        public bool CanBtnCommand
        {
            get
            {
                return !string.IsNullOrWhiteSpace(FName);
            }
        }

        public void ShowMessage() => _windowManger.ShowMessageBox(FName);
        //public void ShowDialog() => _windowManger.ShowDialog(_ChildDialog);
    }
}
