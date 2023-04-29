using System.ComponentModel.DataAnnotations;

namespace PasseiosComCaes.ViewModels
{
    public class RegistroViewModel
    {
        [Display(Name = "Endereço de Email")]
        [Required(ErrorMessage = "Requer um endereço de email")]
        public string Email {get; set; }
        [Display(Name = "Nome de Usuario")]
        [Required(ErrorMessage = "Requer um Nome de Usuario")]
        public string UserName { get; set; }
        [Display(Name = "Senha")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirme sua Senha")]
        [Required(ErrorMessage = "Requer confirmar sua senha")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Senhas não se coincidem")]
        public string ConfirmePassword { get; set; }
    }
}
