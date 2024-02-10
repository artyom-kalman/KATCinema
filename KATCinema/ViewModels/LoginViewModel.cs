using System.ComponentModel.DataAnnotations;

namespace KATCinema.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Адрес электронной почты")]
        [Required(ErrorMessage = "Введите адрес почты")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
