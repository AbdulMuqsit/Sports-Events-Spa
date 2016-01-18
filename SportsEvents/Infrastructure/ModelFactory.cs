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
            return new Event() { Description = model.Description };
        }
    }
}