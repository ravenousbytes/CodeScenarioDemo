using AdministrationTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministrationTool.Data.Services
{
    public class AdministrationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
