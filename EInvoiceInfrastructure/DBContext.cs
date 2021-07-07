using EInvoiceCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EInvoiceInfrastructure
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }
        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<CodeItem> CodeItems { get; set; }
        
    }
}
