using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsEvents.Models
{
    public class AcceptRegistrationRequestsModel
    {
        [Display(Name = "User Id")]
        [Required(ErrorMessage = "{0} is Required.")]
        public int userId { get; set; }

        [Display(Name = "Event id")]
        [Required(ErrorMessage = "{0} is Required.")]
        public int eventId { get; set; }

        [Display(Name = "User Name")]
        public string userName { get; set; }
    }
}