using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsEvents.Common.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public ICollection<Event> Events { get; set; }
        public int CountryId { get; set; }
    }
}