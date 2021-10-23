using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LiveChartsTest.Commands
{
    class DelegateCommand:ICommand
    {
        public bool CanExecute(object parameter)
        {
            if (this.CanExecuteFunc==null)
            {
                return true;
            }
            this.CanExecuteFunc(parameter);
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (this.ExecuteAction == null)
            {
                return;
            }
            this.ExecuteAction(parameter);
        }

        public Action<object> ExecuteAction { get; set; }
        public Func<object,bool> CanExecuteFunc { get; set; }
    }
}
