using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagment.Application.Interface.Services
{
    public interface IClassService
    {
        //ABC
        Task<IEnumerable<SelectListItem>> GetClassesAsync();

    }
}
