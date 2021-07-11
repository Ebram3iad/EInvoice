using AutoMapper;
using EInvoiceCore.Entities;
using EInvoiceInfrastructure.EFRepository;
using EInvoiceInfrastructure.Services.InvoiceHeaderServices.InvoiceVModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoiceInfrastructure.Services.InvoiceHeaderServices
{
    public class InvoiceHeaderService : IInvoiceHeaderService
    {
        #region Fields

        private readonly DBContext _context;
        private readonly IRepository<InvoiceHeader> _invoiceHeaderRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public InvoiceHeaderService(IMapper mapper, DBContext context, IRepository<InvoiceHeader> invoiceHeaderRepository)
        {
            _invoiceHeaderRepository = invoiceHeaderRepository;
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public async Task Create(InvoiceHeaderRequest model)
        {
            try
            {
                model.NetTotal = await CalulateNetValue(model);
                var invoiceHeader = _mapper.Map<InvoiceHeader>(model);
                await _invoiceHeaderRepository.Create(invoiceHeader);
                await _invoiceHeaderRepository.Save();
            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<IEnumerable<InvoiceHeader>> GetAll()
        {
            try
            {
                var invoices = _context.InvoiceHeaders.Include(x => x.InvoiceLines).AsNoTracking().ToList();
                return invoices;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<InvoiceHeader> IvoiceDetails(int invoiceId)
        {
            var invoice = _context.InvoiceHeaders.Include(x => x.InvoiceLines.Where(i=> i.InvoiceID==invoiceId)).Where(i=> i.Id==invoiceId).AsNoTracking().FirstOrDefault();
            return invoice;
        }

        private async Task<decimal> CalulateNetValue(InvoiceHeaderRequest model)
        {
            if (await CalulateProductTotal(model)!=0)
            model.TotalAmount = await CalulateProductTotal(model);
            decimal taxValue = model.TotalAmount * model.TaxValue;
            return (model.TotalAmount - taxValue);
        }

        private async Task<decimal> CalulateProductTotal(InvoiceHeaderRequest model)
        {
            decimal totalAmount = 0;
            if (model.InvoiceLines != null && model.InvoiceLines.Count > 0)
                foreach (var item in model.InvoiceLines)
                {
                    item.Total = item.Quantity * item.Price;
                    totalAmount += item.Total;
                }
            return (totalAmount);
        }
    }
}
