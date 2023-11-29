using Identity.Context;
using Identity.Entities;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {

            var users = await _userManager.GetUsersInRoleAsync("Member");
            return View(users);
        }
        public IActionResult Create()
        {
            return View(new UserAdminCreateModel());
        }

        [HttpPost]

        public async Task<IActionResult> Create(UserAdminCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    Email = model.Email,
                    Gender = model.Gender,
                    UserName = model.UserName
                };
                var result = await _userManager.CreateAsync(user, model.UserName + "123");     //default olarak şifresi kullanıcıadı artı 123 olsun.Örn ferhat123
                if (result.Succeeded)
                {
                    var memberRole = await _roleManager.FindByNameAsync("Member");
                    if (memberRole == null)
                    {
                        await _roleManager.CreateAsync(new()
                        {
                            Name = "Member",
                            CreatedTime = DateTime.Now
                        });
                        await _userManager.AddToRoleAsync(user, "Member");
                        return RedirectToAction("Index");
                    }
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                }
                
            }
            return View(model);
        }
    }
}
