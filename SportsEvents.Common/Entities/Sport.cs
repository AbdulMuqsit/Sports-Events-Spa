 using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsEvents.Common.Entities
{
    public class Sport
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string Name { get; set; }
        public ICollection<Event> Events { get; set; }

    }
}