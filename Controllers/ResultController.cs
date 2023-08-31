using Fochso.Models.Result;
using Fochso.Models.Student;
using Fochso.Service.Implementation;
using Fochso.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Fochso.Controllers
{
    public class ResultController : Controller
    {

        private readonly IResultService _resultService;

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        public IActionResult Index()
        {
            var response = _resultService.GetAllResult();
            ViewData["Message"] = response.Message;
            ViewData["status"] = response.Status;
            return View(response.Data);
        }

        public IActionResult StuDetails(int id)
        {
            var response = _resultService.GetResultsByStudentId(id);
 
            return View(response);
        }

        public ActionResult Details(int id)
        {
            var response = _resultService.GetResult(id);
            ViewData["Message"] = response.Message;
            ViewData["status"] = response.Status;
            if (response.Status is false)
            {
                return RedirectToAction("Index", "Result");
            }
            return View(response.Data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateResultViewModel createResult)
           {
            var response = _resultService.CreateResult(createResult);
            if (response.Status is false)
            {
                return View(createResult);
            }

            return RedirectToAction("Index", "Result");
        }


        public ActionResult Update(int id)
        {
            var response = _resultService.GetResult(id);
            if (response.Status is false)
            {
                return RedirectToAction("Index", "  Result");
            }
            return View(response);
        }

        [HttpPost]
        public ActionResult Update(int id, UpdateResultViewModel updateResult)
        {
            var mine= _resultService.GetResultsByStudentId(id);
            var response = _resultService.UpdateResult(id, updateResult);

            if (response.Status is false)
            {
                return View(response);
            }

            return RedirectToAction("Index", "Student");


        }

        public ActionResult Delete(int id,int studentId) 
        {
            //< a asp - action = "Delete" asp - route - studentId = "123" asp - route - resultId = "456" > Delete Result </ a >

            var mine = _resultService.GetResultsByStudentId(studentId);
            var response = _resultService.DeleteResult(id);

            if (response.Status == true)
            {
                // Successful deletion, redirect to a success view or action
                return RedirectToAction("Index", "Result"); // Replace with the appropriate route
            }
            else
            {
                return RedirectToAction("Index", "Result");
            }
        }
    }
}
