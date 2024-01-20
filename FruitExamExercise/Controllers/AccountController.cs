using FruitExamExercise.Helpers;
using FruitExamExercise.Models;
using FruitExamExercise.ViewModels.AccountVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FruitExamExercise.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.roleManager = roleManager;
		}
        public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM vm) 
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			AppUser user = new AppUser()
			{
				Name = vm.Name,
				Surname = vm.Surname,
				Email = vm.Email,
				UserName = vm.UsernName
			};
			var result = await userManager.CreateAsync(user, vm.Password);
			if (!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}

			}
			await userManager.AddToRoleAsync(user, "Admin");
			return RedirectToAction("Login");
		}

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVM vm)
		{
			if(!ModelState.IsValid)
			{
				return View();
			}

			var user = await userManager.FindByEmailAsync(vm.UsernameOrEmail);
			if(user == null)
			{
				user = await userManager.FindByNameAsync(vm.UsernameOrEmail);
				if(user == null)
				{
					throw new Exception("UserName/Email adress or Password is incorrect.");
				}
			}
			var result = await signInManager.CheckPasswordSignInAsync(user, vm.Password, false);
			if (!result.Succeeded)
			{
				throw new Exception("UserName/Email adress or Password is incorrect.");
			}
			await signInManager.SignInAsync(user, false);
			return RedirectToAction("Index","Home");
		}

		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index","Home");
		}
		
		public async Task<IActionResult> CreateRole()
		{
			foreach (UserRole item in Enum.GetValues(typeof(UserRole)))
			{
				if(await roleManager.FindByNameAsync(item.ToString()) == null)
				{
					await roleManager.CreateAsync(new IdentityRole()
					{
						Name = item.ToString(),
					});

					
				}
			}
			return RedirectToAction("Index","Home");
		} 
	}
}
