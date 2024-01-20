using FruitExamExercise.Areas.Manage.ViewModels.ProductVMs;
using FruitExamExercise.DAL;
using FruitExamExercise.Helpers;
using FruitExamExercise.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace FruitExamExercise.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _db.Products.ToListAsync();
            return View(products);
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(ProductCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //if (!vm.Image.CheckType("image/"))
            //{
            //    ModelState.AddModelError("Image", "Enter right image format.");
            //}
            //if (!vm.Image.CheckLength(3000))
            //{
            //    ModelState.AddModelError("Image", "Enter max 3mb image.");

            //}
            Product product = new Product()
            {
                Title = vm.Title,
                Category = vm.Category,
                ImgUrl = vm.Image.Upload(_env.WebRootPath, @"\Upload\Product\")
            };
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(int id)
        {
            Product product = await _db.Products.FindAsync(id);
            ProductUpdateVM updated = new ProductUpdateVM()
            {
                Id = product.Id,
                Title = product.Title,
                Category = product.Category,
                ImgUrl = product.ImgUrl
            };
            return View(updated);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var product = await _db.Products.FindAsync(vm.Id);
            if(product == null)
            {
                throw new Exception("Product is null.");
            }
            if (vm.Image != null)
            {
                if (!vm.Image.CheckType("image/"))
                {
                    ModelState.AddModelError("Image", "Enter right image format.");
                }
                if (!vm.Image.CheckLength(3000))
                {
                    ModelState.AddModelError("Image", "Enter max 3mb image.");

                }
            }
            if (vm.Image != null)
            {
                product.ImgUrl = vm.Image.Upload(_env.WebRootPath, @"\Upload\Product\");
            }
            if (vm.Id <= 0)
            {
                throw new Exception("Id is can not zero or negative.");
            }

            product.Title = vm.Title;
            product.Category = vm.Category;
            await _db.SaveChangesAsync(); 
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _db.Products.FindAsync(id);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
