using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SportsEvents.Models
{
    public class CountryPostViewModel
    {
        [Display(Name = "Country Name")]
        [Required(ErrorMessage = "{0} is Required")]
        public string Name { get; set; }
    }
}