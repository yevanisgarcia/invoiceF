using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using invoiceF.Models;

namespace invoiceF.Pages.Invoices
{
    public class EditModel : PageModel
    {
        private readonly invoiceF.Models.CustomerContext _context;

        public EditModel(invoiceF.Models.CustomerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Invoice Invoice { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Invoice = await _context.Invoice.FindAsync(id);

            if (Invoice == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var invoiceToUpdate = await _context.Invoice.FindAsync(id);

            if (await TryUpdateModelAsync<Invoice>(
                invoiceToUpdate,
                "invoice",
                s => s.CustomerID, s => s.ServiceName, s => s.ServiceCost, s => s.ServiceDescription, s => s.InvoiceCreatedAt))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();

        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoice.Any(e => e.InvoiceID == id);
        }
    }
}
