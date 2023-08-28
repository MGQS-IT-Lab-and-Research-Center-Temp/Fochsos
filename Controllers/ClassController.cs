using Fochso.Context;
using Fochso.Entities;
using Fochso.Models.Class;
using Fochso.Repository.Interfaces;
using Fochso.Service.Implementation;
using Fochso.Service.Interface;
using Fochso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Fochso.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClassController : Controller
    {

        private readonly IClassService _classService;
        private readonly IClassRepository _classRepository;

        public ClassController(IClassService classService, IClassRepository classRepository)
        {
            _classService = classService;
            _classRepository = classRepository;
        }

        // GET: StudentController
        [AllowAnonymous]
        public IActionResult Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            var response = _classService.GetAllClass();
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

           var classes = from s in _classRepository.GetClasses()
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                classes = classes.Where(s => s.Name.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    classes = classes.OrderByDescending(s => s.Name);
                    break;
                case "Description":
                    classes = classes.OrderBy(s => s.Description);
                    break;
                case "date_desc":
                    classes = classes.OrderByDescending(s => s.DateCreated);
                    break;
                case "date":
                    classes = classes.OrderBy(s => s.DateCreated);
                    break;
                default:
                    classes = classes.OrderBy(s => s.Name);
                    break;
            }
            ViewData["Message"] = response.Message;
            ViewData["status"] = response.Status;
            int pageSize = 10;

            return View(PaginatedList<ClassViewModel>.Create(response.Data, pageNumber ?? 1, pageSize)); ;
        } 

        // GET: StudentController/Details/5
        public ActionResult GetClass(int id)
        {
            var response = _classService.GetClass(id);
            ViewData["Message"] = response.Message;
            ViewData["status"] = response.Status;
            if (response.Status is false)
            {
                return RedirectToAction("Index", "Class");
            }
            return View(response.Data);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        public ActionResult Create(CreateClassViewModel createClass)
        {
            var response = _classService.CreateClass(createClass);
            if (response.Status is false)
            {
                return View(createClass);
            }

            return RedirectToAction("Index","Class");
        }

        // GET: StudentController/Edit/5
        public ActionResult Update(int id)
        {
            var response = _classService.GetClass(id);
            if (response.Status is false)
            {
                return RedirectToAction("Index", "Class");
            }
            return View(response.Data);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        public ActionResult Update(int id, UpdateClassViewModel updateClass)
        {
            var response = _classService.UpdateClass(id, updateClass);

            if (response.Status is false)
            {
                return View();
            }

            return RedirectToAction("Index", "Class");
        }

        // GET: StudentController/Delete/5
      

        // POST: StudentController/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete([FromRoute] int id)
        {
            var record = _classService.GetClass(id);
            var response = _classService.DeleteClass(id);
           
            //if (response.Status is false)
            //{
            //    return View();
            //}
            return RedirectToAction("Index", "Class");
        }
    }
}
