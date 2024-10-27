using System.ComponentModel.DataAnnotations;

namespace Resunet.ViewModels
{
    public class RegisterViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-]).{10,}$",
            ErrorMessage = "Слишком простой пароль")]
        public string? Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password == "qwer1234")
            {
                yield return new ValidationResult("Слишком простой пароль", new[] { "Password" });
            }
        }
    }
}