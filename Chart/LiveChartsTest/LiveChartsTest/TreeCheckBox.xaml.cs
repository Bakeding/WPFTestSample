using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LiveChartsTest
{
    /// <summary>
    /// TreeCheckBox.xaml 的交互逻辑
    /// </summary>
    public partial class TreeCheckBox : Window
    {
        public TreeCheckBox()
        {
             TreeNode node1 = new TreeNode("1", "");
            TreeNode node2 = new TreeNode("2", "");
            TreeNode node1_1 = new TreeNode("1.1", "");
            TreeNode node1_2 = new TreeNode("1.2", "");
            TreeNode node1_3 = new TreeNode("1.3", "");
            TreeNode node1_2_1 = new TreeNode("1.2.1", "");
            TreeNode node1_2_2 = new TreeNode("1.2.2", "");
            node1_2.Children = new List<TreeNode>() { node1_2_1, node1_2_2 };
            node1.Children = new List<TreeNode>() { node1_1, node1_2, node1_3 };
            List<TreeNode> nodes = new List<TreeNode>() { node1, node2 };
            
            this.treeElement.ItemsSource = nodes;

            InitializeComponent();
        }
    }
}
