using Fochso.Models;
using Fochso.Models.News;

namespace Fochso.Service.Interface
{
    public interface INewsService
    {
        BaseResponseModel CreateNews(CreateNewsViewModel createNews,IFormFile  filePath);
        BaseResponseModel DeleteNews(int newsId);
        BaseResponseModel UpdateNews(int newsId, UpdateNewsViewModel updateNews);
        NewsResponseModel GetNews(int newsId);
        NewsesResponseModel GetAllNews();
    }
}
