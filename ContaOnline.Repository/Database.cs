using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace OnlineBill.Repository
{
    public static class Database
    {
        private static string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jugimaujo\\source\\repos\\ContaOnline\\ContaOnline.Repository\\db_online_bill.mdf;Integrated Security=True";

        private static SqlConnection GetConnection()
        {   
            var connection = new SqlConnection(_connectionString);

            return connection;
        }

        public static T QueryEntity<T>(string storedProcedure, object param)
        {
            T result;

            using (var connection = GetConnection())
            {
                result = connection.QueryFirstOrDefault<T>(storedProcedure, param: param, commandType: CommandType.StoredProcedure);
            }

            return result;
        }

        public static IEnumerable<T> QueryCollection<T>(string storedProcedure, object param)
        {
            IEnumerable<T> result;

            using (var connection = GetConnection())
            {
                result = connection.Query<T>(storedProcedure, param: param, commandType: CommandType.StoredProcedure);
            }

            return result;
        }

        public static int Execute(string storedProcedure, object param)
        {
            int total;

            using (var connection = GetConnection())
            {
                total = connection.Execute(storedProcedure, param: param, commandType: CommandType.StoredProcedure);
            }

            return total;
        }
    }
}
