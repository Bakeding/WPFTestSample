using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsTest.Models;

namespace LiveChartsTest.ViewModels
{
    public class DistrictMainViewModel : NotificationObject
    {
        private ObservableCollection<DistrictNodeViewModel> vmNodes;

        public ObservableCollection<DistrictNodeViewModel> VmNodes
        {
            get { return vmNodes; }
            set
            {
                vmNodes = value;
                RaisePropertyChanged("VmNodes");
            }
        }

        public DistrictMainViewModel()
        {
            this.VmNodes = new ObservableCollection<DistrictNodeViewModel>
            {
                LoadData()
            };
        }
        public DistrictNodeViewModel LoadData()
        {
            ObservableCollection<District> rootNodes = new ObservableCollection<District>();
            District d00 = new District()
            {
                Xzqhmc = "全国",
                Parent = null
            };
            District d0 = new District()
            {
                Xzqhmc = "河南",
                Parent = d00
            };

            District d1 = new District()
            {
                Xzqhmc = "北京",
                Parent = d00
            };

            District d2 = new District()
            {
                Xzqhmc = "山东",
                Parent = d00
            };
            District d11 = new District()
            {
                Xzqhmc = "海淀区",
                Parent = d1
            };
            District d12 = new District()
            {
                Xzqhmc = "石景山区",
                Parent = d1
            };
            District d13 = new District()
            {
                Xzqhmc = "朝阳区",
                Parent = d1
            };

            District d01 = new District()
            {
                Xzqhmc = "商丘",
                Parent = d0
            };
            District d02 = new District()
            {
                Xzqhmc = "郑州",
                Parent = d0
            };
            District d03 = new District()
            {
                Xzqhmc = "周口",
                Parent = d0
            };
            d1.Children = new List<District> { d11, d12, d13 };
            d0.Children = new List<District> { d01, d02, d03 };
            d00.Children = new List<District> { d1, d2, d0 };
            rootNodes.Add(d00);
            DistrictNodeViewModel dnv = new DistrictNodeViewModel();
            dnv.District = rootNodes[0];
            SetDNV(dnv, rootNodes[0]);
            return dnv;
        }

        private void SetDNV(DistrictNodeViewModel vm, District root)
        {
            if (root == null || root.Children == null || root.Children.Count == 0)
            {
                return;
            }
            foreach (var item in root.Children)
            {
                DistrictNodeViewModel vmNew = new DistrictNodeViewModel();
                vmNew.District = item;
                vmNew.Parent = vm;
                vmNew.Img = new System.Windows.Media.Imaging.BitmapImage(new Uri("/dog.jpg", UriKind.Relative));
                vm.Children.Add(vmNew);
                SetDNV(vmNew, item);
            }
        }

    }
}
