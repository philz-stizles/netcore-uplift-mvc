using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Uplift.DataAccess.Repository
{
    public interface ISP_Call: IDisposable
    {
        Task<IEnumerable<T>> ReturnList<T>(string procedureName, DynamicParameters param = null);
        Task ExecuteWithoutReturn<T>(string procedureName, DynamicParameters param = null);
        Task<T> ReturnScalar<T>(string procedureName, DynamicParameters param = null);
    }
}