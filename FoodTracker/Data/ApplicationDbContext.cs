using System;
using System.Collections.Generic;
using System.Text;
using FoodTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Data
{
	public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
		public DbSet<SubCategory> SubCategory { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<Food> Foods { get; set; }
		public DbSet<ApplicationUser> ApplicationUser { get; set; }
	}
}
