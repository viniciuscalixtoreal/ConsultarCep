using ConsultarCep.Models;
using System.Collections.Generic;

namespace ConsultarCep.Repository.Interfaces
{
    public interface ICepRepository
    {
        IEnumerable<CepModel> GetAll();
        CepModel GetById(string cep);
        void Add(CepModel cep);
    }
}
