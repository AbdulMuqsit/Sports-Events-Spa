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
        [Route("RegistrationRequests")]
        public async Task<IHttpActionResult> GetRegistrationRequests()
        {
            var events = await DbContext.Events.Where(_event => _event.OrganizerId == User.Identity.GetUserId()).Include(_event => _event.RegisterRequestVisitors).Select(_event => new
            {
                _event.Id,
                _event.Description,
                RegistrationRequests = _event.RegisterRequestVisitors.Select(user => new { user.Id, user.UserName })

            }).ToListAsync();
            return Ok(events);
        }

        [HttpGet]
        [Route("RegisteredVisitors")]
        public async Task<IHttpActionResult> GetRegisteredVisitors()
        {
            var registeredVisitors = await DbContext.Events.Where(_event => _event.OrganizerId == User.Identity.GetUserId()).Include(_event => _event.RegisteredVisitors).Select(_event => new
            {
                _event.Id,
                _event.Description,
                RegisteredVisitors = _event.RegisteredVisitors.Select(user => new { user.Id, user.UserName })
            }).ToListAsync();
            return Ok(registeredVisitors);
        }

        [HttpGet]
        [Route("ClickerUsers")]
        public async Task<IHttpActionResult> GetClickerUsers()
        {
            var clickerUsers = await DbContext.Events.Where(_event => _event.OrganizerId == User.Identity.GetUserId()).Include(_event => _event.ClickerUsers).Select(_event => new
            {
                _event.Id,
                _event.Description,
                ClickerVisitors = _event.ClickerUsers.Select(user => new { user.Id, user.UserName })
            }).ToListAsync();
            return Ok(clickerUsers);
        }
     }
}