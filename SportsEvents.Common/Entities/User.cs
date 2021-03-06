using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SportsEvents.Common.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [System.ComponentModel.DataAnnotations.Url]
        public string Link { get; set; }

        public Address Address { get; set; }
        public string OrganiztionName { get; set; }
        public string OrganizationDecription { get; set; }
        public string OrganaiztionLogo { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Event> RegisteredEvents { get; set; }
        public ICollection<Event> BookmarkedEvents { get; set; }
        public ICollection<Event> RegistrationRequests { get; set; }

        public ICollection<Event> ClickedEvents { get; set; }
        public ContactDetails ContactDetails { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;



        }
    }
}