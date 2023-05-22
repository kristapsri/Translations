using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TranslationsAdmin.Services
{
    public interface ISqlServerConnection
    {
        Task<IEnumerable<T>> Execute<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        Task<IEnumerable<T>> Execute<T>(string storedProcedure, T parameters, string connectionId = "Default");
        Task<IEnumerable<T>> Execute<T>(string storedProcedure, string connectionId = "Default");
    }

    public class SqlServerConnection : ISqlServerConnection
    {
        private readonly IConfiguration _config;

        public SqlServerConnection(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> Execute<T, U>(
            string storedProcedure,
            U parameters,
            string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<T>> Execute<T>(string storedProcedure, T parameters, string connectionId = "Default")
        {
            return await Execute<T, dynamic>(storedProcedure, parameters, connectionId);
        }
        public async Task<IEnumerable<T>> Execute<T>(string storedProcedure, string connectionId = "Default")
        {
            return await Execute<T, dynamic>(storedProcedure, new { }, connectionId);
        }
    }
}
