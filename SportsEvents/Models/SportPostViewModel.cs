using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SportsEvents.Models
{
    public class SportPostViewModel
    {
        [Required(ErrorMessage="{0} is Required")]
        [Display(Name = "Sport Name")]
        public string Name { get; set; }
    }
}