using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using invoiceF.Models;

namespace invoiceF.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Last name cannot be longer than 30 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "First name cannot be longer than 30 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        [Required]
        [Display(Name = "Customer phone")]
        [Phone]
        public string CustomerPhone { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Full Name")]
        public string CustomerName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        //navigation property. Holds all the invoices for a customer
        public ICollection<Invoice> Invoices { get; set; }

    }
}
