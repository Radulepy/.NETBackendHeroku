using RaduMVC.Models;
using RaduMVC.Services;
using System.Linq;
using Xunit;

namespace RazorMvc.Tests
{
    public class InternshipServiceTests
    {
        [Fact]
        public void InitiallyContainsThreeMembers()
        {
            // Assume
            var intershipService = new InternshipService();

            // Act

            // Assert
            Assert.Equal(4, intershipService.GetClass().Members.Count);
        }

        [Fact]
        public void WhenAddMemberItShouldBeThere()
        {
            // Assume
            var intershipService = new InternshipService();
            var intern = new Intern();
            intern.Name = "Marko";

            // Act
            intershipService.AddMember(intern);

            // Assert
            Assert.Equal(4, intershipService.GetClass().Members.Count);
            Assert.Contains("Marko", intershipService.GetClass().Members.Select(member => member.Name));
        }
    }
}