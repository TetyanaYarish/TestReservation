using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservationTest.Models;

namespace ReservationTest.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Menus> Menus { get; set; }
        public DbSet<Meal> Meals { get; set; }

        public DbSet<Drink> Drinks { get; set; }

        //public DbSet<PhoneNumber> PhoneNumbers { get; set; }

    }
}
