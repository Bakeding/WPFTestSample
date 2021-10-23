using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVVM.ViewModels
{
    class RadioShowMsgCmdClass
    {
        RadioViewModelClass vm;
        public RadioShowMsgCmdClass(RadioViewModelClass fvm)
        {
            vm = fvm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        //public void Execute(object parameter)
        //{
        //    vm.ShowMsg();
        //}
        public void Execute()
        {
            //vm.ShowMsg(null);
        }
    }
}
