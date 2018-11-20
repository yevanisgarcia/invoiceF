using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using invoiceF.Models;

namespace invoiceF.Pages.Invoices
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

        public PaginatedList<Invoice> Invoice { get; set; }

        //public IList<Invoice> Invoice { get;set; }

        public async Task OnGetAsync(string sortOrder,
    string currentFilter, string searchString, int? pageIndex)
        {

            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            CurrentFilter = searchString;



            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }



            IQueryable<Invoice> invoiceIQ = from s in _context.Invoice
                                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                invoiceIQ = invoiceIQ.Where(s => s.ServiceName.Contains(searchString)
                                            || s.ServiceDescription.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    invoiceIQ = invoiceIQ.OrderByDescending(s => s.ServiceName);
                    break;
                case "Date":
                    invoiceIQ = invoiceIQ.OrderBy(s => s.InvoiceCreatedAt);
                    break;
                case "date_desc":
                    invoiceIQ = invoiceIQ.OrderByDescending(s => s.InvoiceCreatedAt);
                    break;
                default:
                    invoiceIQ = invoiceIQ.OrderBy(s => s.ServiceName);
                    break;
            }

            //this is needed for load data into the navigation property Customer,
            //so we can use this v to show the Customer name on Invoice
            //@Html.DisplayFor(modelItem => item.Customer.CustomerName)
            invoiceIQ = invoiceIQ.Include(s => s.Customer);


            int pageSize = 5;

            Invoice = await PaginatedList<Invoice>.CreateAsync(
                invoiceIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
