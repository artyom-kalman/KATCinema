using KATCinema.Models;
using KATCinema.Utils;
using KATCinema.Utils.DBConnection;
using KATCinema.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        [CustomAuthorizationFilter]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Reservation> reservations = _context.Reservations.Where(reservation => reservation.UserId == userId).
                Include(reservation => reservation.Session).
                Include(reservation => reservation.Session.Movie).ToList();
            return View(reservations);
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
            this.SignOut();

            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            // Поиск пользователя по email
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            // Проверка пользователя
            if (user == null)
            {
                TempData["Error"] = "Нет пользователя с таким email-адресом";
                return View(loginViewModel);
            }

            // Проверка пароля
            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (!isValidPassword)
            {
                TempData["Error"] = "Неверный пароль";
                return View(loginViewModel);
            }


            // Попытка войти в систему
            var signInResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password,
                isPersistent: true, lockoutOnFailure: false);
                
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
            
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        //public async Task<IActionResult>Reservation(int reservationId)
        //{
        //    List<ReservedSeat> reservedSeats = _context.ReservedSeats.Where(reservedSeat => reservedSeat.ReservationId == reservationId).ToList();
        //    return View(reservedSeats);
        //}
    }
}
