using INNOVATEQ.DATA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INNOVATEQ.DATA.Context
{
    public class INNOVATEQDBContext : DbContext
    {
        public INNOVATEQDBContext(DbContextOptions<INNOVATEQDBContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
