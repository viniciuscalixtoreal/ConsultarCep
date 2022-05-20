using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsultarCep.Repository.Repository
{
    public class DapperRepository : IDisposable
    {
        protected string _connectionString;

        public DapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
