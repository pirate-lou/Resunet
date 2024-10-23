using System.ComponentModel.DataAnnotations;

namespace Resunet.ViewModels
{
    public class RegisterViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Без email никуда не пущу")]
        [EmailAddress(ErrorMessage = "Формат поменяй")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        // [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-]).{10,}$",
        // ErrorMessage = "Слишком простой пароль")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{10,}$",
            ErrorMessage = "Слишком простой пароль")]
        public string? Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password == "qwerty123")
            {
                yield return new ValidationResult("Слишком простой пароль", new[] {"Password"});
            }
        }
    }
}