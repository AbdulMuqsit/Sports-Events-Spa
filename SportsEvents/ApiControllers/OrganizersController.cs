using Microsoft.AspNet.Identity;
using SportsEvents.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SportsEvents.ApiControllers
{
    [RoutePrefix("api/Organizer")]
    public class OrganizersController : ApiControllerBase
    {
        //    GetRegisterdEvents
        //    ClickedEvents

        [HttpGet]
        [Authorize]
        [Route("RegistrationRequests")]
        public async Task<IHttpActionResult> GetRegistrationRequests()
        {
            var userId = User.Identity.GetUserId();
            var events = await DbContext.Events.Include(_event => _event.RegisterRequestVisitors).Where(_event => _event.OrganizerId == userId && _event.RegisterRequestVisitors.Any()).Select(_event => new
            {
                _event.Id,
                _event.Description,
                Users = _event.RegisterRequestVisitors.Select(user => new { user.Id, user.UserName })

            }).ToListAsync();
            return Ok(events);
        }

        [HttpGet]
        [Authorize]

        [Route("RegisteredVisitors")]
        public async Task<IHttpActionResult> GetRegisteredVisitors()
        {
            var userId = User.Identity.GetUserId();
            var registeredVisitors = await DbContext.Events.Include(_event => _event.RegisteredVisitors).Where(_event => _event.OrganizerId == userId && _event.RegisteredVisitors.Any()).Select(_event => new
            {
                _event.Id,
                _event.Description,
                Users = _event.RegisteredVisitors.Select(user => new { user.Id, user.UserName })
            }).ToListAsync();
            return Ok(registeredVisitors);
        }

        [HttpGet]
        [Authorize]

        [Route("ClickerUsers")]
        public async Task<IHttpActionResult> GetClickerUsers()
        {
            var userId = User.Identity.GetUserId();
            var clickerUsers = await DbContext.Events.Include(_event => _event.ClickerUsers).Where(_event => _event.OrganizerId == userId && _event.ClickerUsers.Any()).Select(_event => new
            {
                _event.Id,
                _event.Description,
                Users = _event.ClickerUsers.Select(user => new { user.Id, user.UserName })
            }).ToListAsync();
            return Ok(clickerUsers);
        }
    }
}