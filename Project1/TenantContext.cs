using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testMultiTenants
{
    public abstract class TenantContext : DbContext
    {
        protected TenantContext(DbContextOptions options) : base(options)
        {
        }

        private TenantContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var svc = this.GetService();
            //svc.OnModelCreating(modelBuilder, this);
        }

        public override int SaveChanges()
        {
            //var svc = this.GetService();
            //svc.SaveChanges(this);
            return base.SaveChanges();
        }

        // rest goes here
    }
}
