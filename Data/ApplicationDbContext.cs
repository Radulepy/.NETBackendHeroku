using System;
using System.Collections.Generic;
using System.Text;



namespace RaduMVC.Data
{
    public class ApplicationDbContext : InternDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}