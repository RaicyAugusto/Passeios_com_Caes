using PasseiosComCaes.Models;

namespace PasseiosComCaes.ViewModels
{
    public class EditarPasseioViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public string URL { get; set; }
        public IFormFile Imagem { get; set; }
 
    }
}
