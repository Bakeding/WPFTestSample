using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChartsTest
{
    public class TreeNode : INotifyPropertyChanged
    {
        #region 析构函数
        public TreeNode()
        {

        }
        public TreeNode(string name, object content)
        {
            _name = name;
            _content = content;
        }
        #endregion

        #region 属性
        private TreeNode _parent;

        public TreeNode Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != value)
                {
                    _parent = value;
                    OnPropertyChanged("Parent");
                }
            }
        }

        private List<TreeNode> _children;

        public List<TreeNode> Children
        {
            get { return _children; }
            set
            {
                if (_children != value)
                {
                    _children = value;
                    OnPropertyChanged("Children");
                }
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private bool? _isChecked = false;

        public bool? IsChecked
        {
            get { return _isChecked; }
            set
            {
                SetChecked(value, true, true);
                //OnPropertyChanged("IsChecked");
            }
        }

        private object _content;

        public object Content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged("Content");
                }
            }
        }


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #region 方法
        private void SetChecked(bool? value,bool checkChild,bool checkParent)
        {
            if (_isChecked==value)
            {
                return;
            }
            _isChecked = value;
			//如果有子节点，检查子节点状态
            if (checkChild&&value.HasValue&&_children!=null)
            {
                _children.ForEach(c => c.SetChecked(value,true,false));
            }
            //如果有父节点，检查父节点状态
            if (checkParent&&_parent!=null)
            {
                _parent.CheckParent();
            }
            OnPropertyChanged("IsChecked");
        }

        private void CheckParent()
        {
            string checkedNames = string.Empty;
            bool? _currentState = this.IsChecked;
            bool? _firstState = null;
            for (int i = 0; i < this.Children.Count(); i++)
            {
                bool? childrenState = this.Children[i].IsChecked;
                if (i == 0)
                {
                    _firstState = childrenState;
                }
                else if (_firstState != childrenState)
                {
                    _firstState = null;
                }
            }
            if (_firstState != null) _currentState = _firstState;
            SetChecked(_firstState, false, true);
        }


        #endregion

    }
}

