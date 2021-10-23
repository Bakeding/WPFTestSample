using HelloDotNetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HelloDotNetCore.ViewWindow
{
    /// <summary>
    /// InputDataChangeValidate.xaml 的交互逻辑
    /// </summary>
    public partial class InputDataChangeValidate : Window
    {
        int hash;
        bool discardChanges;
        public InputDataChangeValidate()
        {
            InitializeComponent();
            discardChanges = false;

            var contact = new Contact("wang", "wang@163.com", "whut");
            hash = contact.GetHashCode();

            this.DataContext = contact;
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.DataContext.GetHashCode() != hash && !discardChanges)
            {
                FocusManager.SetFocusedElement(this, SnackbarUnsavedChanges);
                SnackbarUnsavedChanges.IsActive = true;
                e.Cancel = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Save Data
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarUnsavedChanges.IsActive = false;
            discardChanges = true;
            this.Close();
        }

        private void SnackbarUnsavedChanges_LostFocus(object sender, RoutedEventArgs e)
        {
            if (FocusManager.GetFocusedElement(this)!= SnackbarUnsavedChangesMessage)
            {
                SnackbarUnsavedChanges.IsActive = false;
            }
            
            //MessageBox.Show(FocusManager.GetFocusedElement(this).ToString());
        }
    }
}
