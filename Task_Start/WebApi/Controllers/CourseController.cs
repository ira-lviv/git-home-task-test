using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Services;
using WebApi.Dto;
using System;
using WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseService _courseService;
        private readonly StudentService _studentService;
        public CourseController(CourseService courseService, StudentService studentService)
        {
            _courseService = courseService;
            _studentService = studentService;
        }
       
        [HttpGet]
        public ViewResult Courses()
        {
            IEnumerable<CourseDto> model =_courseService.GetAllCourses().Select(course => CourseDto.FromModel(course));
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _courseService.GetCourseById(id);

            if (course == null)
            {
                return NotFound();
            }

            var model=CourseDto.FromModel(course);
            ViewBag.Action = "Edit";
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(CourseDto courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest();
            }
            _courseService.UpdateCourse(courseDto.ToModel());
            return RedirectToAction("Courses");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(CourseDto courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View("Edit", courseDto);
            }

            try
            {
                _courseService.CreateCourse(courseDto.ToModel());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View("Edit", courseDto);

            }
            return RedirectToAction("Courses");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CourseDto() { EndDate = DateTime.Now, StartDate = DateTime.Now };
            ViewBag.Action = "Create";
            return View("Edit", model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            _courseService.DeleteCourse(id);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AssignStudents(int id)
        {
            var students = _studentService.GetAllStudents();
            var course = _courseService.GetCourseById(id);
            if (course == null)
            {
                return BadRequest();
            }
            var model = new CourseStudentsAssignViewModel();
            model.Id = id;
            model.StartDate = course.StartDate;
            model.Name = course.Name;
            model.EndDate = course.EndDate;
            model.Students = new List<AssignementStudentViewModel>();
            foreach(var student in students)
            {
                bool isAssigned = course.Students.Any(p => p.Id == student.Id);
                model.Students.Add(new AssignementStudentViewModel() { StudentId = student.Id,IsAssigned=isAssigned,StudentFullName=student.Name }) ;
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AssignStudents(CourseStudentsAssignViewModel assignViewModel)
        {
            _courseService.SetStudentsToCourse(assignViewModel.Id,assignViewModel.Students.Where(p=>p.IsAssigned).Select(student=>student.StudentId));
            return RedirectToAction("Courses");
        }
    }
}
