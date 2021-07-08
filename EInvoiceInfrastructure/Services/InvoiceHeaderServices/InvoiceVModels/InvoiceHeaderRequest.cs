using EInvoiceCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EInvoiceInfrastructure.Services.InvoiceHeaderServices.InvoiceVModels
{
    public class InvoiceHeaderRequest
    {
        [Required]
        public int InternalId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public DateTime InvoiceDate { get; set; }
        public decimal TaxValue { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetTotal { get; set; }
        public ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}
