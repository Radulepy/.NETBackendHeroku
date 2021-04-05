using RaduMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaduMVC.Models
{
    public static class SeedData
    {
        public static void Initialize(InternDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Interns.Any())
            {
                return;   // DB has been seeded
            }

            var interns = new Intern[]
            {
                new Intern { Name = "A", DateOfJoin = DateTime.Parse("2021-04-01") },
                new Intern { Name = "B", DateOfJoin = DateTime.Parse("2021-04-01") },
                new Intern { Name = "C", DateOfJoin = DateTime.Parse("2021-03-31") },
            };

            context.Interns.AddRange(interns);
            context.SaveChanges();
        }
    }
}