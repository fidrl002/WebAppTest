using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppTest.Models.CodeFirst;

namespace WebAppTest.Data
{
    public class WebAppTestContext : DbContext
    {
        public WebAppTestContext (DbContextOptions<WebAppTestContext> options)
            : base(options)
        {
        }

        public DbSet<WebAppTest.Models.CodeFirst.ExampleItem> ExampleItem { get; set; } = default!;
    }
}
