using KATCinema.Models;
using KATCinema.Utils.DBConnection;
using KATCinema.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KATCinema.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            // Поиск пользователя по email
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            // Проверка пользователя
            if (user == null)
            {
                TempData["Error"] = "Попробуйте ещё раз";
                return View(loginViewModel);
            }

            // Проверка пороля
            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (!isValidPassword)
            {
                TempData["Error"] = "Попробуйте ещё раз";
                return View(loginViewModel);
            }

            // Попытка пойти в систему
            var signInResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
            if (!signInResult.Succeeded)
            {
                TempData["Error"] = "Что-то пошло не так";
                return View(loginViewModel);
            }

            return RedirectToAction("Index", "Movie");
        }
    }
}
