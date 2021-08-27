using Microsoft.EntityFrameworkCore;
using Portfolio.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Repository
{
    public class PortfolioDbContext:DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
        {

        }

        //public DbSet<Entities.UserMaster.UserMaster> UserMasters { get; set; }
        //public DbSet<AssignedRoles> AssignedRoles { get; set; }
        //public DbSet<UserTokens> UserTokens { get; set; }
        //public DbSet<RoleMaster> RoleMasters { get; set; }
        public DbSet<Movies> Movies { get; set; }
    }
}
