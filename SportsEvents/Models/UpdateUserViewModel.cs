using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsEvents.Models
{
    public class UpdateUserViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only have capital or small alphabets.")]
        [MaxLength(20, ErrorMessage = "Maximum 20 Characters Allowed.")]
        [Display(Name = "First Name")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters required.")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only have capital or small alphabets.")]
        [MaxLength(20, ErrorMessage = "Maximum 20 Characters Allowed")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //address
        [MaxLength(120, ErrorMessage = "Maximum 120 Characters Allowed")]
        [Display(Name = "Line One")]
        [MinLength(10, ErrorMessage = "Minimum 10 characters required.")]
        public string LineOne { get; set; }

        [Display(Name = "Line Two")]
        [MaxLength(120, ErrorMessage = "Maximum 120 Characters Allowed")]
        public string LineTwo { get; set; }

        [Display(Name = "City")]
        public int? CityId { get; set; }

        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Zip")]
        [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Invalid zip code.")]
        public string Zip { get; set; }


    }
}