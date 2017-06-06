using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace ThreeLayoutVer2._0.Core
{
    public class Provider
    {
        static SqlConnection _conn;
        static string stringConnect;

        public static string StringConnect
        {
            get
            {
                return stringConnect;
            }

            set
            {
                stringConnect = value;
            }
        }

        public static SqlConnection Connection
        {
            get
            {
                return _conn;
            }
        }

        public string NewStringConnect(string nameServer, string nameDatabase = null, string username = null, string password = null)
        {
            string sDatabase = nameDatabase != null ? " ; Initial Catalog = " + nameDatabase : " ";
            string sUsername = username != null ? string.Format(" ; User ID =  {0} ; Password = {1} ", username, password) : " ; Integrated Security=True ";
            return string.Format("Data Source={0} {1} {2} ", nameServer, sDatabase, sUsername);
        }


        public SqlConnection NewConnecttion()
        {
            try
            {
                _conn = new SqlConnection(StringConnect);
                return _conn;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SqlConnection NewConnecttion(string stringConnect)
        {
            try
            {
                StringConnect = stringConnect;
                return new SqlConnection(StringConnect);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SqlConnection Open()
        {
            try
            {
                if (Connection == null)
                {
                    _conn = new SqlConnection(stringConnect);
                }
                if (Connection.State != System.Data.ConnectionState.Open)
                {
                    Connection.Open();
                }
                return Connection;
            }
            catch (Exception)
            {
                throw new Exception("Không thể mở kết nối!");
            }
        }

        public void Close()
        {
            try
            {
                if (Connection == null) return;
                if (Connection.State != System.Data.ConnectionState.Closed) Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetDataFromQuery(string query)
        {
            try
            {
                if (query == null) return null;
                DataTable dt = new DataTable();
                using (SqlDataAdapter _da = new SqlDataAdapter(query, Open()))
                {
                    _da.Fill(dt);
                    Close();
                }
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ExecQuery(string query)
        {
            int i = 0;
            try
            {
                using (SqlCommand _cmd = new SqlCommand(query, Open()))
                {
                    i = _cmd.ExecuteNonQuery();
                    Close();
                    return i;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ExecQueryWithValues(string query, SqlParameter[] values)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, Open()))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddRange(values);
                    int i = cmd.ExecuteNonQuery();
                    Close();
                    return i;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetId(string name_table)
        {
            return ExecQuery(string.Format(" DBCC CHECKIDENT ('[{0}]',RESEED,0) ", name_table)) > 0;
        }
    }
}