using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SportsEvents.Models;
using SportsEvents.EntityFramework;
using SportsEvents.Infrastructure;

namespace SportsEvents.ApiControllers
{
    public class EventsController : ApiControllerBase
    {
        [HttpGet]
        public IHttpActionResult Get()
        {

            throw new NotImplementedException();



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
        
        public async Task<IHttpActionResult> Delete(int id)
        {
            var _event = await DbContext.Events.SingleOrDefaultAsync();

            throw new NotImplementedException();
        }

    }
}