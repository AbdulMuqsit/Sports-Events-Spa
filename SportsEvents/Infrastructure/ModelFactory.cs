using System;
using SportsEvents.Common.Entities;
using SportsEvents.Models;

namespace SportsEvents.Infrastructure
{
    public class ModelFactory
    {
        public static Event Get(EventPostViewModel model)
        {
            if (model == null)
            {
                return default(Event);
            }
            return new Event()
            {
                Description = model.Description,
                BeginDate = model.BeginDate,
                BeginTime = (DateTime) model.BeginTime,
                EndDate = model.EndDate,
                Address = new Address()
                {
                    CityId = model.CityId,
                    LineOne = model.LineOne,
                    LineTwo = model.LineTwo,
                    Zip = model.Zip
                },
                Details = model.Details,
                EndTime = (DateTime) model.EndTime,
                ExternalLink = model.ExternalLink,
                VideoLink = model.VideoLink,
                IsFeatured = model.IsFeatured,
                SportId = (int) model.SportId,
                CityId = model.CityId.Value,
                StartingPrice = model.StartingPrice,
                EventTypeId = (int) model.EventTypeId,

                Zip = model.Zip,
            };
        }

        public static City Get(CityPostViewModel model)
        {
            if (model == null)
            {
                return default(City);
            }
            return new City()
            {
                Name = model.Name,
                CountryId = model.CountryId.Value
            };
        }

        public static Country Get(CountryPostViewModel model)
        {
            if (model == null)
            {
                return default(Country);
            }
            return new Country()
            {
                Name = model.Name
            };
        }

        public static EventType Get(EventTypePostViewModel model)
        {
            if (model == null)
            {
                return default(EventType);
            }
            return new EventType()
            {
                Name = model.Name
            };
        }

        public static Sport Get(SportPostViewModel model)
        {
            if (model == null)
            {
                return default(Sport);
            }
            return new Sport()
            {
                Name = model.Name
            };
        }
    }
}