namespace FruitExamExercise.Areas.Manage.ViewModels.ProductVMs
{
    public class ProductCreateVM
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public IFormFile? Image { get; set; }
    }
}
