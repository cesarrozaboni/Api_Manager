using System.ComponentModel.DataAnnotations;

namespace Manager.Api.Controllers.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O login nao pode ser vazio")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha nao pode ser vazio")]
        public string Password { get; set; }
    }
}
