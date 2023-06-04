using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using assignment4.Data;
using assignment4.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace assignment4.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly assignment4.Data.northwindContext _context;

        public IndexModel(assignment4.Data.northwindContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;
        public SelectList SalesPerson { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Empid { get; set; }


        public async Task OnGetAsync()
        {
            var genreQuery = from o in _context.Orders
                             select new { Name = o.Employee.FirstName + " " + o.Employee.LastName, o.Employee.EmployeeId,o.Employee.LastName };
                             
            SalesPerson = new SelectList(genreQuery.Distinct().OrderBy(o => o.LastName), "EmployeeId","Name");

            if (_context.Orders != null)
            {
                var order = from o in _context.Orders
                    .Where(o => o.Freight >= 250)
                    .Include(o => o.Employee)
                    .Include(o => o.ShipViaNavigation)
                            select o;

                if (Empid != 0)
                {
                    order = order.Where(o => o.EmployeeId == Empid);
                }
                Order = await order.AsNoTracking().ToListAsync();
            }
        }
    }
}
