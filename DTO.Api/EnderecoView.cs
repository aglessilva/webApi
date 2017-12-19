using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DTO.Api
{
    public class Enderecos
    {
        [Key]
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Cep { get; set; }
        public string Localidade { get; set; }
        public string Complemento { get; set; } 

        public virtual Usuarios Usuario { get; set; }
    }

    public class Usuarios
    {
        public Usuarios()
        {
            this.Enderecos = new HashSet<Enderecos>();
        }

        [Key]
        [JsonProperty("idUsuario")]
        public int IdUsuario { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("documento")]
        public string Documento { get; set; }

        [JsonProperty("dataNascimento")]
        public Nullable<DateTime> DataNascimento { get; set; }

        [JsonProperty("sexo")]
        public string Sexo { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("senha")]
        public string Senha { get; set; }

        [JsonProperty("isAuthentication")]
        public bool IsAuthentication { get; set; }


        public virtual ICollection<Enderecos> Enderecos { get; set; }
    }
}
