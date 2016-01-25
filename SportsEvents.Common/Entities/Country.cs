using System.ComponentModel.DataAnnotations;

namespace SportsEvents.Common.Entities
{
    public class Country

    {
        public int Id { get; set; }
        [Required(ErrorMessage="{0} is required.")]
        public string Name { get; set; }

    }
}