using Microsoft.EntityFrameworkCore;
using RaduMVC.Models;
using System;
using System.Collections.Generic;
using System.Text;



namespace RaduMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}