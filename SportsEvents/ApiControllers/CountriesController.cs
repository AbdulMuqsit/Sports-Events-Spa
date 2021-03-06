﻿using SportsEvents.Infrastructure;
using SportsEvents.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SportsEvents.ApiControllers
{
    [RoutePrefix("api/Countries")]
    public class CountriesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {

            return Ok(await DbContext.Countries.ToListAsync());

        }
        [HttpGet]
        [Route("{id:int}/Cities")]
        public async Task<IHttpActionResult> GetCities(int id)
        {
            var cities = await DbContext.Cities.Where(city => city.CountryId == id).ToListAsync();
            return Ok(cities);
        }

        [HttpPost]

        public async Task<IHttpActionResult> Post(CountryPostViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var country = ModelFactory.Get(model);

                DbContext.Countries.Add(country);
                var result = await DbContext.SaveChangesAsync();

                if (result > 0)
                {
                    return Ok(country);
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
            //var _city = await DbContext.Cities.SingleOrDefaultAsync();
            throw new NotImplementedException();
        }
    }
}