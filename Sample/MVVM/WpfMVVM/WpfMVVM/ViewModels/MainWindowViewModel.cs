using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMVVM.Commands;

namespace WpfMVVM.ViewModels
{
    class MainWindowViewModel:NotificationObject
    {
        private int input1;

        public int Input1
        {
            get { return input1; }
            set 
            {
                input1 = value;
                this.RaisePropertyChanged("Input1");
            }
        }

        private int input2;

        public int Input2
        {
            get { return input2; }
            set
            {
                input2 = value;
                this.RaisePropertyChanged("Input2");
            }
        }

        private int result;

        public int Result
        {
            get { return result; }
            set
            {
                result = value;
                this.RaisePropertyChanged("Result");
            }
        }

        public DelegateCommand AddCommand { get; set;}
        public DelegateCommand SaveCommand { get; set; }

        private void Add(object parameter)
        {
            this.Result=this.Input1+this.Input2;
        }

        private void Save(object parameter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.ShowDialog();
        }

        public MainWindowViewModel()
        {
            this.AddCommand = new DelegateCommand();
            this.AddCommand.ExecuteAction = new Action<object>(this.Add);

            this.SaveCommand=new DelegateCommand();
            this.SaveCommand.ExecuteAction = new Action<object>(this.Save);
        }
    }
}
