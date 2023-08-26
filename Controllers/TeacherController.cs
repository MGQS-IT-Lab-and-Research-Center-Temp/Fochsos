using Fochso.Models.Teacher;
using Fochso.Service.Implementation;
using Fochso.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fochso.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {

        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: StudentController
        public ActionResult Index()
        {
            var response = _teacherService.GetAllTeacher();
            ViewData["Message"] = response.Message;
            ViewData["status"] = response.Status;
            return View(response.Data);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            var response = _teacherService.GetTeacher(id);
            ViewData["Message"] = response.Message;
            ViewData["status"] = response.Status;
            if (response.Status is false)
            {
                return RedirectToAction("Index", "Teacher");
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTeacherViewModel createTeacher)
        {
            var response = _teacherService.CreateTeacher(createTeacher);
            if (response.Status is false)
            {
                return View(createTeacher);
            }

            return RedirectToAction("Index", "Teacher");
        }

        // GET: StudentController/Edit/5
        public ActionResult Update(int id)
        {
            var response = _teacherService.GetTeacher(id);
            if (response.Status is false)
            {
                return RedirectToAction("Index", "Teacher");
            }
            return View(response);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        public ActionResult Update(int id, UpdateTeacherViewModel updateTeacher)
        {
            var response = _teacherService.UpdateTeacher(id, updateTeacher);

            if (response.Status is false)
            {
                return View(response);
            }

            return RedirectToAction("Index", "Teacher");
        }

        // GET: StudentController/Delete/5

        // POST: StudentController/Delete/5
        [HttpDelete]
        public ActionResult Delete([FromRoute] int id)
        {
            var record = _teacherService.GetTeacher(id);
            var response = _teacherService.DeleteTeacher(id);
            if (response is null)
            {
                return View();
            }
            //if (response.Status is false)
            //{
            //    return View();
            //}
            return RedirectToAction("Index", "Teacher");
        }
    }
}
