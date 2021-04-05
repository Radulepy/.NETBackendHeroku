using RaduMVC.Models;
using System;
using System.Linq;

namespace RaduMVC.Services
{
    public class InternshipService
    {
        private readonly InternshipClass _internshipClass = new();

        public void RemoveMember(int index)
        {
            _internshipClass.Members.RemoveAt(index);
        }

        public Intern AddMember(Intern intern)
        {
            _internshipClass.Members.Add(intern);
            return intern;
        }

        public void UpdateMember(int id, string memberName)
        {
            var itemToBeUpdated = _internshipClass.Members.Single(_ => _.Id == id);
            itemToBeUpdated.Name = memberName;
        }

        public InternshipClass GetClass()
        {
            return _internshipClass;
        }
    }
}