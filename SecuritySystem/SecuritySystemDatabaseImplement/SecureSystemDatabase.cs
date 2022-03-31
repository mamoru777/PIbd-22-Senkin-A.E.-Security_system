using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace SecuritySystemDatabaseImplement
{
    class SecureSystemDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SecureSystemDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Secure> Secures { set; get; }
        public virtual DbSet<SecureComponent> SecureComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
    }
}
