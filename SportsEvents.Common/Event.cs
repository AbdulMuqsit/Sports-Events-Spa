using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsEvents.Common
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public User Organizer { get; set; }
    }
}