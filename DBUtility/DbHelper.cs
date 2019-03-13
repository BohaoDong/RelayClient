using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DBUtility
{
    public class DbHelper
    {
        /// <summary>
        /// 资源清理
        /// </summary>
        public void DisPoseResource()
        {
            Dbfactory = null;
            DbDataAdapter = null;
            Dt = null;
            Ds = null; 
        }
        #region 私有变量
        private static string dbProviderName = "System.Data.SqlClient"; //ConfigHelper.Config.CatiDbProvider;
        //public static string LinkaddressLength = System.Configuration.ConfigurationManager.AppSettings["LinkaddressLength"];//数据库类型
        private static string dbConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        //ConfigurationManager.AppSettings["Connstring"];//数据库链接
        private DbConnection connection;
        #endregion

        //公有静态方法，将SQL字符串里面的(')转换成('')，再在字符串的两边加上(')
        public static string GetQuotedString(String xStr)
        {
            return ("'" + GetSafeSqlString(xStr) + "'");
        }
        //公有静态方法，将SQL字符串里面的(')转换成('')
        private static string GetSafeSqlString(String xStr)
        {
            return xStr.Replace("'", "''");
        }
        public DbHelper()
        {
            connection = CreateConnection(dbConnectionString);
        }
        public DbHelper(string connectionString)
        {
            connection = CreateConnection(connectionString);
        }

        #region ExecuteNonQuery

        public int ExecuteNonQuery(string cmdText)
        {
            return ExecuteNonQuery(CommandType.Text, cmdText, null);
        }

        public int ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            return ExecuteNonQuery(cmdType, cmdText, null);
        }

        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = GetCommand(cmdText, commandParameters, cmdType);
            BeforeExecute(cmdText);
            ConnOpen(cmd.Connection);
            int ret = cmd.ExecuteNonQuery();
            ConnClosed(cmd.Connection);
            cmd.Parameters.Clear();
            OnSuccess(cmdText);
            return ret;
        }
        public int ExecuteNonQueryTry(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            try
            {
                DbCommand cmd = GetCommand(cmdText, commandParameters, cmdType);
                BeforeExecute(cmdText);
                ConnOpen(cmd.Connection);
                int ret = cmd.ExecuteNonQuery();
                ConnClosed(cmd.Connection);
                cmd.Parameters.Clear();
                OnSuccess(cmdText);
                return ret;
            }
            catch (Exception ex)
            {
                OnError(cmdText, ex);
            }
            return -1;
        }


        public int ExecuteNonQuery(CommandType cmdType, string cmdText, int commandTimeout, params DbParameter[] commandParameters)
        {

            DbCommand cmd = GetCommand(cmdText, commandParameters, cmdType);
            cmd.CommandTimeout = commandTimeout;
            BeforeExecute(cmdText);
            ConnOpen(cmd.Connection);
            int ret = cmd.ExecuteNonQuery();
            ConnClosed(cmd.Connection);
            cmd.Parameters.Clear();
            OnSuccess(cmdText);
            return ret;

        }
        public int ExecuteNonQueryTry(CommandType cmdType, string cmdText, int commandTimeout, params DbParameter[] commandParameters)
        {
            try
            {
                DbCommand cmd = GetCommand(cmdText, commandParameters, cmdType);
                cmd.CommandTimeout = commandTimeout;
                BeforeExecute(cmdText);
                ConnOpen(cmd.Connection);
                int ret = cmd.ExecuteNonQuery();
                ConnClosed(cmd.Connection);
                cmd.Parameters.Clear();
                OnSuccess(cmdText);
                return ret;
            }
            catch (Exception ex)
            {
                OnError(cmdText, ex);
            }
            return -1;
        }

        public int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = GetCommand(cmdText, commandParameters, cmdType);
            BeforeExecute(cmdText);
            cmd.Transaction = trans;
            ConnOpen(cmd.Connection);
            int ret = cmd.ExecuteNonQuery();
            ConnClosed(cmd.Connection);
            cmd.Parameters.Clear();
            OnSuccess(cmdText);
            return ret;
        }
        public int ExecuteNonQueryTry(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            try
            {
                DbCommand cmd = GetCommand(cmdText, commandParameters, cmdType);
                BeforeExecute(cmdText);
                cmd.Transaction = trans;
                ConnOpen(cmd.Connection);
                int ret = cmd.ExecuteNonQuery();
                ConnClosed(cmd.Connection);
                cmd.Parameters.Clear();
                OnSuccess(cmdText);
                return ret;
            }
            catch (Exception ex)
            {
                OnError(cmdText, ex);
            }
            return -1;
        }

        #endregion ExecuteNonQuery

        #region ExecuteScalar

        public object ExecuteScalar(string cmdText)
        {
            return ExecuteScalar(CommandType.Text, cmdText, null);
        }

        public object ExecuteScalar(CommandType cmdType, string cmdText)
        {
            return ExecuteScalar(cmdType, cmdText, null);
        }

        public object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = GetCommand(cmdText, commandParameters, cmdType);
            BeforeExecute(cmdText);
            ConnOpen(cmd.Connection);
            object val = cmd.ExecuteScalar();
            ConnClosed(cmd.Connection);
            cmd.Parameters.Clear();
            OnSuccess(cmdText);
            return val;
        }
        public object ExecuteScalarTry(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            try
            {
                DbCommand cmd = GetCommand(cmdText, commandParameters, cmdType);
                BeforeExecute(cmdText);
                ConnOpen(cmd.Connection);
                object val = cmd.ExecuteScalar();
                ConnClosed(cmd.Connection);
                cmd.Parameters.Clear();
                OnSuccess(cmdText);
                return val;
            }
            catch (Exception ex)
            {
                OnError(cmdText, ex);
            }
            return null;
        }

        public object ExecuteScalar(CommandType cmdType, string cmdText, int commandTimeout, params DbParameter[] commandParameters)
        {
            DbCommand cmd = GetCommand(cmdText, commandParameters, cmdType);
            cmd.CommandTimeout = commandTimeout;
            BeforeExecute(cmdText);
            ConnOpen(cmd.Connection);
            object val = cmd.ExecuteScalar();
            ConnClosed(cmd.Connection);
            cmd.Parameters.Clear();
            OnSuccess(cmdText);
            return val;
        }
        public object ExecuteScalarTry(CommandType cmdType, string cmdText, int commandTimeout, params DbParameter[] commandParameters)
        {

            try
            {
                DbCommand cmd = GetCommand(cmdText, commandParameters, cmdType);
                cmd.CommandTimeout = commandTimeout;
                BeforeExecute(cmdText);
                ConnOpen(cmd.Connection);
                object val = cmd.ExecuteScalar();
                ConnClosed(cmd.Connection);
                cmd.Parameters.Clear();
                OnSuccess(cmdText);
                return val;
            }
            catch (Exception ex)
            {
                OnError(cmdText, ex);
            }
            return null;
        }

        public object ExecuteScalar(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = GetCommand(cmdText, commandParameters, cmdType);
            cmd.Transaction = trans;
            BeforeExecute(cmdText);
            ConnOpen(cmd.Connection);
            object val = cmd.ExecuteScalar();
            ConnClosed(cmd.Connection);
            cmd.Parameters.Clear();
            OnSuccess(cmdText);
            return val;
        }
        public object ExecuteScalarTry(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            try
            {
                DbCommand cmd = GetCommand(cmdText, commandParameters, cmdType);
                cmd.Transaction = trans;
                BeforeExecute(cmdText);
                ConnOpen(cmd.Connection);
                object val = cmd.ExecuteScalar();
                ConnClosed(cmd.Connection);
                cmd.Parameters.Clear();
                OnSuccess(cmdText);
                return val;
            }
            catch (Exception ex)
            {
                OnError(cmdText, ex);
            }
            return null;
        }

        #endregion ExecuteScalar

        #region GetDataTable

        public DataTable GetDataTable(string cmdText)
        {
            return GetDataTable(CommandType.Text, cmdText, null);
        }

        public DataTable GetDataTable(CommandType cmdType, string cmdText)
        {
            return GetDataTable(cmdType, cmdText, null);
        }

        public DbProviderFactory Dbfactory;
        public DbDataAdapter DbDataAdapter;
        public DataTable Dt;
        public DataTable GetDataTable(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            Dbfactory = DbProviderFactories.GetFactory(dbProviderName);
            DbDataAdapter = Dbfactory.CreateDataAdapter();
            BeforeExecute(cmdText);
            DbDataAdapter.SelectCommand = GetCommand(cmdText, commandParameters, cmdType);
            Dt = new DataTable();
            DbDataAdapter.Fill(Dt);
            OnSuccess(cmdText);
            return Dt;
        }

        public DataTable GetDataTableTry(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            try
            {
                DbProviderFactory dbfactory = DbProviderFactories.GetFactory(dbProviderName);
                DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
                BeforeExecute(cmdText);
                dbDataAdapter.SelectCommand = GetCommand(cmdText, commandParameters, cmdType);
                DataTable dt = new DataTable();
                dbDataAdapter.Fill(dt);
                OnSuccess(cmdText);
                return dt;
            }
            catch (Exception ex)
            {
                OnError(cmdText, ex);
            }
            return new DataTable();
        }

        public DataTable GetDataTable(CommandType cmdType, string cmdText, int commandTimeout, params DbParameter[] commandParameters)
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            BeforeExecute(cmdText);
            dbDataAdapter.SelectCommand = GetCommand(cmdText, commandParameters, cmdType);
            dbDataAdapter.SelectCommand.CommandTimeout = commandTimeout;
            DataTable dt = new DataTable();
            dbDataAdapter.Fill(dt);
            OnSuccess(cmdText);
            return dt;
        }
        public DataTable GetDataTableTry(CommandType cmdType, string cmdText, int commandTimeout, params DbParameter[] commandParameters)
        {
            try
            {
                DbProviderFactory dbfactory = DbProviderFactories.GetFactory(dbProviderName);
                DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
                BeforeExecute(cmdText);
                dbDataAdapter.SelectCommand = GetCommand(cmdText, commandParameters, cmdType);
                dbDataAdapter.SelectCommand.CommandTimeout = commandTimeout;
                DataTable dt = new DataTable();
                dbDataAdapter.Fill(dt);
                OnSuccess(cmdText);
                return dt;
            }
            catch (Exception ex)
            {
                OnError(cmdText, ex);
            }
            return new DataTable();
        }

        public DataTable GetDataTable(CommandType cmdType, string cmdText, int pageIndex, int pageCount, params DbParameter[] commandParameters)
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            BeforeExecute(cmdText);
            dbDataAdapter.SelectCommand = GetCommand(cmdText, commandParameters, cmdType);

            DataTable dt = new DataTable();
            //取得分页数据
            int start = (pageIndex - 1) * pageCount;
            dbDataAdapter.Fill(start, pageCount, dt);
            OnSuccess(cmdText);
            return dt;
        }
        public DataTable GetDataTableTry(CommandType cmdType, string cmdText, int pageIndex, int pageCount, params DbParameter[] commandParameters)
        {
            try
            {
                DbProviderFactory dbfactory = DbProviderFactories.GetFactory(dbProviderName);
                DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
                BeforeExecute(cmdText);
                dbDataAdapter.SelectCommand = GetCommand(cmdText, commandParameters, cmdType);

                DataTable dt = new DataTable();
                //取得分页数据
                int start = (pageIndex - 1) * pageCount;
                dbDataAdapter.Fill(start, pageCount, dt);
                OnSuccess(cmdText);
                return dt;
            }
            catch (Exception ex)
            {
                OnError(cmdText, ex);
            }
            return new DataTable();
        }

        #endregion GetDataTable

        #region GetDataSet

        public DataSet GetDataSet(string cmdText)
        {
            return GetDataSet(CommandType.Text, cmdText, null);
        }

        public DataSet GetDataSet(CommandType cmdType, string cmdText)
        {
            return GetDataSet(cmdType, cmdText, null);
        }

        public DataSet Ds;
        public DataSet GetDataSet(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            Dbfactory = DbProviderFactories.GetFactory(dbProviderName);
            DbDataAdapter = Dbfactory.CreateDataAdapter();
            BeforeExecute(cmdText);
            DbDataAdapter.SelectCommand = GetCommand(cmdText, commandParameters, cmdType);
            Ds = new DataSet();
            DbDataAdapter.Fill(Ds);
            OnSuccess(cmdText);
            return Ds;
        }

        public DataSet GetDataSetTry(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            try
            {
                DbProviderFactory dbfactory = DbProviderFactories.GetFactory(dbProviderName);
                DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
                BeforeExecute(cmdText);
                dbDataAdapter.SelectCommand = GetCommand(cmdText, commandParameters, cmdType);
                DataSet ds = new DataSet();
                dbDataAdapter.Fill(ds);
                OnSuccess(cmdText);
                return ds;
            }
            catch (Exception ex)
            {
                OnError(cmdText, ex);
            }
            return new DataSet();
        }

        #endregion GetDataSet

        /// <summary>
        /// 初始化表
        /// </summary>
        /// <param name="tablename"></param>
        public void Truncate(string tablename)
        {
            string sql = "truncate table [" + tablename + "]";
            ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询表结构。
        /// </summary>
        /// <param name="tableName">要获取表结构的表名</param>
        /// <returns></returns>
        public DataTable GetTableStruct(string tableName)
        {
            string cmdText = "select top 1 * from [" + tableName + "]";
            DbCommand cmd = GetCommand(cmdText, new DbParameter[] { }, CommandType.Text);
            BeforeExecute(cmdText);
            ConnOpen(cmd.Connection);
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = reader.GetSchemaTable();
            ConnClosed(cmd.Connection);
            OnSuccess(cmdText);
            return dt;
        }
        public DataTable GetTableStructTry(string tableName)
        {
            string cmdText = "select top 1 * from [" + tableName + "]";
            try
            {
                DbCommand cmd = GetCommand(cmdText, new DbParameter[] { }, CommandType.Text);
                BeforeExecute(cmdText);
                ConnOpen(cmd.Connection);
                DbDataReader reader = cmd.ExecuteReader();
                ConnClosed(cmd.Connection);
                OnSuccess(cmdText);
                return reader.GetSchemaTable();
            }
            catch (Exception ex)
            {
                OnError(cmdText, ex);
            }
            finally
            {
                // Close();
            }
            return null;
        }

        #region 连接相关
        public static DbConnection CreateConnection(string connectionString)
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(dbProviderName);
            DbConnection dbconn = dbfactory.CreateConnection();
            dbconn.ConnectionString = connectionString;
            return dbconn;
        }

        private DbCommand GetCommand(string cmdText, DbParameter[] commandParameters, CommandType type)
        {
            DbCommand dbCommand = null;

            if (type == CommandType.Text)
            {
                dbCommand = GetSqlStringCommond(cmdText);
            }
            else
            {
                dbCommand = GetStoredProcCommond(cmdText);
            }

            if (commandParameters != null)
            {
                dbCommand.Parameters.AddRange(commandParameters);
            }

            return dbCommand;
        }
        private DbCommand GetStoredProcCommond(string storedProcedure)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = storedProcedure;
            dbCommand.CommandType = CommandType.StoredProcedure;
            return dbCommand;
        }
        private DbCommand GetSqlStringCommond(string sqlQuery)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.CommandType = CommandType.Text;
            return dbCommand;
        }
        /// <summary>
        /// 打开链接
        /// </summary>
        /// <param name="conn"></param>
        private void ConnOpen(DbConnection conn)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }
        /// <summary>
        /// 关闭链接
        /// </summary>
        /// <param name="conn"></param>
        private void ConnClosed(DbConnection conn)
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
        }

        #endregion

        #region 异常处理
        private const string BEFORE = "准备执行:";
        private const string SUCCESS = "执行成功:";
        private const string ERROR = "执行错误:";

        private void BeforeExecute(string cmdText)
        {
            //WriteLogHelper.DebugLog(typeof(DbHelper), BEFORE + cmdText);
        }

        private void OnSuccess(string cmdText)
        {
           // WriteLogHelper.DebugLog(typeof(DbHelper), SUCCESS + cmdText);
        }

        private void OnError(string cmdText, Exception ex)
        {
            this.connection.Close();
            //WriteLogHelper.ErrorLog(typeof(DbHelper), ERROR + cmdText, ex);
        }

        #endregion
        /// <summary>
        /// 使用SqlBulkCopy方式插入数据
        /// </summary> 
        public bool SqlBulkCopyInsert(DataTable dt, string tablename)
        {
            return SqlBulkCopyInsert(dbConnectionString, dt, tablename, 30);
        }

        public bool SqlBulkCopyInsert(DataTable dt, string tablename, int timeout)
        {
            return SqlBulkCopyInsert(dbConnectionString, dt, tablename, timeout);
        }



        /// <summary>
        /// 使用SqlBulkCopy方式插入数据
        /// </summary> 
        public bool SqlBulkCopyInsert(string connString, DataTable dt, string tablename, int timeout)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection))
                    {
                        connection.Open();
                        sqlBulkCopy.DestinationTableName = tablename;
                        sqlBulkCopy.BatchSize = dt.Rows.Count;
                        sqlBulkCopy.BulkCopyTimeout = timeout;
                        if (dt.Rows.Count != 0)
                        {
                            sqlBulkCopy.WriteToServer(dt);
                        }
                        sqlBulkCopy.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex) { return false; }
        } 
        ///
        ///获取datatable数据类型
        ///
        public string GetDtType(DataTable dt)
        { 
            string str = "";   
            foreach (DataColumn col in dt.Columns)
            { 
                str += col.ColumnName + ":" + col.DataType + ","; 
            }
            return str.TrimEnd(',');
        }
    }

    public class Trans : IDisposable
    {
        private DbConnection conn;
        private DbTransaction dbTrans;
        public DbConnection DbConnection
        {
            get { return conn; }
        }
        public DbTransaction DbTrans
        {
            get { return dbTrans; }
        }

        public Trans()
        {
            conn = DbHelper.CreateConnection("");
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }
        public Trans(string connectionString)
        {
            conn = DbHelper.CreateConnection(connectionString);
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }
        public void Commit()
        {
            dbTrans.Commit();
            Colse();
        }

        public void RollBack()
        {
            dbTrans.Rollback();
            Colse();
        }

        public void Dispose()
        {
            Colse();
        }

        public void Colse()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        } 
    } 
}