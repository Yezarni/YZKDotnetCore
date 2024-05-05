using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static Yzk.share.AdoDotnetService;

namespace Yzk.share
{
    public class AdoDotnetService
    {
        private readonly string _connectionString;

        public AdoDotnetService()
        {
        }

        public AdoDotnetService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<M> Query<M>(string query, params AdoDotnetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Name, item.Value);
                }
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            string json = JsonConvert.SerializeObject(dt);
            List<M> lst = JsonConvert.DeserializeObject<List<M>>(json);
            return lst;
        }
        public M QueryFirstOrDefault<M>(string query,params AdoDotnetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Name, item.Value);
                }
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            string json = JsonConvert.SerializeObject(dt);
            List<M> lst = JsonConvert.DeserializeObject<List<M>>(json);
            return lst[0];
        }

        public int Execute(string query, params AdoDotnetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Name, item.Value);
                }
            }
            var result = cmd.ExecuteNonQuery();


            connection.Close();

            return result ;
        }

        public class AdoDotnetParameter
        {
            public AdoDotnetParameter() { }
            public AdoDotnetParameter(string name, object value)
            {
                Name = name;
                Value = value;
            }
            public string Name { get; set; }
            public object Value { get; set; }

        }
    }
}
