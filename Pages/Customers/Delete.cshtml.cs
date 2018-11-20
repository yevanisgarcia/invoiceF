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
    public class DeleteModel : PageModel
    {
        private readonly CustomerContext _context;

        public DeleteModel(CustomerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customer.AsNoTracking().FirstOrDefaultAsync(m => m.CustomerID == id);

            if (Customer == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.CustomerID == id);

            if (customer == null)
            {
                return NotFound();
            }

            try
            {
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }

            //Customer = await _context.Customer.FindAsync(id);

            //if (Customer != null)
            //{
            //    _context.Customer.Remove(Customer);
            //    await _context.SaveChangesAsync();
            //}

            //return RedirectToPage("./Index");
        }
    }
}
