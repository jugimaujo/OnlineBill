using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OnlineBill.UI.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Um nome deve ser informado.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Um email deve ser informado")]
        [EmailAddress(ErrorMessage = "Digite um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Uma senha deve ser informada.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Digite a confirmação de sua senha.")]
        [Compare("Password", ErrorMessage = "A confirmação deve ser igual a senha.")]
        public string ConfirmPassword { get; set; }
    }
}
