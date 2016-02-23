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
    public class CitiesController : ApiControllerBase
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Post(CityPostViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var city = ModelFactory.Get(model);

                DbContext.Cities.Add(city);
                var result = await DbContext.SaveChangesAsync();

                if (result > 0)
                {
                    return Ok(city);
                }
                return InternalServerError();
            }
            catch   (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            //var _city = await DbContext.Cities.SingleOrDefaultAsync();
            throw new NotImplementedException();
        }
    }
}