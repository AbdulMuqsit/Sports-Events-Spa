using SportsEvents.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using SportsEvents.Models;
using System.Threading.Tasks;

namespace SportsEvents.ApiControllers
{
    public class SportsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var sports = await DbContext.Sports.ToListAsync();
            return Ok(sports);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(SportPostViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var sport = ModelFactory.Get(model);

                DbContext.Sports.Add(sport);
                var result = await DbContext.SaveChangesAsync();

                if (result > 0)
                {
                    return Ok(sport);
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