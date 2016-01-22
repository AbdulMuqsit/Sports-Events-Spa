using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SportsEvents.Models
{
    public class CityPostViewModel
    {
        [Display(Name = "City Name")]
        [Required(ErrorMessage="{0} is Required")]
        public string Name { get; set; }

        [Display(Name = "Country Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int? CountryId { get; set; }
    }
}