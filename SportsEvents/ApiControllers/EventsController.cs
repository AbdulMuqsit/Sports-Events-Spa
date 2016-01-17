using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SportsEvents.Models;
using SportsEvents.Common;
using SportsEvents.EntityFramework;

namespace SportsEvents.ApiControllers
{
    public class EventsController : ApiController
    {
        public IHttpActionResult Get()
        {
            var event_ = new Event() { Id = 0, Name = "ladsfds" };
            var context = new SportsEventsDbContext();


            return Ok(context.SaveChanges());

        }

       
    }
}