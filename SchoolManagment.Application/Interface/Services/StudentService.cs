using SchoolManagment.Application.StudentRepository;
using SchoolManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagment.Application.Interface.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        public StudentService(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }
        public async Task AddStudentAsync(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.RollNumber))
            {
                throw new ArgumentNullException("Roll Number is Required");
            }
            await _studentRepo.AddStudentAsync(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            

             await _studentRepo.DeleteStudentAsync(id);
        }

        public Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
           var getallstudent = _studentRepo.GetAllStudentsAsync();
            return getallstudent;
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Student ID");
            }
            return await _studentRepo.GetStudentByIdAsync(id);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            if (student.StudentId <= 0)
                throw new ArgumentException("Invalid Student ID");

            await _studentRepo.UpdateStudentAsync(student);
        }
    }
}
