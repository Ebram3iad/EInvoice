using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceInfrastructure.Services.CodeItemServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EInvoice.Web.Controllers
{
    public class CodeItemController : Controller
    {
        private readonly ICodeItemService _codeItemService;
        public CodeItemController(ICodeItemService codeItemService)
        {
            _codeItemService = codeItemService;
        }

        public async Task<IActionResult> Index()
        {
            var codeItems = await _codeItemService.GetAll();
            return View(codeItems);
        }
        public async Task<IActionResult>ImportDataFromExcelFile(IFormFile file)
        {
            if (file !=null)
            {
                await _codeItemService.AddDataFromExcelFile(file);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}