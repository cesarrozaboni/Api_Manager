using System.ComponentModel.DataAnnotations;

namespace Manager.Api.Controllers.ViewModel
{
    public class UpdateUserViewModel
    {
        [Required(ErrorMessage = "O Id não pode ser nulo")]
        [Range(1, long.MaxValue, ErrorMessage = "O Id deve ter no minimo 1 caracter")]
        public long Id { get; set; }

        [Required(ErrorMessage = "O Nome não pode ser nulo")]
        [MinLength(3, ErrorMessage = "O Nome deve ter no minimo 3 caracteres")]
        [MaxLength(80, ErrorMessage = "O nome deve ter no maximo 80 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Email não pode ser nulo")]
        [MinLength(3, ErrorMessage = "O Email deve ter no minimo 10 caracteres")]
        [MaxLength(80, ErrorMessage = "O Email deve ter no maximo 180 caracteres")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Senha não pode ser vazia")]
        [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres")]
        [MaxLength(15, ErrorMessage = "A senha deve ter no maximo 15 caracteres")]
        public string Password { get; set; }
    }
}
