using System.ComponentModel.DataAnnotations;

namespace SportsEvents.Common.Entities
{
    public class ContactDetails
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Address BillingAddress { get; set; }
        public string Phone { get; set; }



    }
}