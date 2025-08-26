using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagment.Application.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{
   
        public class ClassService : IClassService
        {
            private readonly StudentDbContext _context;

            public ClassService(StudentDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<SelectListItem>> GetClassesAsync()
            {
                return await _context.Classes
                    .Select(c => new SelectListItem
                    {
                        Value = c.ClassId.ToString(),
                        Text = c.ClassName
                    })
                    .ToListAsync();
            }
        }
    }

