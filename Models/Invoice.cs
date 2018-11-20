using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using invoiceF.Models;

namespace invoiceF.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Service name")]
        public string ServiceName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Service cost")]
        [DataType(DataType.Currency)]
        public decimal ServiceCost { get; set; }

        [MaxLength(1024)]
        [Display(Name = "Service description")]
        public string ServiceDescription { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Invoice created on")]
        public DateTime InvoiceCreatedAt { get; set; }

        //FK    
        public int CustomerID { get; set; }
        //navigation property
        public Customer Customer { get; set; }

    }
}
