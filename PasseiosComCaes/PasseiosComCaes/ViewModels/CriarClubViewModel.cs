using PasseiosComCaes.Data.Enum;
using PasseiosComCaes.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PasseiosComCaes.ViewModels
{
    public class CriarClubViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Endereco Endereco { get; set; }
        public IFormFile Imagem { get; set; }
        public TamanhoDoClub TamanhoDoClub { get; set; }
        public string AppUserId { get; set; }
        
    }
}
