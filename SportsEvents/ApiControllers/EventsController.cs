using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SportsEvents.Models;

namespace SportsEvents.ApiControllers
{
    public class EventsController : ApiController
    {
        public IHttpActionResult Get()
        {
            var event_ = new Event() { Id = 0, Name = "ladsfds" };
            var auction = new Event() { Id = 12, Name = "GoldChest" };

            return Ok(new { auction, event_ });

        }

    }
}