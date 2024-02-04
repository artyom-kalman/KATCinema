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

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "Эта почта уже используется";
                return View(registerViewModel);
            }

            var newUser = new User()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser,registerViewModel.Password);
            
            return View("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
