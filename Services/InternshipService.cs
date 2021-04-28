using System.Linq;
using RazorMvc.Data;
using RazorMvc.Models;
using System.Collections.Generic;
using RazorMvc.Hubs;

namespace RazorMvc.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly InternshipClass _internshipClass = new();

        public void RemoveMember(int id)
        {
            var itemToBeDeleted = GetMemberById(id);
            _internshipClass.Members.Remove(itemToBeDeleted);
        }

        public Intern AddMember(Intern intern)
        {
            _internshipClass.Members.Add(intern);
            return intern;
        }

        public IList<Intern> GetMembers()
        {
            return _internshipClass.Members;
        }

        public void UpdateMember(Intern intern)
        {
            var itemToBeUpdated = GetMemberById(intern.Id)
;            itemToBeUpdated.Name = intern.Name;
        }

        public Intern GetMemberById(int id)
        {
            var member = _internshipClass.Members.Single(_ => _.Id == id);
            return member;
        }

        public void SubscribeToAddMember(IAddMemberSubscriber messageHub)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateLocation(int id, int locationId)
        {
            throw new System.NotImplementedException();
        }
    }
}