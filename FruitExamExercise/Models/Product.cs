using FruitExamExercise.Models.BaseModel;
using System.Security;

namespace FruitExamExercise.Models
{
	public class Product:BaseEntity
	{
        public string Title { get; set; }
        public string Category { get; set; }
        public string? ImgUrl { get; set; }
    }
}
