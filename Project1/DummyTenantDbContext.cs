using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace testMultiTenants
{
    public sealed class DummyTenantDbContext : ITenantDbContext
    {
        public void OnModelCreating(ModelBuilder modelBuilder, DbContext context) { }
        public void SaveChanges(DbContext context) { }
    }
}
