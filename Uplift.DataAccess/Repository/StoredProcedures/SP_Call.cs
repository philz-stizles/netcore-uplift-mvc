using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Uplift.DataAccess.Repository
{
    public class SP_Call: ISP_Call
    {
        private readonly DbContext Context;
        private static string ConnectionString = "";

        public SP_Call(DbContext context)
        {
            Context = context;
            ConnectionString = Context.Database.GetDbConnection().ConnectionString;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task ExecuteWithoutReturn<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                await sqlCon.ExecuteAsync(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return await sqlCon.QueryAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> ReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return (T)Convert.ChangeType(await sqlCon.ExecuteScalarAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure), typeof(T));
            }
        }
    }
}