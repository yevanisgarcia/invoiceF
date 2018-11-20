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
    public class DetailsModel : PageModel
    {
        private readonly CustomerContext _context;

        public DetailsModel(CustomerContext context)
        {
            _context = context;
        }

        public Invoice Invoice { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Invoice = await _context.Invoice
                                    .Include(i => i.Customer)
                                    .FirstOrDefaultAsync(m => m.InvoiceID == id);

            if (Invoice == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
