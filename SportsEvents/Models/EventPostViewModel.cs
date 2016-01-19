using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsEvents.Models
{
    public class EventPostViewModel
    {
        [Display(Name = "Featured")]
        public bool IsFeatured { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "{0} is required")]
        public double? StartingPrice { get; set; }

        [Display(Name = "Starting Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "Ending Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Starting Time")]
        [Required(ErrorMessage = "{0} is required.")]
        public DateTime BeginTime { get; set; }

        [Display(Name = "Ending Time")]
        [Required(ErrorMessage = "{0} is required.")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Address Line One")]
        [Required(ErrorMessage = "{0} is required.")]
        public string LineOne { get; set; }

        [Display(Name = "Address Line Two")]
        public string LineTwo { get; set; }

        [Display(Name = "City ID")]
        [Required(ErrorMessage = "{0} is required.")]
        public int? CityId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string Zip { get; set; }

        public string State { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "Length exceeded. Maximum 150 characters allowed.")]
        public string Description { get; set; }

        [MaxLength(500, ErrorMessage = "Length exceeded. Maximum 500 characters allowed.")]
        public string Details { get; set; }

        public string Icon { get; set; }
        public List<string> Pictures { get; set; }
        public string VideoLink { get; set; }
        public string ExternalLink { get; set; }
        public int SportId { get; set; }
        public int EventTypeId { get; set; }
    }
}