using System.ComponentModel.DataAnnotations;

namespace SportsEvents.Common.Entities
{
    public class Address
    {
        [Display(Name = "Address Line One")]
        public string LineOne { get; set; }
        [Display(Name = "Address Line Two")]
        public string LineTwo { get; set; }
        [Display(Name = "City ID")]
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
    }
}