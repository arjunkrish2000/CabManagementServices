using CabManagementSystem.Controllers;
using CabManagementSystem.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CabManagementSystem.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid details");
                return View(model);
            }

            var res = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (res.Succeeded)
            {
                //return RedirectToAction("Index", "Home", new {Area=""});
                //return Redirect("/");
                {
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "User", new { Area = "Admin" });
                    }
                    else
                    {
                        TempData["email"] = model.Email;
                        return RedirectToAction("Index", "Home", new { Area = "" });
                    }
                }
            }
            return View(model);
        }






        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = Guid.NewGuid().ToString().Replace("-", "")
            };

            var res = await _userManager.CreateAsync(user, model.Password);

            if (res.Succeeded)
                return RedirectToAction(nameof(Login));
            //{
            //    if (user.is)
            //    {
            //        return RedirectToAction("Index", "User", new { Area = "Admin" });

            //    }
            //    else
            //    {
            //        return RedirectToAction("Login", "Home", new { Area = "" });


            //    }


            //    var Task<bool> role = _userManager.GetRolesAsync(user, "Admin");

            //    foreach (var role in roles)
            //    {
            //        if (role == "Admin")
            //        {
            //            return RedirectToAction("Index", "User", new { Area = "Admin" });
            //        }
            //    }
            //}

            ModelState.AddModelError("", "An Error Occoured");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new {Area=""});
        }

        public async Task<IActionResult> GenerateData()
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "CabDriver" });

            var users = await _userManager.GetUsersInRoleAsync("Admin");
            if (users.Count == 0)
            {
                var appUser = new ApplicationUser()
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@admin.com",
                    UserName = "admin",
                    
                };
                var res = await _userManager.CreateAsync(appUser, "Pass@123");
                await _userManager.AddToRoleAsync(appUser, "Admin");
            }
            return Ok("Data generated");
        }


        public async Task<IActionResult> UserHome()
        {
            var signeduser = await _userManager.GetUserAsync(User);
            var user = await _userManager.FindByEmailAsync(signeduser.Email);



            return View(new RegisterViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                
            });
            
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult payment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> payment(book model)
        {
            return RedirectToAction("Reciept", "Home", new { id = "123" });
        }



        public IActionResult Reciept()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Reciept(book model)
        {
            return RedirectToPage("Home Page");
        }




        [HttpGet]
        public IActionResult BookCab()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> BookCab(book model)
        {
            if (!ModelState.IsValid)
                return View(model);



            await _db.books.AddAsync(new book()
            {
                BookDate = model.BookDate,
                from = model.from,
                to = model.to,  
                time = model.time,
                Fee = 100,
                userEmail = model.userEmail,
                
            });
            await _db.SaveChangesAsync();
            return RedirectToAction("payment", "Home", new { id = model.Id });
        }


    }
}
