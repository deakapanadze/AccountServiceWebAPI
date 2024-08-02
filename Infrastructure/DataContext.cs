using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public  class DataContext : DbContext
    {
        public DbSet<Account> acounts {  get; set; }


        public DataContext(DbContextOptions<DataContext> options):base(options) {
        
        } 


        public DataContext() { }

    }
}
