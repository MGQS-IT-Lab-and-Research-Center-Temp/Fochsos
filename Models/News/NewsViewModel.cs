namespace Fochso.Models.News
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Photo { get; set; }

    }
}
