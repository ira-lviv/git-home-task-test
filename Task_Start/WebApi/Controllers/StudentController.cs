using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Services;
using WebApi.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
   
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;
        private readonly IAuthorizationService _authorizationService;

        public StudentController(StudentService studentService, IAuthorizationService authorizationService)
        {
            _studentService = studentService;
            _authorizationService = authorizationService;
        }

      
        [HttpGet]
        public ViewResult Students()
        {
            var model = _studentService.GetAllStudents().Select(student =>StudentDto.FromModel(student));
            return View(model);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var student = _studentService.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }
            var result = await _authorizationService.AuthorizeAsync(User, student, "SameUserPolicy");
            if (result.Succeeded)
            {
                var model = StudentDto.FromModel(student);
                ViewBag.Action = "Edit";
                return View(model);
            }
            return Forbid();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(StudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Action = "Edit";
                return View("Edit", studentDto);
            }

            var result = await _authorizationService.AuthorizeAsync(User, studentDto, "SameUserPolicy");
            if (result.Succeeded)
            {
                _studentService.UpdateStudent(studentDto.ToModel());

                return RedirectToAction("Students");
            }
            return Forbid();
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult Create(StudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Action = "Create";
                return View("Edit", studentDto);
            }
            _studentService.CreateStudent(studentDto.ToModel());
            return RedirectToAction("Students");
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new StudentDto() {  };
            ViewBag.Action = "Create";
            return View("Edit", model);
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            _studentService.DeleteStudent(id);
            return RedirectToAction("Students");
        }
    }
}
