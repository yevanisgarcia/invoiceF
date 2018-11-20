using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using invoiceF.Models;
using invoiceF.Models.CustomerViewModels;
using Microsoft.EntityFrameworkCore;

namespace invoiceF.Pages
{
    public class AboutModel : PageModel
    {
        private readonly CustomerContext _context;

        public AboutModel(CustomerContext context)
        {
            _context = context;
        }

        public string DateSort { get; set; }
        public string CurrentSort { get; set; }

        public IList<InvoiceDateGroup> Invoice { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            CurrentSort = sortOrder;
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<InvoiceDateGroup> invoiceIQ =
                from invoices in _context.Invoice
                group invoices by invoices.InvoiceCreatedAt into dateGroup
                select new InvoiceDateGroup()
                {
                    InvoiceCreatedAt = dateGroup.Key,
                    InvoiceCount = dateGroup.Count()
                };

            switch (sortOrder)
            {
                case "Date":
                    invoiceIQ = invoiceIQ.OrderBy(s => s.InvoiceCreatedAt);
                    break;
                case "date_desc":
                    invoiceIQ = invoiceIQ.OrderByDescending(s => s.InvoiceCreatedAt);
                    break;
                default:
                    invoiceIQ = invoiceIQ.OrderBy(s => s.InvoiceCreatedAt);
                    break;
            }

            Invoice = await invoiceIQ.AsNoTracking().ToListAsync();
        }
    }
}