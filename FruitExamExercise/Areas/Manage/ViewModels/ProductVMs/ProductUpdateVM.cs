namespace FruitExamExercise.Areas.Manage.ViewModels.ProductVMs
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public IFormFile Image { get; set; }
        public string?  ImgUrl { get; set; }
    }
}
