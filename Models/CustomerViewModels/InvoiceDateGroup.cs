using System;
using System.ComponentModel.DataAnnotations;

namespace invoiceF.Models.CustomerViewModels
{
    public class InvoiceDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime InvoiceCreatedAt { get; set; }

        public int InvoiceCount { get; set; }
    }
}
