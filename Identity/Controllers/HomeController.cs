using Identity.Entities;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;  //create için kullanılır
        private readonly SignInManager<AppUser> _signInManager; //sign in için
        private readonly RoleManager<AppRole> _roleManager;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Create()
        {
            return View(new UserCreateModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Email = model.Email,
                    Gender = model.Gender,
                    UserName = model.UserName
                };


                var identityResult = await _userManager.CreateAsync(user, model.Password);
                if (identityResult.Succeeded)
                {
                    await _roleManager.CreateAsync(new()
                    {
                        Name = "Admin",
                        CreatedTime = DateTime.Now
                    });
                    await _userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("Index");
                }
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public IActionResult SignIn(string returnUrl)
        {

            return View(new UserSignInModel
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);  //1.false(rememberme) her seferinde signin yapmasn kayıtlı kalsın mı diye.Biz kalmasn dedik.2.si ise belirli  sayıda yanlış girişten sonra kilitlensin mi?
                if (signInResult.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                    {
                        return RedirectToAction(model.ReturnUrl);
                    }
                    
                    var role = await _userManager.GetRolesAsync(user);
                    if (role.Contains("Admin"))
                    {
                        return RedirectToAction("AdminPanel");

                    }
                    else
                    {
                        return RedirectToAction("Panel");
                    }

                }
    
            }
            return View(model);
        }

        [Authorize]
        public IActionResult GetUserInfo()
        {
            //giriş yapmış kullanıcıların görceği sayfa
            var userName = User.Identity.Name;
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            User.IsInRole("Member"); //Member mı? diye sorguladık
            return View();
        }
        [Authorize(Roles = "Admin")]

        public IActionResult AdminPanel()
        {
            return View();

        }
        [Authorize(Roles = "Member")]
        public IActionResult Panel()
        {
            return View();

        }
        [Authorize(Roles = "Member")]
        public IActionResult MemberPage()
        {
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}
