using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SportsEvents.Common.Entities
{
    public class Event
    {
        public int Id { get; set; }
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
        [Required(ErrorMessage = "{0} is required")]
        public Address Address { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage = "Length exceeded. Maximum 150 characters allowed.")]
        public string Description { get; set; }
        [MaxLength(500, ErrorMessage = "Length exceeded. Maximum 500 characters allowed.")]
        public string Details { get; set; }
        public string Icon { get; set; }
        public List<Picture> Pictures { get; set; }
        public string VideoLink { get; set; }
        public string ExternalLink { get; set; }
        //[Required]
        public DbGeography Coordinates { get; set; }
        [Display(Name = "Starting Time")]
        [Required(ErrorMessage = "{0} is required.")]
        public DateTime BeginTime { get; set; }
        [Display(Name = "Ending Time")]
        [Required(ErrorMessage = "{0} is required.")]
        public DateTime EndTime { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string Zip { get; set; }

        public string AddressString { get; set; }
        public int SportId { get; set; }
        [ForeignKey("SportId")]
        public Sport Sport { get; set; }

        public string SportName { get; set; }

        public int EventTypeId { get; set; }


        [ForeignKey("EventTypeId")]
        public EventType EventType { get; set; }
        public string EventTypeName { get; set; }

        public string OrganizerId { get; set; }
        [ForeignKey("OrganizerId")]
        public User Organizer { get; set; }
        public string OrganizerName { get; set; }

        public ICollection<User> BookmarkerVisitors { get; set; }
        public ICollection<User> RegisterRequestVisitors { get; set; }
        public ICollection<User> RegisteredVisitors { get; set; }
        public ICollection<User> ClickerUsers { get; set; }
    }
    public class Advertisement
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public int Priority { get; set; }
        public string Keywords { get; set; }
        public bool Prelogin { get; set; }
    }
    public class Picture
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}