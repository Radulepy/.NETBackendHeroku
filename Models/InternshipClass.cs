using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaduMVC.Models
{
    public class InternshipClass
    {

        private List<string> _members;
        public InternshipClass()
        {
            _members = new List<string>
            {
                "Borys",
                "Liova",
                "Orest",
            };
        }

        public IList<string> Members
        {
            get { return _members; }
        }
    }
}
