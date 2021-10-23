using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding.Services
{
    public class SQLiteDAO
    {
        #region 初始化
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string _connectionString;
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        SQLiteConnection connection = null;

        private SQLiteCommand cmd = new SQLiteCommand();
        private SQLiteDataAdapter da;
        private SQLiteTransaction tran;


        /// <summary>
        /// 数据库连接字符串
        /// 默认为：DefaultConnectionString
        /// </summary>
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString)) return Path.Combine(Environment.CurrentDirectory, "ACT.db");
                return _connectionString;
            }
            set { _connectionString = value; }
        }
        /// <summary>
        /// 连接数据库的属性
        /// </summary>
        private SQLiteConnection Connection
        {
            get
            {
                try
                {
                    if (connection == null)
                    {
                        connection = new SQLiteConnection(ConnectionString);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return connection;
            }
        }
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="path">
        public void CreateDatabase(string path)
        {
            try
            {
                SQLiteConnection.CreateFile(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 是否能连接数据库
        /// </summary>
        /// <returns></returns>
        public bool CanConnectDatabase()
        {
            try
            {
                connection = this.Connection;
                connection.Open();
            }
            catch
            {
                return false;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return true;
        }
        #endregion

        #region 查询数据库
        /// <summary>
        /// 执行查询语句(DataSet)
        /// </summary>
        /// <param name="sql">查询语句
        /// <returns>DataSet</returns>
        public DataSet QuerySqlReturnDataSet(string sql)
        {
            DataSet ds = new DataSet();

            try
            {
                cmd.Connection = Connection;
                cmd.CommandText = sql;

                da = new SQLiteDataAdapter(cmd);

                cmd.Connection.Open();

                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        /// <summary>
        /// 执行查询语句(DataTable)
        /// </summary>
        /// <param name="sql">查询语句
        /// <returns>DataTable</returns>
        public DataTable QuerySqlReturnDataTable(string sql)
        {
            try
            {
                if (QuerySqlReturnDataSet(sql).Tables.Count <= 0) return null;

                return QuerySqlReturnDataSet(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行查询语句(DataRow)
        /// </summary>
        /// <param name="sql">查询语句
        /// <returns>DataRow</returns>
        public DataRow QuerySqlReturnDataRow(string sql)
        {
            try
            {
                if (QuerySqlReturnDataTable(sql).Rows.Count <= 0) return null;

                return QuerySqlReturnDataTable(sql).Rows[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        /// <summary>
        /// 执行查询语句(Object)
        /// </summary>
        /// <param name="sql">查询语句
        /// <returns>object</returns>
        public object QuerySqlReturnObject(string sql)
        {
            try
            {
                object o = new object();

                cmd.Connection = Connection;
                cmd.CommandText = sql;

                cmd.Connection.Open();

                o = cmd.ExecuteScalar();

                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }


        /// <summary>
        /// 查询数据库返回DataReader
        /// </summary>
        /// <param name="sql">
        /// <returns></returns>
        public SQLiteDataReader QuerySqlReturnDataReader(string sql, SQLiteParameter[] parameters)
        {
            cmd = new SQLiteCommand();
            cmd.CommandText = sql;
            cmd.Connection = Connection;

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            try
            {
                cmd.Parameters.Clear();

                if (parameters != null && parameters.Length > 0)
                {
                    foreach (SQLiteParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex) { Connection.Close(); throw ex; }
        }
        /// <summary>
        /// 执行查询，返回第一行第一列
        /// </summary>
        /// <param name="sql">
        /// <param name="paramters">
        /// <returns></returns>
        public object ExecuteScalar(string sql, params SQLiteParameter[] parameters)
        {
            cmd = new SQLiteCommand();
            cmd.CommandText = sql;
            cmd.Connection = Connection;

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            try
            {
                cmd.Parameters.Clear();

                if (parameters != null && parameters.Length > 0)
                {
                    foreach (SQLiteParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                if (!sql.ToLower().Trim().StartsWith("insert into"))
                {
                    return cmd.ExecuteScalar();
                }
                else
                {
                    int count = cmd.ExecuteNonQuery();
                    if (count <= 0) return null;
                    cmd.CommandText = "select last_insert_rowid()";
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        #endregion

        #region 更新数据库
        /// <summary>
        /// 执行非查询语句
        /// </summary>
        /// <param name="sql">SQL语句
        /// <returns>Bool</returns>
        public bool ExecuteNonQuery(string sql)
        {
            try
            {
                cmd.Connection = Connection;
                cmd.CommandText = sql;

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                if (cmd.ExecuteNonQuery() < 0) return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        /// <summary>
        /// 执行非查询语句带参数
        /// </summary>
        /// <param name="sql">
        /// <param name="paramters">
        /// <returns></returns>
        public bool ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            try
            {
                cmd.CommandText = sql;
                cmd.Connection = Connection;

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                cmd.Parameters.Clear();

                if (parameters != null && parameters.Length > 0)
                {
                    foreach (SQLiteParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                if (cmd.ExecuteNonQuery() < 0) return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        #endregion

        #region 事务执行
        /// <summary>
        /// 开始事务
        /// </summary>
        public void DbBeginTransaction()
        {
            try
            {
                Connection.Open();

                this.tran = Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void DbCommitTransaction()
        {
            try
            {
                this.tran.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        /// <summary>
        /// 回滚事务失败
        /// </summary>
        public void DbRollbackTransaction()
        {
            try
            {
                this.tran.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        /// <summary>
        /// 执行事务查询数据库操作:返回第一行第一列数据
        /// </summary>
        /// <param name="sql">查询语句
        /// <returns>cid</returns>
        public object TranExecuteScalar(string sql, SQLiteParameter[] parameters)
        {
            try
            {
                cmd = new SQLiteCommand();
                cmd.Connection = Connection;
                cmd.CommandText = sql;
                cmd.Transaction = tran;

                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.Clear();

                    foreach (SQLiteParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                //更新记录数
                //object count = cmd.ExecuteScalar();
                //查询ID主键
                //cmd.CommandText = "SELECT @@IDENTITY";
                //return cmd.ExecuteScalar();

                if (!sql.ToLower().Trim().StartsWith("insert into"))
                {
                    return cmd.ExecuteScalar();
                }
                else
                {
                    int count = cmd.ExecuteNonQuery();
                    if (count <= 0) return null;
                    cmd.CommandText = "select last_insert_rowid()";
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行事务更新数据库操作
        /// </summary>
        /// <param name="sql">SQL语句
        public bool TranExecuteNonQuery(string sql, SQLiteParameter[] parameters)
        {
            try
            {
                cmd = new SQLiteCommand();
                cmd.Connection = Connection;
                cmd.CommandText = sql;
                cmd.Transaction = this.tran;

                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.Clear();

                    foreach (SQLiteParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                if (cmd.ExecuteNonQuery() < 0) return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
