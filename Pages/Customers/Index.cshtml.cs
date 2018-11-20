using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using invoiceF.Models;

namespace invoiceF.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly CustomerContext _context;

        public IndexModel(CustomerContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Customer> Customer { get; set; }


        public async Task OnGetAsync(string sortOrder,
    string currentFilter, string searchString, int? pageIndex)
        {

            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CurrentFilter = searchString;

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<Customer> customerIQ = from s in _context.Customer
                                              select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                customerIQ = customerIQ.Where(s => s.LastName.Contains(searchString)
                                              || s.FirstName.Contains(searchString)
                                              || s.BusinessName.Contains(searchString)
                                              || s.CustomerPhone.Contains(searchString)
                                              || s.CustomerEmail.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    customerIQ = customerIQ.OrderByDescending(s => s.LastName);
                    break;

                default:
                    customerIQ = customerIQ.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 5;
            Customer = await PaginatedList<Customer>.CreateAsync(
                customerIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
    }
}
