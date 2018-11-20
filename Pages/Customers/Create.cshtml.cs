using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using invoiceF.Models;

namespace invoiceF.Pages.Customers
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
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyCustomer = new Customer();

            if (await TryUpdateModelAsync<Customer>(
                emptyCustomer,
                "customer",   // Prefix for form value.
                s => s.LastName, s => s.FirstName, s => s.BusinessName, s => s.CustomerPhone, s => s.CustomerEmail))
            {
                _context.Customer.Add(emptyCustomer);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return null;
        }
    }
}