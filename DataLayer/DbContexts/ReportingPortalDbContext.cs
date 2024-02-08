using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbContexts
{
    public class ReportingPortalDbContext:DbContext
    {
        private readonly IConfiguration _configuration;

        public ReportingPortalDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Topics> Topics { get; set; }
        public DbSet<Fact> Facts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("ReportingPortalConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
