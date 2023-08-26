using AspNetCoreHero.ToastNotification.Abstractions;
using Fochso.Models.Role;
using Fochso.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fochso.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly INotyfService _notyf;

        public RoleController(IRoleService roleService, INotyfService notyf)
        {
            _roleService = roleService;
            _notyf = notyf;
        }

        public ActionResult Index()
        {
            var roles =  _roleService.GetAllRole();

            ViewData["Message"] = roles.Message;
            ViewData["Status"] = roles.Status;

            return View(roles.Roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateRoleViewModel request)
        {
            var response =  _roleService.CreateRole(request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);

                return View(request);
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Role");
        }

        public IActionResult GetRoleDetail(int id)
        {
            var response =  _roleService.GetRole(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Role");
            }

            return View(response.Role);
        }

        public IActionResult Update(int id)
        {
            var response =  _roleService.GetRole(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Role");
            }

            var viewModel = new UpdateRoleViewModel
            {
                Id = response.Role.Id,
                RoleName = response.Role.RoleName,
                Description = response.Role.Description
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(int id, UpdateRoleViewModel request)
        {
            var response =  _roleService.UpdateRole(id, request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View(request);
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Role");
        }

        [HttpPost]
        public IActionResult DeleteRole([FromRoute] int id)
        {
            var response =  _roleService.DeleteRole(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Role"); ;
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Role");
        }
    }
}
