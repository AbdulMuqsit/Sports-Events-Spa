using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using SportsEvents.Common.Entities;
using SportsEvents.Infrastructure;
using SportsEvents.Models;
using System.Web;
using System.Diagnostics;
using System.IO;
using OfficeOpenXml;

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
        public async Task<IHttpActionResult> GetCalender([FromUri] int page = 0, [FromUri] int take = 20)

        {
            var authenticated = User.Identity.IsAuthenticated;
            var userId = User.Identity.GetUserId();
            var events =
                await
                    DbContext.Events.Include(e => e.RegisterRequestVisitors)
                        .Include(e => e.RegisteredVisitors)
                        .Include(e => e.BookmarkerVisitors)
                        .Where(e => e.BeginDate > DateTime.UtcNow)
                        .OrderBy(e => e.BeginDate)
                        .Skip(page * take)
                        .Take(take)
                        .Select(e => new
                        {
                            e.Id,
                            e.Description,
                            e.OrganizerName,
                            e.Details,
                            e.EventTypeName,
                            e.SportName,
                            e.BeginDate,
                            e.EndDate,
                            e.StartingPrice,
                            Bookmarked = authenticated && e.BookmarkerVisitors.Any(u => u.Id == userId),
                            Registered = authenticated && e.RegisteredVisitors.Any(u => u.Id == userId),
                            RequestedRegistration = authenticated && e.RegisterRequestVisitors.Any(u => u.Id == userId)
                        }).ToListAsync();


            return Ok(events);
        }

        [HttpGet]
        [Route("MyEvents")]
        public async Task<IHttpActionResult> GetMyEvents()
        {
            var userId = User.Identity.GetUserId();
            var events =
                await
                    DbContext.Events.Include(e => e.RegisterRequestVisitors)
                        .Include(e => e.RegisteredVisitors)
                        .Include(e => e.BookmarkerVisitors)
                        .Include(e => e.ClickerUsers)
                        .Where(_event => _event.OrganizerId == userId)
                        .Select(
                            e =>
                                new
                                {
                                    e.Id,
                                    e.Description,
                                    BookmarksCount = e.BookmarkerVisitors.Count,
                                    RegistrationRequestsCount = e.RegisterRequestVisitors.Count,
                                    RegisteredVisitorsCount = e.RegisteredVisitors.Count,
                                    ClicksCount = e.ClickerUsers.Count
                                })
                        .ToListAsync();
            return Ok(events);
        }

        [HttpGet]
        [Route("RegisteredEvents")]
        public async Task<IHttpActionResult> RegisteredEvents()
        {
            var user =
                await
                    UserManager.Users.Include(u => u.RegisteredEvents)
                        .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

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
        public async Task<IHttpActionResult> Search([FromUri] string searchPhrase = "", [FromUri] int sportType = 0,
            [FromUri] int eventType = 0, [FromUri] DateTime? startingDate = null, [FromUri] string zipCode = "",
            [FromUri] int city = 0, [FromUri] float? startingPrice = 0.0F)
        {
            var events =
                await
                    DbContext.Events.Where(
                        e =>
                            e.Description.Contains(searchPhrase) && (sportType == 0 || e.SportId == sportType) &&
                            (eventType == 0 || e.EventTypeId == eventType) &&
                            (startingDate == null || e.BeginDate > startingDate) && (zipCode == "" || e.Zip == zipCode) &&
                            (city == 0 || e.CityId == city) &&
                            (startingPrice == null || e.StartingPrice > startingPrice)).ToListAsync();
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
                var city = await DbContext.Cities.FindAsync(model.CityId);
                var sport = await DbContext.Sports.FindAsync(model.SportId);
                var eventType = await DbContext.EventTypes.FindAsync(model.EventTypeId);

                var _event = ModelFactory.Get(model);

                _event.CityId = city.Id;
                _event.SportName = sport.Name;
                _event.SportId = sport.Id;
                _event.EventTypeId = eventType.Id;
                _event.EventTypeName = eventType.Name;
                _event.OrganizerId = User.Identity.GetUserId();
                _event.OrganizerName = User.Identity.Name;

                //saving to database
                DbContext.Events.Add(_event);
                var result = await DbContext.SaveChangesAsync();

                if (result > 0)
                {
                    return Ok(new { _event.Description, _event.Details, _event.Id, _event.SportName, _event.EventTypeName, _event.OrganizerName });
                }
                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("BookmarkEvent")]
        public async Task<IHttpActionResult> BookmarkEvent(BookmarkEventsModel model)
        {
            try
            {
                var user =
                    await
                        UserManager.Users.Include(u => u.BookmarkedEvents)
                            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                var event_ =
                    await DbContext.Events.Include(e => e.BookmarkerVisitors).FirstOrDefaultAsync(e => e.Id == model.Id);
                if (user.BookmarkedEvents == null)
                {
                    user.BookmarkedEvents = new List<Event>();
                }
                if (event_.BookmarkerVisitors == null)
                {
                    event_.BookmarkerVisitors = new List<User>();
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
        [HttpPost]
        [Authorize(Roles = "Visitor")]
        public async Task<IHttpActionResult> RequestRegistration(RegistrationRequestsModel model)
        {
            var user =
                await
                    UserManager.Users.Include(u => u.RegistrationRequests)
                        .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var event_ =
                await
                    DbContext.Events.Include(e => e.RegisterRequestVisitors).FirstOrDefaultAsync(e => e.Id == model.Id);
            if (user.RegistrationRequests == null)
            {
                user.RegistrationRequests = new List<Event>();
            }
            if (event_.RegisterRequestVisitors == null)
            {
                event_.RegisterRequestVisitors = new List<User>();
            }
            if (user.RegistrationRequests.All(e => e.Id != event_.Id))
            {
                user.RegistrationRequests.Add(event_);
                var result = await DbContext.SaveChangesAsync();
                if (result <= 0)
                {
                    return InternalServerError();
                }
            }
            if (user.RegistrationRequests.All(e => e.Id != model.Id))
            {
                user.RegistrationRequests.Add(event_);
                var identityResult = await UserManager.UpdateAsync(user);
                if (!identityResult.Succeeded)
                {
                    return InternalServerError();
                }
            }
            return Ok();
        }

        [Route("AcceptRegistrationRequests")]
        public async Task<IHttpActionResult> AcceptRegistrationRequests(AcceptRegistrationRequestsModel model)
        {
            try
            {
                var user =
                       await
                           UserManager.Users.Include(u => u.RegisteredEvents)
                               .Include(u => u.RegistrationRequests)
                               .Where(u => u.Id == model.UserId)
                               .FirstOrDefaultAsync();
                var event_ =
                    await
                        DbContext.Events.Include(ev => ev.RegisteredVisitors)
                            .Include(ev => ev.RegisterRequestVisitors)
                            .FirstOrDefaultAsync(ev => ev.Id == model.EventId);

                var organizerEvents = DbContext.Events.Where(ev => ev.OrganizerName == User.Identity.Name).ToList();

                if (organizerEvents.All(ev => ev.Id != model.EventId))
                {
                    return BadRequest();
                }
                if (event_.RegisterRequestVisitors.All(u => u.Id != model.UserId))
                {
                    return BadRequest();
                }
                if (user.RegistrationRequests.All(ev => ev.Id != model.EventId))
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
                var result = await DbContext.SaveChangesAsync();
                if (result <= 0)
                {
                    return InternalServerError();
                }
                var identityResult = await UserManager.UpdateAsync(user);
                if (!identityResult.Succeeded)
                {
                    return InternalServerError();
                }

                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var _event = await DbContext.Events.SingleOrDefaultAsync(e => e.Id == id);
                if (_event == null)
                {
                    return NotFound();
                }
                DbContext.Events.Remove(_event);
                var result = await DbContext.SaveChangesAsync();
                if (result <= 0)
                {
                    return InternalServerError();
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [Route("BulkRead")]
        [HttpPost]
        public async Task<IHttpActionResult> BulkRead()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string rootPath = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new  MultipartFormDataStreamProvider(rootPath);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                var file = HttpContext.Current.Request.Files[0];
                if (file == null)
                {
                    return BadRequest();
                }
                //if file[0] == null return badrequest
                //using (var stream = new MemoryStream())
                //{
                //    file.InputStream.CopyTo(stream);
                //}
                //byte[] file = File.ReadAllBytes(@"C:\file.xlsx");

                var stream = new MemoryStream();
                file.InputStream.CopyTo(stream);

                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                    }
                    else
                    {
                        foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
                        {


                        }
                    }
                }
                throw new NotImplementedException();

            }

 
            catch
                (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}