using Fochso.Entities;
using Fochso.Models;
using Fochso.Models.News;
using Fochso.Models.Student;
using Fochso.Repository.Interfaces;
using Fochso.Service.Interface;
using System.Linq.Expressions;

namespace Fochso.Service.Implementation
{
    public class NewsService : INewsService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly string _uploadFolderPath;

        public NewsService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public BaseResponseModel CreateNews(CreateNewsViewModel createNews,IFormFile ImagePath)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var isNewsExist = _unitOfWork.Newses.Exists(s => s.Title == createNews.Title);
            if (isNewsExist)
            {
                response.Message = "News already exist!";
                return response;
            }
            if (createNews == null)
            {
                response.Message = "News field is required";
                return response;
            }
            string folder = "cars/cover/";
            createNews.ImagePath = UploadImage(folder, createNews.Photo);
            //var originalFiePath = Path.GetFileName(ImagePath.FileName);
            //var uniqueFileName = Path.GetRandomFileName();
            ////var uniqueFilePath = Path.Combine(@"C:\temp\", uniqueFileName);
            //using (var stream = System.IO.File.Create(uniqueFilePath))
            //{
            //     ImagePath.CopyTo(stream);
            //}
            var news = new News
                {
                    Title = createNews.Title,
                    Description = createNews.Description,
                    //ImagePath = ImagePath.ToString(),
                ImagePath = createNews.ImagePath,
                CreatedBy = createdBy,
                };
                //if (ImagePath != null && ImagePath.Length > 0)
                //{
                //    // Generate a unique file name for the image
                //    string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImagePath.FileName;
               
                //    // Determine the file path where you want to save the image
                //    string filePathway = Path.Combine(_uploadFolderPath, uniqueFileName);

                //    // Save the image to the specified file path
                //    using (var fileStream = new FileStream(filePathway, FileMode.Create))
                //    {
                //    ImagePath.CopyTo(fileStream);
                //    }

                //    // Set the image path in the news entity
                //    news.ImagePath = ImagePath;
                //}

            try
            {
                _unitOfWork.Newses.Create(news);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "News created successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create news at this time: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel DeleteNews(int newsId)
        {
            var response = new BaseResponseModel();

            Expression<Func<News, bool>> expression = (q => q.Id ==newsId);

            var isNewsExist = _unitOfWork.Newses.Exists(s => s.Id == newsId);

            if (!isNewsExist)
            {
                response.Message = "News does not exist";
                return response;
            }
              var news = _unitOfWork.Newses.Get(newsId);

            try
            {
                _unitOfWork.Newses.Remove(news);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "News successfully deleted.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"News delete failed: {ex.Message}";
                return response;
            }
        }

        public NewsesResponseModel GetAllNews()
        {
            var response = new NewsesResponseModel();
            try
            {
                var news = _unitOfWork.Newses.GetAll();

                if (news is null)
                {
                    response.Status = false;
                    response.Message = "No news found";
                    return response;
                }
                response.Data = news.Select(
                    news => new NewsViewModel
                    {
                        Id = news.Id,
                        Title = news.Title,
                        Description = news.Description
                    }).ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            return response;
        }

        public NewsResponseModel GetNews(int newsId)
        {

            var response = new NewsResponseModel();

            Expression<Func<News, bool>> expression = s =>
                                              (s.Id == newsId);
            var isNewsExist = _unitOfWork.Newses.Exists(expression);

            if (!isNewsExist)
            {
                response.Status = false;
                response.Message = $"News with id {newsId} does not exist.";
                return response;
            }
            var news = _unitOfWork.Newses.Get(newsId);
            response.Status = true;
            response.Message = "Success";
            response.Data = new NewsViewModel
            {
                Id = newsId,
                Title = news.Title,
                Description = news.Description,
               ImagePath = news.ImagePath,
            };
            return response;
    }

        public BaseResponseModel UpdateNews(int newsId, UpdateNewsViewModel updateNews)
        {
            var response = new BaseResponseModel();
            string modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var isNewsExist = _unitOfWork.Newses.Exists(expression => expression.Id == newsId);

            if (!isNewsExist)
            {
                response.Message = "News does not exist";
                return response;
            }
            var news = _unitOfWork.Newses.Get(newsId);
            news.Title = updateNews.Title;
            news.Description = updateNews.Description;

            try
            {
                _unitOfWork.Newses.Update(news);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "News successfully updated";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }


        private string UploadImage(string folderPath, IFormFile photo)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + photo.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            photo.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
    }
}
