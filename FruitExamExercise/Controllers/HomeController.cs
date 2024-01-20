using FruitExamExercise.DAL;
using FruitExamExercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitExamExercise.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext _db;

		public HomeController(AppDbContext db)
        {
			_db = db;
		}
        public async Task<IActionResult> Index()
		{
			List<Product> products = await _db.Products.ToListAsync();
			return View(products);
		}
	}
}
