using PasseiosComCaes.Data.Enum;
using PasseiosComCaes.Models;

namespace PasseiosComCaes.ViewModels
{
    public class CriarPasseiosViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Endereco Endereco { get; set; }
        public IFormFile Imagem { get; set; }
        public string AppUserId { get; set; }
    }
}
