using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PasseiosComCaes.Data;
using PasseiosComCaes.Models;
using PasseiosComCaes.ViewModels;

namespace PasseiosComCaes.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDataContext _context;

        public ContaController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

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

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user != null)
            {   //Usuario encontrado, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //Password está correto, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Passeio");
                    }
                }
                //Password está correto
                TempData["Error"] = "Credenciais erradas. Por favor tente novamente";
                return View(loginViewModel);    
            }
            //Usuario não encontrado
            TempData["Error"] = "Credenciais erradas. Por favor tente novamente";
            return View(loginViewModel);
        }

        public IActionResult Registrar()
        {
            var response = new RegistroViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistroViewModel registroViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registroViewModel);
            }

            var userEmail = await _userManager.FindByEmailAsync(registroViewModel.Email);
            if (userEmail != null)
            {
                TempData["Error"] = "Este email já está e uso";
                return View(registroViewModel);
            }

            var userName = await _userManager.FindByNameAsync(registroViewModel.UserName);
            if (userName != null)
            {
                TempData["Error"] = "Este nome de usuario já existe";
                return View(registroViewModel);
            }

            var newUser = new AppUser
            {
                Email = registroViewModel.Email,
                UserName = registroViewModel.UserName,
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registroViewModel.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return RedirectToAction("Index", "Passeio");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Passeio");
        }
    }
}
