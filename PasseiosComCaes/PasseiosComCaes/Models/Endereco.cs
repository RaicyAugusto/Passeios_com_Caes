using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PasseiosComCaes.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        public string Bairro { get; set; } 
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
    }
}
