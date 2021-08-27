using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Portfolio.Entities;

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
        public DbSet<Entities.Movies> Movies { get; set; }
    }
}
