using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Services;
using WebApi.Dto;
using Microsoft.AspNetCore.Routing;
using WebApi.ViewModels;
using System;
using Models.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
   
    public class HomeTaskController : Controller
    {
        private readonly HomeTaskService _homeTaskService;
        private readonly StudentService _studentService;


        public HomeTaskController(HomeTaskService homeTaskService,StudentService studentService)
        {
            _homeTaskService = homeTaskService;
            _studentService = studentService;
        }

       
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Create(int courseId)
        {
            ViewData["CourseId"] = courseId;
            var model = new HomeTaskDto();
            ViewBag.Action = "Create";
            return View("Edit", model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(HomeTaskDto homeTaskDto,int courseId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CourseId"] = courseId;
                ViewBag.Action = "Create";
                return View("Edit", homeTaskDto);
            }
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", courseId);
            _homeTaskService.CreateHomeTask(homeTaskDto.ToModel());
            return RedirectToAction("Edit","Course",routeValueDictionary);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var homeTask = _homeTaskService.GetHomeTaskById(id);

            if (homeTask == null)
            {
                return NotFound();
            }

            var model = HomeTaskDto.FromModel(homeTask);
            ViewBag.Action = "Edit";
            
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(HomeTaskDto homeTaskDto)
        {
            if (homeTaskDto == null)
            {
                ViewBag.Action = "Edit";

                return View(homeTaskDto);
            }
            _homeTaskService.UpdateHomeTask(homeTaskDto.ToModel());
            var homeTask = _homeTaskService.GetHomeTaskById(homeTaskDto.Id);
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", homeTask.CourseId);
            return RedirectToAction("Edit", "Course", routeValueDictionary);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Delete(int homeTaskId, int courseId)
        {
            _homeTaskService.DeleteHomeTask(homeTaskId);
            return RedirectToAction("Students");
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", courseId);
            return RedirectToAction("Edit", "Course", routeValueDictionary);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Evaluate(int id)
        {
            var homeTask = _homeTaskService.GetHomeTaskById(id);
            if (homeTask == null)
            {
                return NotFound();
            }
            HomeTaskAssessmentViewModel assessmentViewModel = new HomeTaskAssessmentViewModel
            {
                Date = homeTask.Date,
                Description = homeTask.Description,
                Title = homeTask.Title,
                HomeTaskStudents = new List<HomeTaskStudentViewModel>(),
                HomeTaskId = homeTask.Id
            };
            if (homeTask.HomeTaskAssessments.Any())
            {
                foreach(var homeTaskAssessment in homeTask.HomeTaskAssessments)
                {
                    assessmentViewModel.HomeTaskStudents.Add(new HomeTaskStudentViewModel()
                    {
                        StudentFullName=homeTaskAssessment.Student.Name,
                        StudentId=homeTaskAssessment.Student.Id,
                        isComplete=homeTaskAssessment.IsComplete,
                        HomeTaskAssessmentId=homeTaskAssessment.Id

                    });
                }
            }
            else
            {
                foreach(var student in homeTask.Course.Students)
                {
                    assessmentViewModel.HomeTaskStudents.Add(new HomeTaskStudentViewModel()
                    {
                        StudentId=student.Id,
                        StudentFullName=student.Name
                    });
                }
            }
            return View(assessmentViewModel);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult SaveEvaluation(HomeTaskAssessmentViewModel model)
        {
            var homeTask = _homeTaskService.GetHomeTaskById(model.HomeTaskId);
            if (homeTask == null)
            {
                return NotFound();
            }
            foreach(var homeTaskStudent in model.HomeTaskStudents)
            {
                var target = homeTask.HomeTaskAssessments.Find(p => p.Id == homeTaskStudent.HomeTaskAssessmentId);
                if (target != null)
                {
                    target.Date = DateTime.Now;
                    target.IsComplete = homeTaskStudent.isComplete;
                }
                else
                {
                    var student = _studentService.GetStudentById(homeTaskStudent.StudentId);
                    homeTask.HomeTaskAssessments.Add(new HomeTaskAssessment
                    {
                        HomeTask = homeTask,
                        IsComplete=homeTaskStudent.isComplete,
                        Student=student,
                        Date=DateTime.Now
                    });
                }
                _homeTaskService.UpdateHomeTask(homeTask);
            }
            return RedirectToAction("Courses", "Course");
        }
        
    }
}
