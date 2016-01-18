using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SportsEvents.Common.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public bool IsFeatured { get; set; }
        public double? StartingPrice { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Address Address { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string Icon { get; set; }
        public List<string> Pictures { get; set; }
        public string VideoLink { get; set; }
        public string ExternalLink { get; set; }
        public DbGeography Coordinates { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
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
    public class EventType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Event> Enents { get; set; }

    }
    public class Address
    {
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }

        public string Zip { get; set; }
        public string State { get; set; }

    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public ICollection<Event> Events { get; set; }
        public int CountryId { get; set; }
    }

    public class Country

    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Event> Events { get; set; }

    }
}