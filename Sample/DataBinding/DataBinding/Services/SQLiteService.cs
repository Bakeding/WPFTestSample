using DataBinding.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding.Services
{
    public class SQLiteService
    {
        public string connstr = Path.Combine(Environment.CurrentDirectory, "AAA.db");//没有数据库会创建数据库
        public SQLiteConnection db;
        public SQLiteService()
        {
            db = new SQLiteConnection(connstr);
            //db.CreateTable<AAA>();//表已存在不会重复创建

        }

        public int Add<T>(T model)
        {
            return db.Insert(model);
        }

        public int Update<T>(T model)
        {
            return db.Update(model);
        }

        public int Delete<T>(T model)
        {
            return db.Update(model);
        }
        public List<T> Query<T>(string sql) where T : new()
        {
            return db.Query<T>(sql);
        }
        public int Execute(string sql)
        {
            return db.Execute(sql);
        }

        
    }
}
