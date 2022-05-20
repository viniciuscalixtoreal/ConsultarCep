using ConsultarCep.Models;
using ConsultarCep.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ConsultarCep.Repository.Repository
{
    public class CepRepository : DapperRepository, ICepRepository
    {
        private readonly IConfiguration _config;

        public CepRepository(IConfiguration config) : base(config.GetConnectionString("DefaultConnection"))
        {
            _config = config;
        }

        public IEnumerable<CepModel> GetAll()
        {
            using var dbConnection = Connection;
            dbConnection.Open();

            return dbConnection.Query<CepModel>("SELECT * FROM cep WITH(NOLOCK)");
        }

        public CepModel GetById(string cep)
        {
            using var dbConnection = Connection;
            var query = ("SELECT * FROM cep WITH(NOLOCK) WHERE cep = '" + cep + "'");
            dbConnection.Open();

            return dbConnection.Query<CepModel>(query).FirstOrDefault();
        }

        public void Add(CepModel obj)
        {
            using IDbConnection dbConnection = Connection;

            string query = "INSERT INTO [cep] (cep, logradouro, complemento, bairro, localidade, uf, unidade, ibge, gia)"
                + "VALUES(@cep, @logradouro, @complemento, @bairro, @localidade, @uf, @unidade, @ibge, @gia)";

            dbConnection.Open();
            dbConnection.Execute(query, obj);
        }
    }
}
