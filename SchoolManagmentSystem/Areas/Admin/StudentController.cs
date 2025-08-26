using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Infrastructure.Services;
using SchoolManagment.Application.Interface.Services;
using SchoolManagment.Domain.Entities;
using SchoolManagment.UI.Areas.Admin.ViewModels;
using System.Threading.Tasks;

namespace SchoolManagment.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;
        private readonly ISectionService _sectionService;

        public StudentController(
        IStudentService studentService,
        IClassService classService,
        ISectionService sectionService)
        {
            _studentService = studentService;
            _classService = classService;
            _sectionService = sectionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();
            var studentViewModels = students.Select(s => new StudentViewModel
            {
                Id = s.StudentId,
                RollNumber = s.RollNumber,
                FullName = s.User.FullName,
                Email = s.User.Email,
                DateOfBirth = s.DateOfBirth,
                ClassName = s.Class?.ClassName,  // check null
                Section = s.Section?.SectionName   // check null
            }).ToList();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            ViewBag.Classes = await _classService.GetClassesAsync();
            ViewBag.Sections = await _sectionService.GetSectionsAsync();
         
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Student = new Student
                {
                    RollNumber = model.RollNumber,
                    ClassId = model.ClassId,
                    SectionId = model.SectionId,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Address = model.Address,
                    GuardianName = model.GuardianName,
                    User = new User
                    {
                        FullName = model.FullName,
                        Email = model.Email,
                        PasswordHash = model.PasswordHash,
                        Role = "Student",
                        IsActive = true
                    }
                };
                await _studentService.AddStudentAsync(Student);
                return RedirectToAction("Index");
            }
                //model.Classes = await _classService.GetClassesAsync();
                //model.Sections = await _sectionService.GetSectionsAsync();
            

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentViewModel
            {
                RollNumber = student.RollNumber,
                ClassId = student.ClassId,
                SectionId = student.SectionId,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Address = student.Address,
                GuardianName = student.GuardianName,
                FullName = student.User.FullName,
                Email = student.User.Email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, StudentViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                {
                    return NotFound();
                }

                student.RollNumber = model.RollNumber;
                student.ClassId = model.ClassId;
                student.SectionId = model.SectionId;
                student.DateOfBirth = model.DateOfBirth;
                student.Gender = model.Gender;
                student.Address = model.Address;
                student.GuardianName = model.GuardianName;
                student.User.FullName = model.FullName;
                student.User.Email = model.Email;

                await _studentService.UpdateStudentAsync(student);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student); // Confirm delete view
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction("Index");
        }



    }
}
