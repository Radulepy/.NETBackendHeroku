using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaduMVC.Models
{
    public class InternshipClass
    {

        private List<Intern> _members;
        public InternshipClass()
        {
            _members = new List<Intern>();
        }

        public IList<Intern> Members
        {
            get { return _members; }
        }
    }
}
