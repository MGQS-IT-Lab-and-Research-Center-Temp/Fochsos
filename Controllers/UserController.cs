using AspNetCoreHero.ToastNotification.Abstractions;
using Fochso.Context;
using Fochso.Models.User;
using Fochso.Service.Implementation;
using Fochso.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fochso.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly FochsoContext _context;
        private readonly INotyfService _notyf;


        public UserController(IUserService userService, FochsoContext fochsoContext, INotyfService notyf)
        {
            _userService = userService;
            _context = fochsoContext;
            _notyf = notyf;
        }
        // GET: UserController
        //public ActionResult Index(string Id, string sort, string option)
        //{
        //    var response = _userService. ();
        //    ViewData["Message"] = response.Message;
        //    ViewData["status"] = response.Status;
        //    if (Id is not null)
        //    {
        //        return View(response.Data.Where(s => s.UserName!.Contains(Id.ToLower()) || s.RoleName!.Contains(Id.ToLower()) || s.Email!.Contains(Id.ToLower())).ToList());
        //        //_context.Teachers.Where(s => s.Name!.Contains(Id)).ToList());
        //    }
        //    if (sort is not null)
        //    {
        //        if (option == "UserName")
        //        {
        //            return View(response.Data.OrderBy(s => s.UserName!.ToLower()).ToList());
        //        }
        //        if (option == "Email")
        //        {
        //            return View(response.Data.OrderBy(s => s.Email!.ToLower()).ToList());
        //        }
        //    }
        //    return View(response.Data);
        //}

        // GET: UserController/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var response = _userService.GetUser(id);
            ViewData["Message"] = response.Message;
            ViewData["status"] = response.Status;
            if (response.Status is false)
            {
                return RedirectToAction("Index", "User");
            }
            return View(response.Data);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: UserController/Create
        //[HttpPost]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: UserController/Edit/5
        //[AllowAnonymous]
        //public ActionResult Update(string id)
        //{
        //    return View();
        //}

        //// POST: UserController/Edit/5
        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult Update(string id, UpdateUserViewModel updateUser)
        //{
        //    var response = _userService.UpdateUser(id, updateUser);

        //    if (response.Status is false)
        //    {
        //        _notyf.Error(response.Message);

        //        return View();
        //    }
        //    _notyf.Success(response.Message, 10);

        //    return RedirectToAction("Index", "User");
        //}

        // GET: UserController/Delete/5
        //[HttpPost]
        //public ActionResult Delete(string id, UpdateUserViewModel updateUser)
        //{
        //    var response = _userService.DeleteUser(id);

        //    if (response.Status is true)
        //    {
        //        _notyf.Success(response.Message, 10);

        //        return RedirectToAction("Index", "User");// Return an HTTP 200 OK response if the deletion is successful
        //    }
        //    _notyf.Error(response.Message);

        //    return RedirectToAction("Index", "User");
        
    }
}

