using System;
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
        public IHttpActionResult Get()
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("MyEvents")]
        public async Task<IHttpActionResult> GetMyEvents()
        {
            var userId = User.Identity.GetUserId();
            var events = await DbContext.Events.Where(_event => _event.OrganizerId == userId).ToListAsync();
            return Ok(events);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(EventPostViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var _event = ModelFactory.Get(model);

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
            var user = await UserManager.Users.Include(u => u.BookmarkedEvents).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var event_ = await DbContext.Events.FindAsync(model.id);
            if (user.BookmarkedEvents == null)
            {
                user.BookmarkedEvents = new List<Event>();
            }
            user.BookmarkedEvents.Add(event_);
            await UserManager.UpdateAsync(user);
            return Ok(event_);
        }

        [Route("RegistrationRequests")]
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
            var user = await UserManager.Users.Include(u => u.RegisteredEvents).Include(u=>u.RegistrationRequests).Where(u=>u.UserName == model.userName).FirstOrDefaultAsync();
            var event_ = await DbContext.Events.Include(ev => ev.RegisteredVisitors).Include(ev => ev.RegisterRequestVisitors).FirstOrDefaultAsync(ev=>ev.Id == model.eventId);
            var organizer = await UserManager.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var organizerEvents = DbContext.Events.Where(ev => ev.OrganizerName == User.Identity.Name).ToList();

            if (!organizerEvents.Any(ev=>ev.Id == model.eventId))
            {
                return BadRequest();
            }
            if (!event_.RegisterRequestVisitors.Any(u=>u.UserName==model.userName))
            {
                return BadRequest();
            }
            if (!user.RegistrationRequests.Any(ev=>ev.Id==model.eventId))
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