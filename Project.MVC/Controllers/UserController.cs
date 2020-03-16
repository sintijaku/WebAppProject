using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.MODEL.Entities;
using Project.MVC.Models.ViewModels;
using Project.MVC.Services;

namespace Project.MVC.Controllers
{
    public class UserController : Controller
    {
        private IAppUserService _service;

        public UserController(IAppUserService service)
        {
            _service = service;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_service.GetAll());
        }

        [Authorize]
        [HttpGet]
        public IActionResult UserDetails(int id)
        {
            var user = _service.GetById(id);
            if (user == null)
            {
                return BadRequest();
            }
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("index", "user");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(AppUser user)
        {
            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"Please fill out all required fields";
                }
                if (user != null)
                {
                    _service.Add(user);
                }
            }
            else
            {
                return RedirectToAction("create", "user");
            }
            return RedirectToAction("Index", "User");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var user = _service.GetById(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User {user.ID} was not found";
                return View("NotFound");
            }
            if (user != null)
            {
                EditViewModel userEditViewModel = new EditViewModel
                {
                    ID = user.ID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = user.IsActive
                };
                return View(userEditViewModel);
            }
            return RedirectToAction("index", "user");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update(EditViewModel model)
        {
            var user = _service.GetById(model.ID);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User {user.ID} was not found";
                return View("NotFound");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Please fill out all required fields";
            }
            if (ModelState.IsValid)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.IsActive = model.IsActive;

                _service.Update(user);
                return RedirectToAction("Index", "User");
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var user = _service.GetById(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User {user.ID} was not found";
                return View("NotFound");
            }
            if (user != null)
            {
                _service.Delete(id);
                return RedirectToAction("index", "user");
            }
            return RedirectToAction("index", "user");
        }
    }
}