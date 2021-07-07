using AutoMapper;
using EInvoiceCore.Entities;
using EInvoiceInfrastructure.Services.InvoiceHeaderServices.InvoiceVModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoice.Web.Helper
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {
            //CreateMap<Order, OrderResponse>();
            CreateMap<InvoiceHeaderRequest, InvoiceHeader>();
            //CreateMap<OrderItemsRequest, OrderItems>();
            //CreateMap<OrderItems, OrderItemsResponse>();
        }
    }
}
