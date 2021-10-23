using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding.Models
{
    class AAA
    {
        //设置主键
        [PrimaryKey, AutoIncrement]
        public long rowid { get; set; } //记录ID
                                        //添加索引
        [Indexed("AAA_idx1", 1)]
        public string bk1 { get; set; } //状态
        public string bk2 { get; set; } //备用2
        public string bk3 { get; set; } //备用3
        public string bk4 { get; set; } //备用4
        public string bk5 { get; set; } //备用5

    }
}
