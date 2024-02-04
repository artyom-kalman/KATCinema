using System.ComponentModel.DataAnnotations;

namespace KATCinema.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Введите адрес почты")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
