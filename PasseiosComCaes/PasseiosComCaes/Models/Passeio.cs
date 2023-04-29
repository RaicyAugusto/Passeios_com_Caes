using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasseiosComCaes.Models
{
    public class Passeio
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public DateTime Inicio { get; set; }
        [ForeignKey("Endereco")]
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
