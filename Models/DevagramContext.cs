using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DevagramCSharp.Models
{
    public class DevagramContext : DbContext
    {
        public DevagramContext(DbContextOptions<DevagramContext> option) : base(option)
        {
        }

        public DbSet<Usuario> Usuarios { get;  set; }
    }
}