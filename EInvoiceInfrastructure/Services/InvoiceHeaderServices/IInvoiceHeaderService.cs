using EInvoiceCore.Entities;
using EInvoiceInfrastructure.Services.InvoiceHeaderServices.InvoiceVModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EInvoiceInfrastructure.Services.InvoiceHeaderServices
{
    public interface IInvoiceHeaderService
    {
        Task<IEnumerable<InvoiceHeader>> GetAll();
        Task Create(InvoiceHeaderRequest invoiceHeader);
    }
}
