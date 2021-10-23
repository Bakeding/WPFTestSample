using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChartsTest.Models
{
    public class District
    {
        public int ID { get; set; }
        public string Xzqhdm { get; set; }//行政区划代码
        public string Xzqhmc { get; set; }//行政区划名称
        public int Level { get; set; }//级别，0全国，1省，2地市，3县，4，乡镇，5，村
        public IList<District> Children { get; set; }
        public District Parent { get; set; }
    }
}
