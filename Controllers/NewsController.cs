using Fochso.Models.News;
using Fochso.Service.Implementation;
using Fochso.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fochso.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    { 
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        // GET: NewsController
        [AllowAnonymous]
        public ActionResult Index()
        {
            var response = _newsService.GetAllNews();
            ViewData["Message"] = response.Message;
            ViewData["status"] = response.Status;
            return View(response.Data);
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            var response = _newsService.GetNews(id);
            ViewData["Message"] = response.Message;
            ViewData["status"] = response.Status;
            if (response.Status is false)
            {
                return RedirectToAction("Index", "News");
            }
            return View(response.Data);
        }

        // GET: NewsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        public ActionResult Create(CreateNewsViewModel createNews, IFormFile imagePath)
        {
            var response = _newsService.CreateNews(createNews, imagePath);
            if (response.Status is false)
            {
                return View(createNews);
            }

            return RedirectToAction("Index", "News");
        }

        // GET: NewsController/Edit/5
        public ActionResult Update(int id)
        {
            var response = _newsService.GetNews(id);
            if (response.Status is false)
            {
                return RedirectToAction("Index", "News");
            }
            return View(response.Data);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        public ActionResult Update(int id, UpdateNewsViewModel updateNews)
        {
            var response = _newsService.UpdateNews(id, updateNews);

            if (response.Status is false)
            {
                return View();
            }

            return RedirectToAction("Index", "News");
        }

        // GET: NewsController/Delete/5
      

        // POST: NewsController/Delete/5
        [HttpDelete]
        public ActionResult Delete([FromRoute] int id)
        {
            var record = _newsService.GetNews(id);
            var response = _newsService.DeleteNews(id);
            if( record != null)
            {
                return View(response);
            }
            return RedirectToAction("Index", "News");  
        }
    }
}
