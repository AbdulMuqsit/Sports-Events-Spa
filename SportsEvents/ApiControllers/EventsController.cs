﻿using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using SportsEvents.Infrastructure;
using SportsEvents.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using SportsEvents.Common.Entities;
using System.Collections.Generic;

namespace SportsEvents.ApiControllers
{
    [RoutePrefix("api/Events")]
    public class EventsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await DbContext.Events.ToListAsync());
        }
        [HttpGet]
        [Route("Calender/{page?}/{take?}")]
        public async Task<IHttpActionResult> GetCalender([FromUri]int page = 0, [FromUri]int take = 20)
        {
            return Ok(await DbContext.Events.Where(e => e.BeginDate > DateTime.UtcNow).OrderBy(e => e.BeginDate).Skip(page * take).Take(take).ToListAsync());
        }
        [HttpGet]
        [Route("MyEvents")]
        public async Task<IHttpActionResult> GetMyEvents()
        {
            var userId = User.Identity.GetUserId();
            var events = await DbContext.Events.Where(_event => _event.OrganizerId == userId).ToListAsync();
            return Ok(events);
        }
        
        [HttpGet]
        [Route("RegisteredEvents")]
        public async Task<IHttpActionResult> RegisteredEvents()
        {
            var user = await UserManager.Users.Include(u => u.RegisteredEvents).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            
            return Ok(user?.RegisteredEvents);
        }

        [HttpGet]
        [Route("RegistrationRequests")]
        public async Task<IHttpActionResult> RegistrationRequests()
        {
            var user =
                await UserManager.Users.Include(u => u.RegistrationRequests)
                        .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            return Ok(user?.RegistrationRequests);
        }

        [HttpGet]
        [Route("BookmarkedEvents")]
        public async Task<IHttpActionResult> BookmarkedEvents()
        {
            var user = await UserManager.Users.Include(u => u.BookmarkedEvents)
                        .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            return Ok(user?.BookmarkedEvents);
        }

        [HttpGet]
        [Route("Search/{searchPhrase?}/{sportType?}/{eventType?}/{startingDate?}/{zipCode?}/{city?}/{startingPrice?}")]
        public async Task<IHttpActionResult> Search([FromUri] string searchPhrase = "", [FromUri]int sportType = 0, [FromUri]int eventType = 0, [FromUri]DateTime? startingDate = null, [FromUri]string zipCode = "", [FromUri]int city = 0, [FromUri]float? startingPrice = 0.0F)
        {
            var events = await DbContext.Events.Where(e => e.Description.Contains(searchPhrase) && (sportType == 0 || e.SportId == sportType) && (eventType == 0 || e.EventTypeId == eventType) && (startingDate == null || e.BeginDate > startingDate) && (zipCode == "" || e.Zip == zipCode) && (city == 0 || e.CityId == city) && (startingPrice == null || e.StartingPrice > startingPrice)).ToListAsync();
            return Ok(events);
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IHttpActionResult> Post(EventPostViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var city = DbContext.Cities.FindAsync(model.CityId);
                var sport = DbContext.Sports.FindAsync(model.SportId);
                var eventType = DbContext.EventTypes.FindAsync(model.EventTypeId);

                await Task.WhenAll(city, sport, eventType);
                var _event = ModelFactory.Get(model);

                _event.CityId = city.Result.Id;
                _event.SportName = sport.Result.Name;
                _event.SportId = sport.Result.Id;
                _event.EventTypeId = eventType.Result.Id;
                _event.EventTypeName = eventType.Result.Name;
                _event.OrganizerId = User.Identity.GetUserId();
                _event.OrganizerName = User.Identity.Name;

                //saving to database
                DbContext.Events.Add(_event);
                var result = await DbContext.SaveChangesAsync();


                if (result > 0)
                {
                    return Ok(_event);
                }
                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("BookmarkEvents")]
        public async Task<IHttpActionResult> BookmarkEvents(BookmarkEventsModel model)
        {
            try
            {
                var user = await UserManager.Users.Include(u => u.BookmarkedEvents).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                var event_ = await DbContext.Events.Include(e => e.BookmarkerVisitors).FirstOrDefaultAsync(e => e.Id == model.Id);
                if (user.BookmarkedEvents == null)
                {
                    user.BookmarkedEvents = new List<Event>();
                }
                if (user.BookmarkedEvents.All(e => e.Id != event_.Id))
                {
                    user.BookmarkedEvents.Add(event_);
                    var result = await DbContext.SaveChangesAsync();
                    if (result <= 0)
                    {
                        return InternalServerError();
                    }
                }
                if (event_.BookmarkerVisitors.All(e => e.Id != user.Id))
                {
                    event_.BookmarkerVisitors.Add(user);
                    var identityResult = await UserManager.UpdateAsync(user);
                    if (!identityResult.Succeeded)
                    {
                        return InternalServerError();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [Route("RequestRegistration")]
        public async Task<IHttpActionResult> RegisterationRequests(RegistrationRequestsModel model)
        {
            var user = await UserManager.Users.Include(u => u.RegistrationRequests).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var event_ = await DbContext.Events.FindAsync(model.id);
            if (user.RegistrationRequests == null)
            {
                user.RegistrationRequests = new List<Event>();
            }
            user.BookmarkedEvents.Add(event_);
            await UserManager.UpdateAsync(user);
            return Ok(event_);
        }

        [Route("AcceptRegistrationRequests")]
        public async Task<IHttpActionResult> AcceptRegistrationRequests(AcceptRegistrationRequestsModel model)
        {
            var user = await UserManager.Users.Include(u => u.RegisteredEvents).Include(u => u.RegistrationRequests).Where(u => u.UserName == model.userName).FirstOrDefaultAsync();
            var event_ = await DbContext.Events.Include(ev => ev.RegisteredVisitors).Include(ev => ev.RegisterRequestVisitors).FirstOrDefaultAsync(ev => ev.Id == model.eventId);
            var organizer = await UserManager.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var organizerEvents = DbContext.Events.Where(ev => ev.OrganizerName == User.Identity.Name).ToList();

            if (!organizerEvents.Any(ev => ev.Id == model.eventId))
            {
                return BadRequest();
            }
            if (!event_.RegisterRequestVisitors.Any(u => u.UserName == model.userName))
            {
                return BadRequest();
            }
            if (!user.RegistrationRequests.Any(ev => ev.Id == model.eventId))
            {
                return BadRequest();
            }
            event_.RegisterRequestVisitors.Remove(user);
            event_.RegisteredVisitors.Add(user);
            user.RegistrationRequests.Remove(event_);
            user.RegisteredEvents.Add(event_);

            var dbEntityEntryEvent = DbContext.Entry(event_);
            dbEntityEntryEvent.State = EntityState.Modified;

            var dbEnttityEntryUser = DbContext.Entry(user);
            dbEnttityEntryUser.State = EntityState.Modified;

            await UserManager.UpdateAsync(user);
            await DbContext.SaveChangesAsync();
            return Ok();
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var _event = await DbContext.Events.SingleOrDefaultAsync();

            throw new NotImplementedException();
        }
    }
}