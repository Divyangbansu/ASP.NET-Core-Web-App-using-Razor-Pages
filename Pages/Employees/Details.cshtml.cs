using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using assignment4.Data;
using assignment4.Models;
using System.Drawing.Drawing2D;
using System.Composition;

namespace assignment4.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly assignment4.Data.northwindContext _context;

        public DetailsModel(assignment4.Data.northwindContext context)
        {
            _context = context;
        }

      public Employee Employee { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = from e in _context.Employees
                           where e.EmployeeId == id
                           select e;
            var r = _context.Employees.FirstOrDefault(e => e.EmployeeId == employee.FirstOrDefault().ReportsTo);

            ViewData["ReportsTO"] = r?.FirstName + " " + r?.LastName;
            if (employee == null)
            {
                return NotFound();
            }
            else 
            {
                //var r = from x in _context.Employees
                //        where x.EmployeeId == employee.FirstOrDefault().ReportsTo
                //        select x;
                //ViewData["ReportsTO"] = r.FirstOrDefault()?.FirstName + " " + r.FirstOrDefault()?.LastName; 
                Employee = await employee.FirstOrDefaultAsync();
            }
            return Page();
        }
    }
}
