
using Communication.Entity.Models.User.Identity;
using Communication.Entity.ViewModels.User;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;

namespace Communication.Client.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IValidator<RegisterVM> _validator;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IValidator<RegisterVM> validator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _validator = validator;
        }

        public IActionResult login() { return View(); }
        public IActionResult register() { return View();}

        [HttpPost]
        public async Task<IActionResult> login([FromForm]LoginVM loginVM) {
            if (loginVM.username == null || loginVM.password == null)
            {
                ViewData["validation"] = "* username or password cannot be blank.";
                return View();
            }
            var _user = await _userManager.FindByNameAsync(loginVM.username);
            if(_user == null)
            {
                ViewData["validation"] = "* username or password incorrect.";
                return View();
            }
            if (!(await _userManager.CheckPasswordAsync(_user,loginVM.password)))
            {
                ViewData["validation"] = "* username or password incorrect.";
                return View();
            }
            if((await _signInManager.PasswordSignInAsync(loginVM.username, loginVM.password, false, false)).Succeeded){
                return LocalRedirect("/Home/Index");
            }
            return RedirectToAction("login");
        }

        [HttpPost]
        public async Task<IActionResult> register([FromForm] RegisterVM registerVM) {
            AppUser user = new() { Id=Guid.NewGuid().ToString(), UserName = registerVM.username };
            string message = "";
            ValidationResult validationResult = await _validator.ValidateAsync(registerVM);
            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure error in validationResult.Errors)
                {
                    message += $"* {error.ErrorMessage} \\n";
                }
                ViewData["validation"] = message;

                return View();
            }

            if ((await _userManager.CreateAsync(user, registerVM.password)).Succeeded){
                if((await _signInManager.PasswordSignInAsync(user.UserName, registerVM.password, false, false)).Succeeded)
                {
                    return LocalRedirect("/Home/Index");
                }
            }
            ViewData["validation"] = "* Username already taken.";
            return View();
        }

        public async Task<IActionResult> logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }


    }
}
