using System.ComponentModel.DataAnnotations;

namespace Ahmetflix.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string? Email { get; set; }
    }
} 