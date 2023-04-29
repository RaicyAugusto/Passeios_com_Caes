using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PasseiosComCaes.Models
{
    public class AppUser : IdentityUser
    {
        
        public string? Cachorro { get; set; }
        public int? Pecorrido { get; set; }
        [ForeignKey("Endereco")]
        public int? EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Passeio> Passeios { get; set; }
    }
}
