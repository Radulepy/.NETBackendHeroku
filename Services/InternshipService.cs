using RaduMVC.Models;
using System;

namespace RaduMVC.Services
{
    public class InternshipService
    {
        private readonly InternshipClass _internshipClass = new();

        public void RemoveMember(int index)
        {
            _internshipClass.Members.RemoveAt(index);
        }

        public string AddMember(string member)
        {
            _internshipClass.Members.Add(member);
            return member;
        }

        public void UpdateMember(int index, string newName)
        {
            _internshipClass.Members[index] = newName;
        }

        public InternshipClass GetClass()
        {
            return _internshipClass;
        }
    }
}