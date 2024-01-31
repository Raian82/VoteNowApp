using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace VoteNowApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppRoles : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppRoles(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //List All Roles Created by Users
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            //Avoid Duplicate Role
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
              
            return RedirectToAction("Index");
        }
    }
}
