using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlyphongGym.DAO
{
    public class DataProvider
    {
        private string cnStr = "Data Source=.\\sqlexpress;Initial Catalog=QL_PhongGym;Integrated Security=True";
        protected SqlConnection connection;
        protected SqlDataAdapter adapter;
        protected SqlCommand cmd;

        public void connect()
        {
            connection = new SqlConnection(cnStr);
            connection.Open();
        }

        public void disconnect()
        {
            connection.Close();
        }

        public IDataReader executeQuery(string query)
        {
            cmd = new SqlCommand(query, connection);
            return cmd.ExecuteReader();
        }

        public void executeNonQuery(string query, Byte[] img = null)
        {
            connect();
            cmd = new SqlCommand(query, connection);
            //cmd.Parameters.Add("@nndate", SqlDbType.DateTime).Value = ;
            if (img != null)
                cmd.Parameters.AddWithValue("@img", img);
            cmd.ExecuteNonQuery();
        }

        public object executeScalar(string query)
        {
            cmd = new SqlCommand(query, connection);
            return cmd.ExecuteScalar();
        }

        protected ArrayList ConvertDataSetToArrayList(DataSet dataset)
        {
            ArrayList arr = new ArrayList();
            DataTable dt = dataset.Tables[0];
            int i, n = dt.Rows.Count;
            for (i = 0; i < n; i++)
            {
                object obj = GetDataFromDataRow(dt, i);
                arr.Add(obj);
            }
            return arr;
        }

        protected virtual object GetDataFromDataRow(DataTable dt, int i)
        {
            return null;
        }
    }
}

