using System.ComponentModel.DataAnnotations;

namespace PasseiosComCaes.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Insira um endereço de Email")]
        public string Email { get; set; }
        [Display(Name = "Senha")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
