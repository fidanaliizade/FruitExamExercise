using System.ComponentModel.DataAnnotations;

namespace FruitExamExercise.ViewModels.AccountVMs
{
	public class LoginVM
	{
        [Required]
        public string UsernameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
