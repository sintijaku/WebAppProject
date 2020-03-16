using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Project.MODEL.Entities;
using Project.MVC.Models.ViewModels;
using Project.MVC.Services;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private IAppUserService _service;

        public AppUserController(IAppUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GettAllUsers()
        {
            var users = _service.GetAll();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public ActionResult<AppUser> GetUser(int id)
        {
            var user = _service.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(User);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int userId = _service.Add(new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            }).ID;
            return Ok(userId);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, EditViewModel model)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest();
            }

            var user = _service.GetById(id);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.IsActive = model.IsActive;
                _service.Update(user);
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var existingUser = _service.GetById(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            _service.Delete(id);
            return Ok();
        }

    }
}