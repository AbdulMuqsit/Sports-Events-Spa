using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using SportsEvents.Infrastructure;
using SportsEvents.Models;

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

        public async Task<IHttpActionResult> Delete(int id)
        {
            var _event = await DbContext.Events.SingleOrDefaultAsync();

            throw new NotImplementedException();
        }
    }
}