using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SchoolManagment.Domain.Entities
{
    public class Student
    {

        public int StudentId { get; set; }
        public int UserId { get; set; }
        public string RollNumber { get; set; }
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string GuardianName { get; set; }

        public User User { get; set; }
        public Class Class { get; set; }
        public Section Section { get; set; }
    }

}


