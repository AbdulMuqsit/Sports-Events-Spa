using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SportsEvents.EntityFramework;

namespace SportsEvents.Infrastructure
{
    public class ApiControllerBase : ApiController
    {
        private SportsEventsDbContext _dbContext;
        private ApplicationUserManager _userManager;

        public SportsEventsDbContext DbContext
        {
            get { return _dbContext ?? (_dbContext = Request.GetOwinContext().Get<SportsEventsDbContext>()); }
            set { _dbContext = value; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? (_userManager = Request.GetOwinContext().Get<ApplicationUserManager>()); }
            set { _userManager = value; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }

}