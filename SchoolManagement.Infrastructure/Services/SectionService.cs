using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagment.Application.Interface.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{
    public class SectionService : ISectionService
    {
        private readonly StudentDbContext _context;

        public SectionService(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<List<SelectListItem>> GetSectionsAsync()
        {
            var sections = await _context.Sections.ToListAsync();
            return sections.Select(s => new SelectListItem
            {
                Value = s.SectionId.ToString(),
                Text = s.SectionName
            }).ToList();
        }
    }
}
