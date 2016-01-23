using SportsEvents.Infrastructure;
using SportsEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SportsEvents.ApiControllers
{
    public class EventTypesController : ApiControllerBase
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(EventTypePostViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var eventType = ModelFactory.Get(model);

                DbContext.EventTypes.Add(eventType);
                var result = await DbContext.SaveChangesAsync();

                if (result > 0)
                {
                    return Ok(eventType); 
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
            throw new NotImplementedException();
        }
    }
}