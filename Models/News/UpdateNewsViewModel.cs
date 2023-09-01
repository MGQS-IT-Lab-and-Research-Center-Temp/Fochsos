using System.ComponentModel.DataAnnotations;

namespace Fochso.Models.News
{
    public class UpdateNewsViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "News's Id name is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

    }
}
