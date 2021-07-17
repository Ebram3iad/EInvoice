using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceCore.Entities;
using EInvoiceInfrastructure.Services.InvoiceHeaderServices;
using EInvoiceInfrastructure.Services.InvoiceHeaderServices.InvoiceVModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EInvoice.Web.Controllers
{
    public class InvoiceHeaderController : Controller
    {
        private readonly IInvoiceHeaderService _invoiceHeaderService;
        public InvoiceHeaderController(IInvoiceHeaderService invoiceHeaderService)
        {
            _invoiceHeaderService = invoiceHeaderService;
        }
        // GET: InvoiceHeader
        public async Task<ActionResult> Index()
        {
            var invoiceHeaders = await _invoiceHeaderService.GetAll();
            return View(invoiceHeaders);
        }

        // GET: InvoiceHeader/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InvoiceHeader/Create
        public async Task<ActionResult> Create()
        {
            var model = new InvoiceHeaderRequest()
            {
                CustomerName = "",
                InternalId = 0,
                InvoiceDate = DateTime.Now,
                TaxValue = 0,
                TotalAmount = 0,
                NetTotal = 0,
                InvoiceLines = new List<InvoiceLine> {  }
               
        };
            return View(model);
        }

        // POST: InvoiceHeader/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromBody] InvoiceHeaderRequest model)
        {
            try
            {
                if (ModelState.IsValid && model.InvoiceLines != null)
                {
                    await _invoiceHeaderService.Create(model);
                }
               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //return View(model);
                return View();
            }
        }
        public async Task<ActionResult> CreateInvoiceHeader()
        {
            var model = new InvoiceHeaderRequest()
            {
                CustomerName = "",
                InternalId = 0,
                InvoiceDate = DateTime.Now,
                TaxValue = 0,
                TotalAmount = 0,
                NetTotal = 0,
                InvoiceLines = new List<InvoiceLine> { }

            };
            return View(model);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInvoiceHeader(InvoiceHeaderRequest model)
        {
            if (ModelState.IsValid && model.InvoiceLines != null && model.InvoiceLines.Count != 0)
            {
                await _invoiceHeaderService.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        // GET: InvoiceHeader/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InvoiceHeader/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceHeader/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InvoiceHeader/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}