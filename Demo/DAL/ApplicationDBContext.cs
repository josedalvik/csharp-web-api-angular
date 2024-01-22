using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Demo.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Inbox> Inbox { get; set; }
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }
    }
}
