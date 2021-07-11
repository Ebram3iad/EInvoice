using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceCore.Entities;
using EInvoiceInfrastructure.Services.InvoiceHeaderServices;
using EInvoiceInfrastructure.Services.InvoiceHeaderServices.InvoiceVModels;
using Microsoft.AspNetCore.Mvc;

namespace EInvoice.Web.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceHeaderService _invoiceHeaderService;

        public InvoiceController(IInvoiceHeaderService invoiceHeaderService)
        {
            _invoiceHeaderService = invoiceHeaderService;
        }
        public async Task<IActionResult> Index()
        {
            var invoiceHeaders = await _invoiceHeaderService.GetAll();
            return View(invoiceHeaders);
        }
       
        public async Task<IActionResult> CreateInvoice(InvoiceHeaderRequest model)
        {
            if (ModelState.IsValid && model.InvoiceLines != null)
            {
                await _invoiceHeaderService.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var invoice =await _invoiceHeaderService.IvoiceDetails(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }
    }
}