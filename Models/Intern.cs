using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaduMVC.Models
{
    public class Intern
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime RegistrationDateTime { get; set; }
        public DateTime DateOfJoin { get; internal set; }
    }
}