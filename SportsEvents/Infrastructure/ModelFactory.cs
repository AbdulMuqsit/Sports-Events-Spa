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
                BeginDate=model.BeginDate,
                BeginTime=model.BeginTime,
                EndDate = model.EndDate,
                Address = new Address()
                {
                    CityId=model.CityId,
                    LineOne=model.LineOne,
                    LineTwo=model.LineTwo,
                    State=model.State,
                    Zip=model.Zip
                },
                Details = model.Details,
                EndTime = model.EndTime,
                ExternalLink = model.ExternalLink,
                VideoLink = model.VideoLink,
                IsFeatured = model.IsFeatured,
                SportId = model.SportId,
                CityId = model.CityId.Value,
                Zip = model.Zip,
                EventTypeId = model.EventTypeId
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
    }
}