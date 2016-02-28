using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsEvents.Models
{
    public class RegistrationRequestsModel
    {
        [Display(Name = "Event Id")]
        [Required(ErrorMessage = "{0} is Required.")]
        public int Id { get; set; }
        

    }
}