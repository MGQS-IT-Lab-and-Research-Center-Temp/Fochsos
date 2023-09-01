using System.ComponentModel.DataAnnotations;

namespace Fochso.Models.News
{
    public class CreateNewsViewModel
    {
        [Required(ErrorMessage = "News Title is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Photo { get; set; }

    }
}
