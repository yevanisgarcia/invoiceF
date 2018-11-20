using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using invoiceF.Models;

namespace invoiceF.Pages.Invoices
{
    public class CreateModel : PageModel
    {
        private readonly CustomerContext _context;

        public CreateModel(CustomerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerName");

            return Page();
        }

        [BindProperty]
        public Invoice Invoice { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyInvoice = new Invoice();

            if (await TryUpdateModelAsync<Invoice>(
                emptyInvoice,
                "invoice",   // Prefix for form value.
                s => s.CustomerID, s => s.ServiceName, s => s.ServiceCost, s => s.ServiceDescription, s => s.InvoiceCreatedAt))
            {
                _context.Invoice.Add(emptyInvoice);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return null;

        }
    }
}