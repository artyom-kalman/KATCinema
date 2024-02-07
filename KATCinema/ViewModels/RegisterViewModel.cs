using System.ComponentModel.DataAnnotations;

namespace KATCinema.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Адрес электронной почты")]
        [Required(ErrorMessage = "Введите адрес почты")]
        public string EmailAddress { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Повторите пароль")]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
