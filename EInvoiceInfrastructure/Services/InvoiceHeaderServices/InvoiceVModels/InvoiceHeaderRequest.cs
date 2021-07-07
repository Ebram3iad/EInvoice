using EInvoiceCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoiceInfrastructure.Services.InvoiceHeaderServices.InvoiceVModels
{
    public class InvoiceHeaderRequest
    {
        public int InternalId { get; set; }
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TaxValue { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetTotal { get; set; }
        public ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}
