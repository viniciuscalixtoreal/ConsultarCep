using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ConsultarCep.Models
{
    public class CepModel
    {
        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }

        [JsonPropertyName("complemento")]
        public string Complemento { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("localidade")]
        public string localidade { get; set; }

        [JsonPropertyName("uf")]
        public string Uf { get; set; }

        [JsonPropertyName("unidade")]
        public string Unidade { get; set; }

        [JsonPropertyName("ibge")]
        public string Ibge { get; set; }

        [JsonPropertyName("gia")]
        public string Gia { get; set; }

        private string _cep;

        [JsonPropertyName("cep")]
        public string Cep
        {
            get
            {
                return _cep.Replace("-", "");
            }
            set
            {
                _cep = value;
            }
        }
    }
}
