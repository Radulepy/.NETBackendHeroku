using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RazorMvc.Data;
using RazorMvc.Hubs;
using RazorMvc.Models;

namespace RazorMvc.Services
{
    public class InternshipDbService : IInternshipService
    {
        private readonly InternDbContext db;
        private readonly List<IAddMemberSubscriber> subscribers;
        private IConfiguration configuration;
        private Location defaultLocation;

        public InternshipDbService(InternDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.subscribers = new List<IAddMemberSubscriber>();
            this.configuration = configuration;
        }

        public Intern AddMember(Intern member)
        {
            if(member.Location == null)
            {
                member.Location = GetDefaultLocation();
            }
            db.Projects.AddRange((IEnumerable<Project>)member);
            db.SaveChanges();
            subscribers.ForEach(subscriber => subscriber.OnAddMember(member));
            return member;
        }

        private Location GetDefaultLocation()
        {
            if (defaultLocation == null)
            {
                var defaultLocationName = configuration["DefaultLocation"];
                defaultLocation = db.Locations.Where(_ => _.Name == defaultLocationName).OrderBy(_ => _.Id).FirstOrDefault();
            }

            return defaultLocation;
        }

        public InternshipClass GetClass()
        {
            throw new NotImplementedException();
        }

        public IList<Intern> GetMembers()
        {
            var interns = db.Interns.ToList();
            return interns;
        }

        public Intern GetMemberById(int id)
        {

            var intern = db.Find<Intern>(id);
            db.Entry(intern).Reference(_ => _.Location).Load();

            db.Entry(intern).Collection(_ => _.Projects).Load();

            var member = db.Find<Intern>(id);
            return member;
        }

        public void RemoveMember(int id)
        {
            var intern = db.Find<Intern>(id);
            if (intern == null)
                return;
            db.Remove<Intern>(intern);
            db.SaveChanges();
        }

        public void SubscribeToAddMember(IAddMemberSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void UpdateMember(Intern intern)
        {
          //  db.Projects.Update(intern);
            db.SaveChanges();
        }

        public void UpdateLocation(int id, int locationId)
        {
            var intern = db.Find<Intern>(id);
            var location = db.Find<Location>(locationId);
            intern.Location = location;
            db.SaveChanges();
        }
    }
}