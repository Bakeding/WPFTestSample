using LiveChartsTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LiveChartsTest.ViewModels
{

    public class DistrictNodeViewModel : NotificationObject
    {
        private bool? isSelected = false;

        public bool? IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        private bool? isChecked = false;

        public bool? IsChecked
        {
            get { return isChecked; }
            set
            {
                SetIsChecked(value);
            }
        }

        private void SetIsChecked(bool? value)
        {
            if (value != isChecked)
            {
                isChecked = value;
                RaisePropertyChanged("IsChecked");
            }
            if (this.Children.Count > 0 && this.Children[0].isChecked != value)
            {
                //设置子节点勾选状态
                foreach (var item in this.Children)
                {
                    if (value != null)
                    {
                        item.IsChecked = value;
                    }
                }
            }
            if (this.parent != null)
            {
                if (this.Parent.Children.Count == this.Parent.Children.Count(item => item.isChecked == value))
                {
                    //同一级节点全部选中，则父节点选中。反之亦然。
                    this.Parent.IsChecked = value;
                }
                else if (this.Parent.Children.Count > this.Parent.Children.Count(item => item.isChecked == value))
                {
                    if (this.Parent.IsChecked != null)
                    {
                        this.Parent.IsChecked = null;
                    }
                }
            }

        }

        private bool? isExpand = false;

        public bool? IsExpand
        {
            get { return isExpand; }
            set
            {
                isExpand = value;
                RaisePropertyChanged("IsExpand");
            }
        }

        private BitmapImage img;

        public BitmapImage Img
        {
            get { return img; }
            set
            {
                img = value;
                RaisePropertyChanged("Img");
            }
        }

        private ObservableCollection<DistrictNodeViewModel> children = new ObservableCollection<DistrictNodeViewModel>();

        public ObservableCollection<DistrictNodeViewModel> Children
        {
            get { return children; }
            set
            {
                children = value;
                RaisePropertyChanged("Children");
            }
        }

        private DistrictNodeViewModel parent;

        public DistrictNodeViewModel Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                RaisePropertyChanged("Parent");
            }
        }

        private District district;

        public District District
        {
            get { return district; }
            set
            {
                district = value;
                RaisePropertyChanged("District");
            }
        }
    }

}
