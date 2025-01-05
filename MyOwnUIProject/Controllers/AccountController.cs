using DTOs;
using Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyOwnUIProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        [Route("/[controller]/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/[controller]/Register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();

                foreach (var item in ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                ViewBag.Errors = errors;
                return View();
            }

            ApplicationUser user = new ApplicationUser()
            {
                FullName = model.FullName,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.Phone,
                Address = model.Address
            };


            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }

                return View(model);
            }


            return RedirectToAction("Login");


        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
