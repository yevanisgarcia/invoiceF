using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using invoiceF.Models;

namespace invoiceF.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly invoiceF.Models.CustomerContext _context;

        public EditModel(invoiceF.Models.CustomerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customer.FindAsync(id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customerToUpdate = await _context.Customer.FindAsync(id);

            if (await TryUpdateModelAsync<Customer>(
                customerToUpdate,
                "customer",
                s => s.LastName, s => s.FirstName, s => s.BusinessName, s => s.CustomerPhone, s => s.CustomerEmail))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();

        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerID == id);
        }
    }
}
